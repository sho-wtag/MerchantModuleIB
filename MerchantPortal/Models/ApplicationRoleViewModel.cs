using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class ApplicationRoleViewModel : ApplicationRole, IMessage
    {
        public string MessageText { get; set; }
    }
}
