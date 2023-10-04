using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SurveyPulse.Shared.IdentityModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [MinLength(5, ErrorMessage = "Must be between 5 and 256 characters")]
        [MaxLength(256, ErrorMessage = "Must be between 5 and 256 characters")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MinLength(5, ErrorMessage = "Must be between 5 and 256 characters")]
        [MaxLength(256, ErrorMessage = "Must be between 5 and 256 characters")]
        public string Username { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [MinLength(3, ErrorMessage = "Must be between 3 and 256 characters")]
        [MaxLength(256, ErrorMessage = "Must be between 5 and 255 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [MinLength(3, ErrorMessage = "Must be between 3 and 256 characters")]
        [MaxLength(256)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Must be between 8 and 256 characters")]
        [MaxLength(256, ErrorMessage = "Must be between 8 and 256 characters")]
        [RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).+$", ErrorMessage="Password must contain atleast 1 number, 1 letter, and 1 special character.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password confirmation is required")]
        //[DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match")]
        public string ConfirmPassword { get; set; }
    }
}
