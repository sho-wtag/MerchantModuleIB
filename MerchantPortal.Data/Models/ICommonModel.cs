using System;
using System.Collections.Generic;
using System.Text;

namespace MerchantPortal.Data.Models
{
    public interface ICommonModel
    {
        bool IsActive { get; set; }
        Int64 UpdatedBy { get; set; }
        DateTime UpdatedDate { get; set; }
        Int64 EntryBy { get; set; }
        DateTime EntryDate { get; set; }
        bool IsDeleted { get; set; }
    }
}
