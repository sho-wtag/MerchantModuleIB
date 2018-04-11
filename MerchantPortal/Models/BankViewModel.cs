using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MerchantPortal.Data.Models;

namespace MerchantPortal.Models
{
    public class BankViewModel : Bank, IMessage
    {
        public string MessageText { get; set; }
    }
}
