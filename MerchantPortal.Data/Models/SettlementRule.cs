using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Mahmudul Hasan
    /// Date: 11-Mar-2018
    /// Decription :Create model class for table Mct_SettlementRule
    /// Modified By : 
    /// Date: 
    /// </summary>
    /// 

    [Table("Mct_SettlementRule")]
    public class SettlementRule : ICommonModel
    {
        public Int64 Id { get; set; }

        [Required(ErrorMessage = "Merchant is required")]
        [Display(Name = "Merchant Name")]
        public Int64 MerchantId { get; set; }

        [NotMapped]
        [Display(Name = "Merchant Name")]
        public String MerchantName { get; set; }

        [Required(ErrorMessage = "Terminal is required")]
        [Display(Name = "Terminal Name")]
        public Int64 TerminalId { get; set; }

        [NotMapped]
        [Display(Name = "Terminal Name")]
        public String TerminalName { get; set; }

        [Display(Name = "Rule ID")]
        public string SettlementRuleId { get; set; }

        [Display(Name = "Description")]
        [StringLength(250, ErrorMessage = "Maximum 250 chracters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Settlement Type is required")]
        [Display(Name = "Settlement Type")]
        public string SettlementType { get; set; }

        [Required(ErrorMessage = "Frequency is required")]
        [Display(Name = "Frequency")]
        public int Frequency { get; set; }

        [Display(Name = "Enable Commission")]
        public bool CommissionEnable { get; set; }

        [Display(Name = "Commission")]
        public decimal? Commission { get; set; }

        [Display(Name = "Enable VAT")]
        public bool VATEnable { get; set; }

        [Display(Name = "VAT Percentage")]
        public decimal? VATPercentage { get; set; }

        [Display(Name = "Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Approve Status")]
        public bool IsApprove { get; set; }

        public bool IsDeleted { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
    }
}
