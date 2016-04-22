﻿using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using BExIS.Security.Entities.Subjects;
using DataAnnotationsExtensions;

namespace BExIS.Web.Shell.Areas.SAM.Models
{
    public class AccountRegisterModel
    {
        [Display(Name = "User Name")]
        [RegularExpression("^[\\S]*$", ErrorMessage = "The user name must not contain spaces.")]
        [Remote("ValidateUserName", "Account")]
        [Required(AllowEmptyStrings = false)]
        [StringLength(64, ErrorMessage = "The user name must be {2} - {1} characters long.", MinimumLength = 3)]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [RegularExpression("^[\\S]*$", ErrorMessage = "The password must not contain spaces.")]
        [Required]
        [StringLength(24, ErrorMessage = "The password must be {2} - {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [global::System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        [Display(Name = "Confirm Password")]
        [Required]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Email]
        [Remote("ValidateEmail", "Account")]
        [Required]
        [StringLength(250, ErrorMessage = "The email must be {2} - {1} characters long.", MinimumLength = 5)]
        public string Email { get; set; }

        [Display(Name = "Security Answer")]
        [RegularExpression("^[^\\s]+(\\s+[^\\s]+)*", ErrorMessage = "The security answer must start and end with no space.")]
        [Required]
        [StringLength(50, ErrorMessage = "The security answer must be less than {1} characters long.")]
        public string SecurityAnswer { get; set; }

        public SecurityQuestionSelectListModel SecurityQuestionList { get; set; }

        public AuthenticatorSelectListModel AuthenticatorList { get; set; }

        public AccountRegisterModel()
        {
            SecurityQuestionList = new SecurityQuestionSelectListModel();
            AuthenticatorList = new AuthenticatorSelectListModel(true);
        }
    }

    public class MyAccountModel
    {
        [Display(Name = "User Id")]
        public long UserId { get; set; }

        public long AuthenticatorId { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [RegularExpression("^[\\S]*$", ErrorMessage = "The password must not contain spaces.")]
        [StringLength(24, ErrorMessage = "The password must be {2} - {1} characters long.", MinimumLength = 6)]
        public string Password { get; set; }

        [global::System.Web.Mvc.Compare("Password", ErrorMessage = "The password and confirmation do not match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Full Name")]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email Address")]
        [Email]
        [Remote("ValidateEmail", "Account", AdditionalFields = "UserId")]
        [Required]
        [StringLength(250, ErrorMessage = "The email must be {2} - {1} characters long.", MinimumLength = 5)]
        public string Email { get; set; }

        [Display(Name = "Security Answer")]
        [RegularExpression("^[^\\s]+(\\s+[^\\s]+)*", ErrorMessage = "The security answer must start and end with no space.")]
        [StringLength(50, ErrorMessage = "The security answer must be less than {1} characters long.")]
        public string SecurityAnswer { get; set; }

        public long SecurityQuestionId { get; set; }

        public SecurityQuestionSelectListModel SecurityQuestionList { get; set; }

        public AuthenticatorSelectListModel AuthenticatorList { get; set; }

        public MyAccountModel()
        {
            SecurityQuestionList = new SecurityQuestionSelectListModel();
            AuthenticatorList = new AuthenticatorSelectListModel(true);
        }

        public static MyAccountModel Convert(User user)
        {
            return new MyAccountModel()
            {
                UserId = user.Id,
                UserName = user.Name,
                FullName = user.FullName,
                Email = user.Email,
                AuthenticatorId = user.Authenticator.Id,
                SecurityQuestionId = user.Authenticator.Id == 1 ? user.SecurityQuestion.Id : 0
            };
        }
    }

    public class AccountLogOnModel
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public AuthenticatorSelectListModel AuthenticatorList { get; set; }

        public AccountLogOnModel()
        {
            AuthenticatorList = new AuthenticatorSelectListModel();
        }
    }

    public class ChangePasswordModel
    {
        public string UserName { get; set; }

        public string SecurityQuestion { get; set; }

        public string SecurityAnswer { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordModel
    {
        public string UserName { get; set; }
    }
}