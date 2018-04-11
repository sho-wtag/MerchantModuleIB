using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class MctVATSetupViewModel
    {
        public MctVATSetup MctVATSetup { get; set; }

        public IEnumerable<Merchant> Merchants { get; set; }
        public IEnumerable<Terminal> Terminals { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }
    }
}
