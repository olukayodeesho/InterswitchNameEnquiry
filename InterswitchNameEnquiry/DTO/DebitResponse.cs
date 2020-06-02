using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterswitchNameEnquiry.DTO
{
    public class DebitResponse
    {
        public string responseCode { get; set; }
        public string responseMessage { get; set; }
   
        public decimal amount { get; set; }
        public string transactionId { get; set; }
        public DateTime transactionDate { get; set; }
        public string requestReference { get; set; }
    }
}