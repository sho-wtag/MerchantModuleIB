using MerchantPortal.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Models
{
    public class MenuViewModel : Menu, IMessage
    {
        public bool IsSelected { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public ControllerActionMapping ControllerActionMapping { get; set; }
        public RolePermission RolePermission { get; set; }

        public IEnumerable<Menu> MenuList { get; set; }
        public IEnumerable<ApplicationRole> ApplicationRoleList { get; set; }        
        public IEnumerable<RolePermission> RolePermissionList { get; set; }

        public List<ControllerActionMapping> ControllerActionMappingList { get; set; }

        public string MessageText { get; set; }
    }
}
