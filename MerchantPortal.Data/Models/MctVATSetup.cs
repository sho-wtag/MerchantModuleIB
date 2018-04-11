using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{
    [Table("Mct_VAT_Setup")]
    public class MctVATSetup : ICommonModel
    {
        [Column("ID")]
        public int ID { get; set; }

        [Column("VATID")]
        public String VATID { get; set; }

        [Display(Name = "Merchant")]
        [Column("MerchantID")]
        public Int64 MerchantID { get; set; }
        [NotMapped]
        public String MerchantName { get; set; }

        [Display(Name = "Terminal")]
        [Column("TerminalID")]
        public Int64 TerminalId { get; set; }
        [NotMapped]
        public String TerminalName { get; set; }

        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "Maximum 100 chracters.")]
        [Column("Descriptions")]
        public string Descriptions { get; set; }

        [Display(Name = "Country")]
        [Column("CountryID")]
        public int CountryID { get; set; }
        [NotMapped]
        public String CountryName { get; set; }

        [Display(Name = "VAT Reg No")]
        [StringLength(50, ErrorMessage = "Maximum 50 chracters.")]
        [Column("VATRegNo")]
        public string VATRegNo { get; set; }

        [Display(Name = "Continuous type")]
        [Column("IsContinuousType")]
        public bool IsContinuousType { get; set; }

        [Display(Name = "Percentage")]
        [Column("Percentage")]
        public decimal Percentage { get; set; }

        // interface propertise
        [Display(Name = "Active Status")]
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
        [NotMapped]
        public Int64 EntryBy { get; set; }
        [NotMapped]
        public DateTime EntryDate { get; set; }
        [NotMapped]
        public Int64 UpdatedBy { get; set; }
        [NotMapped]
        public DateTime UpdatedDate { get; set; }
    }
}
