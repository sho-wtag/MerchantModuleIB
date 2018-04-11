using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MerchantPortal.Data.Models
{

    [Table("Mct_Commission_Setup")]
    [Serializable]
    public class MctCommissionSetup : ICommonModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Display(Name = "Merchant")]
        [Column("merchantId")]
        public Int64 MerchantId { get; set; }
        [NotMapped]
        public String MerchantName { get; set; }


        [Display(Name = "Terminal")]
        [Column("terminalId")]
        public Int64 TerminalId { get; set; }
        [NotMapped]
        public String TerminalName { get; set; }


        [Display(Name = "Description")]
        [StringLength(100, ErrorMessage = "Maximum 100 chracters.")]
        [Column("Description")]
        public string Description { get; set; }


        [Display(Name = "Country")]
        [Column("countryId")]
        public int CountryId { get; set; }
        [NotMapped]
        public String CountryName { get; set; }


        [Display(Name = "Product")]
        [Column("productId")]
        public int ProductId { get; set; }
        [NotMapped]
        public String ProductName { get; set; }

        [Display(Name = "Transaction type")]
        [StringLength(100, ErrorMessage = "Maximum 100 chracters.")]
        [Column("trnxnType")]
        public string TrnxnType { get; set; }


        [Display(Name = "Currency")]
        [Column("currencyId")]
        public int CurrencyId { get; set; }
        [NotMapped]
        public String CurrencyName { get; set; }

        [Display(Name = "Continuous type")]
        [Column("IsContinuousType")]
        public bool IsContinuousType { get; set; }


        [Display(Name = "Calculation type")]
        [Column("IsPercentage")]
        public bool IsPercentage { get; set; }
        [NotMapped]
        public String CalculationType { get; set; }

        [Display(Name = "Amount")]
        [Column("commissionAmount")]
        public decimal CommissionAmount { get; set; }

        [Display(Name = "Bank percentage(%)")]
        [Column("BankPercentage")]
        public decimal BankPercentage { get; set; }
        

        [Display(Name = "Minimum amount")]
        [Column("minAmount")]
        public decimal MinAmount { get; set; }

        [Display(Name = "Is round up")]
        [Column("IsRoundUpAmount")]
        public bool IsRoundUpAmount { get; set; }

        [Display(Name = "Is round down")]
        [Column("IsRoundDownAmount")]
        public bool IsRoundDownAmount { get; set; }


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
