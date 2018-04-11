using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class MerchantViewModel
    {
        public Merchant Merchant { get; set; }
        public Terminal Terminal { get; set; }
        public MctCommissionSetup MctCommissionSetup { get; set; }
        public MctVATSetup MctVATSetup { get; set; }
        public MctGLSetup MctGLSetup { get; set; }

        public IEnumerable<Merchant> Merchants { get; set; }
        public IEnumerable<Terminal> Terminals { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<Currency> Currencies { get; set; }


    }
}
