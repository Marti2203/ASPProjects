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

        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(3, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        public string SecretAnswer { get; set; }

        [Display(Name = "Agreement", ResourceType = typeof(UserResources))]
        public bool Agreement { get; set; }

        [Display(Name = "Gender", ResourceType = typeof(UserResources))]
        public Gender Gender { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email")]
        [Display(Name = "Email", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Email{ get; set;}


        [Display(Name ="RetypePassword",ResourceType =typeof(UserResources))]
        [System.ComponentModel.DataAnnotations.Compare("PasswordTest")]
        [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [RegularExpression("^[A-Z].*", ErrorMessageResourceName = "InvalidRegex", ErrorMessageResourceType = typeof(ErrorResources))]
        public string PasswordSecond { get; set; }

        [Display(Name ="Password",ResourceType =typeof(UserResources))]
        [System.ComponentModel.DataAnnotations.Compare("PasswordTest")]
        [MaxLength(20, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [MinLength(6, ErrorMessageResourceName = "MinLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [RegularExpression("^[A-Z].*",ErrorMessageResourceName ="InvalidRegex",ErrorMessageResourceType =typeof(ErrorResources))]
        public string PasswordTest { get; set; }

        [Display(Name = "SecretQuestion", ResourceType = typeof(UserResources))]
        [MaxLength(50, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string SecretQuestion { get; set; }

        [Display(Name ="About",ResourceType =typeof(UserResources))]
        public string About { get; set; }


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
    public enum Gender
    {
        Male,Female,AttackHelicopter,Unknown
    }

}