using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterswitchNameEnquiry.DTO
{
    public class Customer
    {
        public string Accountnumber { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string OtherNames { get; set; }
        public string AddrLine1 { get; set; }
        public string AddrLine2 { get; set; }
        public string City { get; set; }

        public string StateCode { get; set; }
        public string CustomerPhoneNo { get; set; }
        public string AccountType { get; set; }
        public string AccountCurrency { get; set; }
        public string CountryCode { get; set; }
        public string Identification { get; set; }
        public string IdNumber { get; set; }
        public string CountryOfIssue { get; set; }
        public string Id_Expiry_date { get; set; }

        public string Nationality { get; set; }
        public string DOB { get; set; } 
    }
}