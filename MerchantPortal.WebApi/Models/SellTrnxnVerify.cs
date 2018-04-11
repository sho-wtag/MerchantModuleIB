using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantPortal.WebApi.Models
{
    public class SellTrnxnVerify
    {
        public string Action { get; set; }
        public string BankRefId { get; set; }
        public string CurrencyCode { get; set; }
        public string MerTransactionID { get; set; }
        public string MerRefID { get; set; }
        public string TxnAmount { get; set; }
        public string TxnStatus { get; set; }
        public string ServerTime { get; set; }      
    }
}