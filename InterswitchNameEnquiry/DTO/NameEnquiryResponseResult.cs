using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterswitchNameEnquiry.DTO
{
    public class NameEnquiryResponseResult
    {
        public string ResponseCode { get; set; }
        public string CustomerID { get; set; }
        public CustomerName CustomerName { get; set; }
        public CustomerAddress CustomerAddress { get; set; }
        public string CustomerPhoneNo { get; set; } //ensure its only number
        public string AccountType { get; set; }
        public string AccountCurrency { get; set; }
        public string CountryCode { get; set; }
        public string Nationality { get; set; }
        public string DOB { get; set; }
        public string CountryOfBirth { get; set; } 
    }
     



}