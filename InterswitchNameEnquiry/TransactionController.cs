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
        
            var response = NameEnquiryLogic.DoDebit(req);
            return Ok(response);
        }
    }
}