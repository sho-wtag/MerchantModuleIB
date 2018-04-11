using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MerchantPortal.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Int64>, ICommonModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        public DateTime? ApprovalDate { get; set; }

        // interface propertise
        public bool IsActive { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ApplicationRole : IdentityRole<Int64>, ICommonModel
    {

        // interface propertise
        public bool IsActive { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class UsmUserRole : IdentityUserRole<Int64>, ICommonModel
    {

        public bool IsActive { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class Menu : ICommonModel
    {

        public int Id { get; set; }
        [Required]
        public int Parent_Id { get; set; }
        public bool IsChild { get; set; }
        public string key { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }

        // Common field

        public bool IsActive { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class ControllerActionMapping : ICommonModel
    {
        public int Id { get; set; }

        public int MenuId { get; set; }
        public string ControllerName { get; set; }
        public string ControllerToView { get; set; }
        public string ActionName { get; set; }
        public string ActionToView { get; set; }

        //public bool IsSelected { get; set; }
        // public DateTime CreatedDate { get; set; }
        // Common field

        public bool IsActive { get; set; }
        public Int64 UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public Int64 EntryBy { get; set; }
        public DateTime EntryDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class RolePermission
    {
        public int Id { get; set; }
        public Int64 RoleId { get; set; }
        public Int32 ControllerActionId { get; set; }
        public bool Permission { get; set; }
    }

}
