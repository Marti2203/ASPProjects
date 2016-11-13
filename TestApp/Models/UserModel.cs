using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using CommonFiles.Resource;
using System.Web.Mvc;

namespace TestApp.Models
{
    public class UserModel
    {
        public UserModel()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UserResources))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Username { get; set; }

        public SHA256 PasswordHash { get; set;}

        public SHA256 SecretAnswer { get; set; }

        [Display(Name = "Agreement", ResourceType = typeof(UserResources))]
        [BooleanValidation(true)]
        public bool Agreement { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(UserResources))]
        public Gender Gender { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Email{ get; set;}

        public string PasswordTest { get; set; }

        [Display(Name = "SecretQuestion", ResourceType = typeof(UserResources))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string SecretQuestion { get; set; }

        [DataType(DataType.MultilineText)]
        public string AboutText { get; set; }


        public readonly static SelectListItem[] Questions =
        {
            SelectListItemFactory.CrateSecretQuestion("Test"),
            SelectListItemFactory.CrateSecretQuestion("Second Test")
        };
    }
    public static class SelectListItemFactory
    {

        public static SelectListItem CrateSecretQuestion(string text)
        {
            SelectListItem item = new SelectListItem();
            item.Value = text;
            item.Text = text;
            return item;
        }
    }
    public class BooleanValidationAttribute : ValidationAttribute
    {
        bool _requiredValue;
        public BooleanValidationAttribute(bool requiredValue)
        {
            this._requiredValue = requiredValue;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if ((bool)value != _requiredValue)
                return new ValidationResult("Value must be " + _requiredValue);

            return null;
        }
    }
    public enum Gender
    {
        Male,Female,AttackHelicopter,Unknown
    }

}