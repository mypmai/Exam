using System;
using System.ComponentModel.DataAnnotations;
namespace Exam.Models
{
    public class ProductViewModel
    {
        [Required]
        [MinLength(2)]
        [RegularExpression(@"^[a-zA-Z]+$")]
        public string Name { get; set; }

        [Required]
        [MinLength(8)]
        public string Description{ get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public float Start_Bid { get; set;}
 
        [Required]
        [FutureDate(ErrorMessage="Date must be in the Future")]
        [DataType(DataType.Date)]
        public DateTime End_Date { get; set; }

        

        
    }
    public sealed class FutureDateAttribute : ValidationAttribute
    {        
        public override bool IsValid(object value)
        {
            DateTime FutureDate = (DateTime)value;
            // Date must start in the future time.
            return (FutureDate > DateTime.Now);
        }
    }

}