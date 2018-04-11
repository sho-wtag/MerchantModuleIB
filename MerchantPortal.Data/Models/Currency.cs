using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Models
{
    public class Currency
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }       
        public System.DateTime UpdatedDate { get; set; }       
        public bool IsDeleted { get; set; }
    }
}
