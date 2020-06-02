using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterswitchNameEnquiry.DTO
{
    public class DebitRequest
    {
        public string destinationBankCode { get; set; }
        public string destinationAccount { get; set; }
        public string  sourceAccount { get; set; }
        public decimal amount { get; set; }
        public string transactionId { get; set; }
        public string sourceAccountName { get; set; }
        public string destinationAccountName { get; set; }
    }
}