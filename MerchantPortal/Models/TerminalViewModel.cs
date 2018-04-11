using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class TerminalViewModel
    {
        //public Merchant Merchant { get; set; }
        //public Terminal Terminal { get; set; }
        
        public Int64 Id { get; set; }
        public Int64 MerchantId { get; set; }

        [Display(Name = "Merchant Name")]
        public string MerchantName { get; set; }

        [Display(Name = "Terminal Name")]
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

        public IEnumerable<Merchant> Merchants { get; set; }
    }
}
