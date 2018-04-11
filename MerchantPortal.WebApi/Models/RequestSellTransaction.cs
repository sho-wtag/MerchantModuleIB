using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantPortal.WebApi.Models
{
    public class RequestSellTransaction
    {      
        public string Action { get; set; }
        public string MerchantID { get; set; }
        public string TerminalID { get; set; }
        public string CurrencyCode { get; set; }
        public string MerTransactionID { get; set; }
        public string LanguageCode { get; set; }       
        public string MerRefID { get; set; }
        public string MerVar1 { get; set; }
        public string MerVar2 { get; set; }
        public string MerVar3 { get; set; }
        public string MerVar4 { get; set; }
        public string MerVar5 { get; set; }
        public string MerVar6 { get; set; }
        public string MerVar7 { get; set; }
        public string MerVar8 { get; set; }
        public string ProductType { get; set; }
        public string ReturnURL { get; set; }
        public string TxnAmount { get; set; }
    }
}