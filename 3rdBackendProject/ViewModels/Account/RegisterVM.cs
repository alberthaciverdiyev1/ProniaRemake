using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace _3rdBackendProject.ViewModels
{
    public class RegisterVM
    {
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Name { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string Surname { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        [MinLength(3)]
        [MaxLength(25)]
        public string UserName { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public bool IsMale { get; set; }

        internal void CheckUserInfo(ModelStateDictionary modelState)
        {
            throw new NotImplementedException();
        }
    }
}
