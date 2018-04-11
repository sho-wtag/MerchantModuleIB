using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Models
{
    [Table("Usm_AuditTrail")]
    public class AuditTrail
    {
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string SessionID { get; set; }
        public int MenuID { get; set; }
        [Column(TypeName = "xml")]
        public string ChangeDetail { get; set; }
    }
}
