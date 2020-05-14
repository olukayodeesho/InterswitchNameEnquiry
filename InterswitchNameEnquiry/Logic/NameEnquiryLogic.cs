using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterswitchNameEnquiry.DataAccess;
using InterswitchNameEnquiry.DTO;

namespace InterswitchNameEnquiry.Logic
{
    public class NameEnquiryLogic
    {
        public static NameEnquiryResponseResult DoNameEnquiry(string customerId)
        {

            var obj = new NameEnquiryResponseResult();
            try
            {
                if (customerId == "0003321999")
                {
                    obj.AccountCurrency = "566";
                    obj.AccountType = "10";
                    obj.CountryCode = "NG";
                    obj.CountryOfBirth = "Nigeria";
                    obj.CustomerAddress = new CustomerAddress { AddrLine1 = "No 30", AddrLine2 = "Benue Road", City = "Otukpo", PostalCode = "", StateCode = "" };
                    obj.CustomerID = "0003321999";
                    obj.CustomerName = new CustomerName { FirstName = "Balarabe", LastName = "Rilwan", OtherNames = "Musa" };
                    obj.CustomerPhoneNo = "08023221100";
                    obj.DOB = "05/09/1960";
                    obj.Nationality = "Nigerian";
                    obj.ResponseCode = "00";
                }
                else { obj.ResponseCode = "25"; }

                Repository.SaveRequestResponse(customerId, obj);

            }
            catch (Exception e)
            {
                Utils.LogError(e, "");
            }
            return obj;
        }

    }
}