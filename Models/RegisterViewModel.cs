using System;
using System.ComponentModel.DataAnnotations;
namespace Exam.Models
{
    public class RegisterViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string First_name { get; set; }

        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Last_name { get; set; }
 
        [Required]
        [EmailAddress]
        public string Email { get; set; }
 
        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and confirmation must match.")]
        public string Confirm_password { get; set; }
    }
    public sealed class DateStartAttribute : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            DateTime dateStart = (DateTime)value;
            // Date must start in the future time.
            return (dateStart < DateTime.Now);
        }
    }

}