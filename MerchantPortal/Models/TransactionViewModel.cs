using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class TransactionViewModel : Transaction, IMessage
    {
        public Merchant Merchant { get; set; }
        public Terminal Terminal { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string SettleStatus { get; set; }
        public List<Transaction> Transactions { get; set; }
        public string MessageText { get; set; }
        
    }
}
