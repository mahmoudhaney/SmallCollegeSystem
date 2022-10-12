using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace CollegeSystem.Models
{
    public class Student
    {
        public int Id { get; set; }

        [DisplayName("Full Name")]
        [Required(ErrorMessage = "Full name is required.")]
        [MinLength(5, ErrorMessage = "Full name mustn't be less than 5 Characters")]
        [MaxLength(50, ErrorMessage = "Full name mustn't exceed 50 Characters")]
        public string FullName { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [NotMapped]
        [Compare("Email", ErrorMessage = "Email and confirm email don't match")]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Password and confirm Password don't match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [DisplayName("Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DisplayName("Created At")]
        [ValidateNever]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Last Update At")]
        [ValidateNever]
        public DateTime LastUpdateAt { get; set; }

        [DisplayName("Specialization")]
        [Range(1, int.MaxValue, ErrorMessage = "Choose a valid Specialization")]
        public int SpecializationId { get; set; }

        // Navigation Property
        [ValidateNever]
        public Specialization Specialization { get; set; }

        [DisplayName("Iamge")]
        [ValidateNever]
        public string ImageUrl { get; set; }        
    }
}
