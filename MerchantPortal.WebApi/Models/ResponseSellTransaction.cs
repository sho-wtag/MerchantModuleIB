using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace MerchantPortal.WebApi.Models
{
    [DataContract]
    public class ResponseSellTransaction
    {
        [DataMember]
        public ObjectResult<Transaction> SellTransaction { get; set; }
    }
}