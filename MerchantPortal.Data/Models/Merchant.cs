using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Debashish Mondal
    /// Date: 28-Feb-2018
    /// Decription :Create model class for table Mct_Merchant
    /// Modified By : Mahmudul Hasan
    /// Date: 04-Mar-2018
    /// </summary>
    /// 

    [Table("Mct_Merchant")]
    [Serializable]
    public class Merchant : ICommonModel
    {
        public Int64 Id { get; set; }
        [Required]
        [Display(Name = "Merchant Name")]
        [StringLength(255, ErrorMessage = "Maximum 255 chracters.")]
        public string MerchantName { get; set; }


        [Required(ErrorMessage = "Organization Name required")]
        [Display(Name = "Organization Name")]
        public string BizOrgName { get; set; }

        [Required]
        [Display(Name = "Owner Name")]
        public string BizOwnerName { get; set; }

        [Required]
        [Display(Name = "Contact Address")]
        public string BizContactAddess { get; set; }

        [Required]
        [Display(Name = "Head Office Address")]
        public string HeadOfcAddress { get; set; }

        [Required]
        [Display(Name = "Buisness Phone")]
        public string BizPhone { get; set; }

        [Display(Name = "Buisness Fax")]
        public string BizFax { get; set; }

        [Required]
        [Display(Name = "Buisness Email")]
        public string BizEmail { get; set; }

        [Display(Name = "Buis. Contact Person")]
        public string BizContactPersonBcp { get; set; }

        [Display(Name = "Bcp Phone")]
        public string BcpPhone { get; set; }

        [Display(Name = "Bcp Address")]
        public string BcpAddress { get; set; }

        [Display(Name = "BCP Email")]
        public string BcpEmail { get; set; }

        [Display(Name = "Tech Contact Person")]
        public string TechContactPrsonTcp { get; set; }

        [Required]
        [Display(Name = "TCP Phone")]
        public string TcpPhone { get; set; }

        [Display(Name = "TCP Address")]
        public string TcpAddress { get; set; }

        [Required]
        [Display(Name = "TCP Email")]
        public string TcpEmail { get; set; }

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

        public virtual ICollection<Terminal> Terminals { get; set; }
    }
}
