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
    }
}