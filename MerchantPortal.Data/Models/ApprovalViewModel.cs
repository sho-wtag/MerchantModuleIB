using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using MerchantPortal.Data.Models;

namespace MerchantPortal.Data.Models
{
    public class ApprovalViewModel
    {
        //public Merchant merchant { get; set; }
        //public ICollection<Merchant> Merchant { get; set; }

        //<---merchant info---->
        public Int64 Id { get; set; }
        public string MerchantName { get; set; }
        public bool IsApprove { get; set; }
        public bool IsActive { get; set; }

        //<---terminal info---->
        public Int64 Id2 { get; set; }
        public string OrgName { get; set; }
        public Int64 MerchantId { get; set; }

        //<---commission info---->
        public Int64 CommissionId { get; set; }
        public decimal CommissionAmount { get; set; }
        public decimal BankPercentage { get; set; }

        //<---vat info---->
        public Int64 VATId { get; set; }
        public String VATRegNo { get; set; }

        //<---gl info---->
        public Int64 GLId { get; set; }
        public String GLNumber { get; set; }
        public String AccountNo { get; set; }

        public Merchant Merchant { get; set; }
        public Terminal Terminal { get; set; }
        public MctCommissionSetup MctCommissionSetup { get; set; }
        public MctVATSetup MctVATSetup { get; set; }
        public MctGLSetup MctGLSetup { get; set; }
    }
}
