//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MerchantPortal.WebApi
{
    using System;
    using System.Collections.Generic;
    
    public partial class Transaction
    {
        public long id { get; set; }
        public string mct_trnxn_id { get; set; }
        public Nullable<decimal> mct_trnxn_Amount { get; set; }
        public Nullable<short> currency_id { get; set; }
        public string success_url { get; set; }
        public string failure_url { get; set; }
        public string cancel_url { get; set; }
        public Nullable<long> customer_bank_acid { get; set; }
        public Nullable<System.DateTime> start_tstamp { get; set; }
        public Nullable<System.DateTime> end_tstamp { get; set; }
        public Nullable<short> trnxn_status_id { get; set; }
        public Nullable<long> mct_user_id { get; set; }
        public Nullable<long> customer_user_id { get; set; }
        public Nullable<long> gl_id { get; set; }
        public Nullable<double> principle_amount { get; set; }
        public Nullable<double> commission_amount { get; set; }
        public Nullable<double> vat_amount { get; set; }
        public Nullable<int> BankRefID { get; set; }
        public string MerVar1 { get; set; }
        public string MerVar2 { get; set; }
        public string MerVar3 { get; set; }
        public string MerVar4 { get; set; }
        public string MerVar5 { get; set; }
        public string MerVar6 { get; set; }
        public string MerVar7 { get; set; }
        public string MerVar8 { get; set; }
        public string mct_ref_id { get; set; }
    }
}
