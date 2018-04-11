using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantPortal.Data.Models
{
    [Table("Mct_Transaction")]
    public class Transaction
    {
        public Int64 Id { get; set; }

        [Required]
        [Display(Name = "Merchant")]
        public Int64 MerchantId { get; set; }

        [Required]
        [Display(Name = "Terminal")]
        public Int64 TerminalId { get; set; }

        [Required]
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Transaction Particular")]
        public string TranParticular { get; set; }

        public string SuccessUrl { get; set; }
        public string FailureUrl { get; set; }
        public string CancelUrl { get; set; }

        [Display(Name = "Customer Bank")]
        public Int64 CustomerBankId { get; set; }

        [Display(Name = "Transaction Start")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime? StartStamp { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime? EndStamp { get; set; }

        [Display(Name = "Transaction Status")]
        public Int64 TrnxStatusId { get; set; }

        [Display(Name = "GL")]
        public Int64 GLId { get; set; }

        [Display(Name = "Principal")]
        public decimal? PrincipalAmount { get; set; }

        [Display(Name = "Comission")]
        public decimal? ComissionAmount { get; set; }

        [Display(Name = "VAT")]
        public decimal? VatAmount { get; set; }
       
        public string MerVar1 { get; set; }
        public string MerVar2 { get; set; }
        public string MerVar3 { get; set; }
        public string MerVar4 { get; set; }
        public string MerVar5 { get; set; }
        public string MerVar6 { get; set; }
        public string MerVar7 { get; set; }
        public string MerVar8 { get; set; }

        public Int64? CheckerId { get; set; }

        [Display(Name = "Settled Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? SettledDate { get; set; }

        [Display(Name = "Refund Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? RefundDate { get; set; }

        [Display(Name = "TTUM Download Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? TTUMDownloadDate { get; set; }

        [Display(Name = "Visitor IP")]
        public string VisitorIP { get; set; }

        [Display(Name = "Visitor Browser Info")]
        public string VisitorBrowserInfo { get; set; }

        public Int64? EntryBy { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd hh:mm tt}")]
        public DateTime? EntryDate { get; set; }

        public Int64? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [NotMapped]
        [Display(Name = "Merchant")]
        public String MerchantName { get; set; }

        [NotMapped]
        [Display(Name = "Terminal")]
        public String TerminalName { get; set; }

        [NotMapped]
        [Display(Name = "Currency")]
        public String CurrencyName { get; set; }

        [NotMapped]
        [Display(Name = "Customer Bank Name")]
        public String CustBankName { get; set; }

        [NotMapped]
        [Display(Name = "Transaction Status")]
        public String StatusName { get; set; }

        [NotMapped]
        [Display(Name = "GL")]
        public String GLNumber { get; set; }
        
        [NotMapped]
        [Display(Name = "Active Status")]
        public bool IsActive { get; set; }

        [Display(Name = "Transaction Ref")]
        public String TransactionRefId { get; set; }

        [Display(Name = "Merchant Ref")]
        public String MerchantRefId { get; set; }

        [Display(Name = "Bank Ref")]
        public String BankRefId { get; set; }
    }
}
