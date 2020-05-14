using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterswitchNameEnquiry.DTO
{
    public class CustomerAddress
    {
        public string AddrLine1 { get; set; }
        public string AddrLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string PostalCode { get; set; }
    }
}