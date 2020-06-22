using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InterswitchNameEnquiry.DTO;
using InterswitchNameEnquiry.Logic;

namespace InterswitchNameEnquiry
{
    //do bearer validation for all methods
    [System.Web.Http.RoutePrefix("api/v1")]
    public class TransactionController : ApiController
    {
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("oauth/token")]
        public IHttpActionResult GenerateAccessToken()
        {
            string bearerAuth = string.Empty;
            string grantType = string.Empty;

            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            if (headers.Contains("Authorization"))
            {
                bearerAuth = headers.GetValues("Authorization").First();
                if (!IsBearerValid(bearerAuth)) { return Unauthorized(); };
            }
            else { return Unauthorized(); }
            if (headers.Contains("Grant-type"))
            {
                grantType = headers.GetValues("Grant-type").First();
                if (!grantType.Equals("client_credentials")) { return Unauthorized(); };
            }
            else { return Unauthorized(); }

            var response = NameEnquiryLogic.GenerateToken();
            if (response != null)
                return Ok(response);
            else
                return NotFound();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("accountdebit/transactions")]
        public IHttpActionResult DoDebit(DebitRequest req)
        {
            string bearerAccessToken = string.Empty;
            string signature = string.Empty;

            System.Net.Http.Headers.HttpRequestHeaders headers = this.Request.Headers;
            if (headers.Contains("Authorization"))
            {
                bearerAccessToken = headers.GetValues("Authorization").First();
                if (!bearerAccessToken.Trim().StartsWith("Bearer ")) { return Unauthorized(); };
                if (!Utils.IsAccessTokenValid(bearerAccessToken.Replace("Bearer ",""))) { return Unauthorized(); };
            }
            else { return Unauthorized(); }
            if (headers.Contains("signature"))
            {
                signature = headers.GetValues("signature").First();
                if (!IsSignatureValid(signature, req)) { return Unauthorized(); };
            }
            else { return Unauthorized(); }
            var response = NameEnquiryLogic.DoDebit(req);
            return Ok(response);
        }


        private bool IsBearerValid(string authBearer)
        {
            var isValid = false;
            try
            {
                Utils.LogInfo("IsBearerValid : " + authBearer);
                if (Utils.Base64StringDecoder(authBearer).Trim().Equals(string.Format("{0}:{1}", Utils.InterswitchRequestClientId, Utils.InterswitchRequestSecretKey))) { isValid = true; }
            }
            catch (Exception e) { Utils.LogError(e, ""); }
            return isValid;
        }
        private bool IsSignatureValid(string signature, DebitRequest req)
        {
            var isValid = false;
            try
            {
                Utils.LogInfo("IsSignatureValid : " + signature);
                if (Utils.Base64StringDecoder(signature).Trim().Equals(Utils.Sha512Hash( string.Format("{0}{1}{2}", req.amount,req.transactionId, Utils.InterswitchRequestSecretKey),true))) { isValid = true; }
            }
            catch (Exception e) { Utils.LogError(e, ""); }
            return isValid;
        }
    }
}