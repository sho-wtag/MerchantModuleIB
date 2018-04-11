using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MerchantPortal.WebApi.Models
{
    [DataContract]
    public class ResponseSellTrnxnVerify
    {
        [DataMember]
        public ObjectResult<PROC_REQ_FROM_MERCHANT_SELL_TRANS_VERIFY_Result> SellTransactionVerify { get; set; }
    }
}