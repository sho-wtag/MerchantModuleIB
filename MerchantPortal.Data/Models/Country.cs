using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Models
{
    [Table("Gen_Country")]
    public class Country
    {
        public int Id { get; set; }
        public int NumCountryCode { get; set; }
        public string StrCountryName { get; set; }
        public string StrRemarks { get; set; }
        public string ISOCode { get; set; }
        public bool BlackListed { get; set; }
        public string StrISOAlpha3 { get; set; }
        public Int16 NumISO { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        //public ICollection<Division> Division { get; set; }
        //public ICollection<Bank> Bank { get; set; }
        //public ICollection<Transaction> Transaction { get; set; }
        //public ICollection<ExchangeHouse> ExchangeHouse { get; set; }
    }
}
