using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterswitchNameEnquiry.DTO;
using InterswitchNameEnquiry.Logic;
using Newtonsoft.Json;

namespace InterswitchNameEnquiry.DataAccess
{
    public class Repository
    {
        public static void SaveRequestResponse(string customerId, NameEnquiryResponseResult response)
        {
            try
            {
                var jsonResp = JsonConvert.SerializeObject(response);
                using (var dc = new InterswitchNameEnquiryEntities())
                {
                    var obj = new RequestResponseLog();
                    obj.RequestBody = customerId;
                    obj.RequestTime = DateTime.Now;
                    obj.ResponseBody = jsonResp;
                    obj.ResponseTime = DateTime.Now;
                    dc.RequestResponseLogs.Add(obj);
                    dc.SaveChanges();

                }

            }
            catch (Exception e)
            {
                Utils.LogError(e, "SaveRequestResponse");
            }

        }
        public static void SaveRequestResponse(RequestResponseLog obj)
        {
            try
            {
                using (var dc = new InterswitchNameEnquiryEntities())
                {
                    dc.RequestResponseLogs.Add(obj);
                    dc.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Utils.LogError(e, "SaveRequestResponse");
            }

        }

        public static void SaveExceptionInDb(Exception e)
        {
            try
            {
                using (var context = new InterswitchNameEnquiryEntities())
                {
                    var ex = new ExceptionLog();
                    ex.ErrorDatetime = DateTime.Now;
                    ex.ErrorMessage = e.Message;
                    ex.ErrorSource = e.Source;
                    ex.ErrorStacktrace = e.StackTrace;

                    context.ExceptionLogs.Add(ex);
                    context.SaveChanges();
                }
            }
            catch (Exception ext)
            {
                Utils.LogError(e, "Original exception meant to be saved on the db ");
                Utils.LogError(ext, "Failed to create SaveExceptionLog ");
            }

        }
        public static void SaveAccessToken(AccessTokenLog token)
        {
            try
            {
                using (var context = new InterswitchNameEnquiryEntities())
                {
                    context.AccessTokenLogs.Add(token);
                    context.SaveChanges();
                }
            }
            catch (Exception ext)
            {
                Utils.LogError(ext, "Failed to create AccessTokenLog ");
            }

        }
        public static void SaveDebitTransactionLog(DebitTransactionLog log)
        {
            try
            {
                using (var context = new InterswitchNameEnquiryEntities())
                {
                    context.DebitTransactionLogs.Add(log);
                    context.SaveChanges();
                }
            }
            catch (Exception ext)
            {
                Utils.LogError(ext, "Failed to create DebitTransactionLog ");
            }

        }
        public static DebitTransactionLog SearchDebitTransactionLogByTransactionId(string transactionId)
        {
            var log = new DebitTransactionLog();
            try
            {
                using (var context = new InterswitchNameEnquiryEntities())
                {
                    log = context.DebitTransactionLogs.FirstOrDefault(g => g.InterswitchTransactionId.Trim() == transactionId.Trim());
                }
            }
            catch (Exception ext)
            {
                Utils.LogError(ext, "Failed to retrieve DebitTransactionLog ");
            }
            return log;
        }
        public static bool IsAccessTokenExired(string access_token)
        {
            var token = new AccessTokenLog();
            var isExpired = true;
            try
            {
                using (var context = new InterswitchNameEnquiryEntities())
                {
                    token = context.AccessTokenLogs.FirstOrDefault(g => g.AccessToken.Trim() == access_token.Trim());
                }
                if (token != null)
                {
                    if (token.ExpiryDate >= DateTime.Now)
                    { isExpired = true; }
                }
            }
            catch (Exception ext)
            {
                Utils.LogError(ext, "Failed to check IsAccessTokenExired ");
            }
            return isExpired;
        }
    }
}