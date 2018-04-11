using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class SettlementRuleViewModel : SettlementRule, IMessage
    {
        public Merchant Merchant { get; set; }
        public Terminal Terminal { get; set; }
        public string MessageText { get; set; }

        public IEnumerable<Merchant> Merchants { get; set; }
        public IEnumerable<Terminal> Terminals { get; set; }


    }
}
