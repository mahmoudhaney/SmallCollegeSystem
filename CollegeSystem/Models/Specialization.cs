using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CollegeSystem.Models
{
    public class Specialization
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You have to provide a valid name")]
        [MinLength(2, ErrorMessage = "Name mustn't be less than 2 Characters")]
        [MaxLength(20, ErrorMessage = "Name mustn't be exceed 20 Characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You have to provide a valid description")]
        [MinLength(5, ErrorMessage = "Description mustn't be less than 5 Characters")]
        [MaxLength(50, ErrorMessage = "Description mustn't be exceed 50 Characters")]
        public string Description { get; set; }

        // Navigation Property
        [ValidateNever]
        public ICollection<Student> Students { get; set; }
    }
}
