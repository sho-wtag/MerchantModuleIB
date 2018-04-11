using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Kowshik Ahmed
    /// Date: 26-Dec-2017
    /// Decription :Create model class for table Gen_Bank
    /// </summary>
    [Table("Gen_Bank")]
    public class Bank
    {   
        public int Id { get; set; }        
        public string Name { get; set; }        
        public string SName { get; set; }
        public string Code { get; set; }        
        public string SwiftCode { get; set; }
        public string HeadOfficeAddress { get; set; }
        public string ContactNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; } 
        public bool IsDeleted { get; set; }
        public int CountryId { get; set; }
        public Country Country { get; set; }
        public ICollection<Branch> Branch { get; set; }
       // public ICollection<Transaction> Transaction { get; set; }
    }
}
