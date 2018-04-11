using MerchantPortal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MerchantPortal.Data.Models.AccountViewModels
{
    public class RegisterViewModel : IMessage
    {
        public string MessageText { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ApplicationRole ApplicationRole { get; set; }
        public IEnumerable<ApplicationUser> ApplicationUsers { get; set; }
        public IEnumerable<ApplicationRole> ApplicationRoles { get; set; }

        #region comment
        //[Required]
        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }

        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[Display(Name = "Address")]
        //public string Address { get; set; }

        //[Required]
        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        //[Required]
        //[Display(Name = "User Role")]
        //public string UserRole { get; set; }

        //[Required]
        //[EmailAddress]
        //[Display(Name = "Email")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        //[Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        #endregion
    }

    public enum Role
    {
        SystemAdmin,
        Admin,
        User
    }
}
