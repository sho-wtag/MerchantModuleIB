using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MerchantPortal.WebApi.Models
{
    public class RequestSellTrnxnVerify
    {
        public string Action { get; set; }
        public string MerchantID { get; set; }
        public string MerRefID { get; set; }
    }
}