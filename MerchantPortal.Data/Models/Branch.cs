using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Kowshik Ahmed
    /// Date: 26-Dec-2017
    /// Decription :Create model class for table Gen_Branch
    /// </summary>
    [Table("Gen_Branch")]
    public class Branch
    {
        public int Id { get; set; }        
        public string Name { get; set; }              
        
        public string BranchSortCode { get; set; }
        public string SwiftCode { get; set; }
        public string RoutingNo { get; set; }
        public byte Type { get; set; }        
        public string Address { get; set; }
        public string Contact { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }

        public int CityId { get; set; }

       // public City City { get; set; }

       // public ICollection<MembershipCommission> MembershipCommission { get; set; }

      //  public ICollection<Transaction> Transaction { get; set; }
    }
}
