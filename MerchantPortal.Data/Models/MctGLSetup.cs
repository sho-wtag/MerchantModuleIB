using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{
    [Table("Mct_GL_Setup")]
    public class MctGLSetup : ICommonModel
    {
        [Column("ID")]
        public int ID { get; set; }

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

        [Display(Name = "GL number")]
        [Column("GLNumber")]
        public String GLNumber { get; set; }

        [Display(Name = "Account number")]
        [Column("AccountNo")]
        public String AccountNo { get; set; }

        [Display(Name = "Account name")]
        [Column("AccountName")]
        public String AccountName { get; set; }

        [Display(Name = "Description")]
        [Column("Descriptions")]
        public String Descriptions { get; set; }

        // interface propertise
        [Display(Name = "Active Status")]
        [Column("IsActive")]
        public bool IsActive { get; set; }
        public bool IsApprove { get; set; }
        [Column("IsDeleted")]
        public bool IsDeleted { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
