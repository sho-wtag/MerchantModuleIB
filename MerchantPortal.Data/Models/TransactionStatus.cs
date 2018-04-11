using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantPortal.Data.Models
{
    /// <summary>
    /// Developed By:Mahmudul Hasan
    /// Date: 14-Mar-2018
    /// Decription :Create model class for table Trnxn_Status
    /// Modified By : 
    /// Date: 
    /// </summary>
    /// 

    [Table("Trnxn_Status")]
    public class TransactionStatus
    {
        public Int64 Id { get; set; }

        [Display(Name = "Status Code")]
        public String StatusCode { get; set; }
        
        [Display(Name = "Status Name")]
        public String StatusName { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }
    }
}
