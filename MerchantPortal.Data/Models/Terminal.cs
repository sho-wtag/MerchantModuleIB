using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Mahmudul Hasan
    /// Date: 06-Mar-2018
    /// Decription :Create model class for table Mct_Terminal
    /// Modified By : 
    /// Date: 
    /// </summary>
    /// 

    [Table("Mct_Terminal")]
    public class Terminal : ICommonModel
    {
        public Int64 Id { get; set; }

        [Required]
        [Display(Name = "Merchant Name")]
        [Column("FkMerchantId")]
        public Int64 MerchantId { get; set; }
        [NotMapped]
        public String MerchantName { get; set; }

        [Required]
        [Display(Name = "Terminal Name")]
        [StringLength(255, ErrorMessage = "Maximum 150 chracters.")]
        public string OrgName { get; set; }


        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        [Display(Name = "Contact Address")]
        public string ContactAddess { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNo { get; set; }

        [Display(Name = "Fax")]
        public string FaxNo { get; set; }

        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [Display(Name = "Contact Person")]
        public string ContactPerson { get; set; }

        [Display(Name = "Contact Person Phone")]
        public string ContactPersonPhone { get; set; }

        [Display(Name = "Contact Person Add.")]
        public string ContactPersonAddress { get; set; }

        [Display(Name = "Contact Person Email")]
        public string ContactPersonEmailId { get; set; }

        [Display(Name = "Trade License No")]
        public string TradeLicenseNo { get; set; }

        [Display(Name = "VAT Registration No")]
        public string VATRegistrationNo { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Approve Status")]
        public bool IsApprove { get; set; }

        public bool IsDeleted { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }

       // public virtual Merchant Merchant { get; set; }
    }
}
