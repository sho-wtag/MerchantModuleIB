using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class MctGLSetupViewModel
    {
        public MctGLSetup MctGLSetup { get; set; }

        public IEnumerable<Merchant> Merchants { get; set; }
        public IEnumerable<Terminal> Terminals { get; set; }
        
    }
}
