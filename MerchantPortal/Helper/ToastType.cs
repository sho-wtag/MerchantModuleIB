using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Helper
{
    public enum ToastType
    {
        [Description("success")]
        Success,
        [Description("info")]
        Info,
        [Description("warning")]
        Warning,
        [Description("error")]
        Error,
    }
}
