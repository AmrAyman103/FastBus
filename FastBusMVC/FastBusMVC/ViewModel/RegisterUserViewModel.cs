using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FastBusMVC.Validation;

namespace FastBusMVC.ViewModel
{
    public enum Gender
    {
        M, // Male
        F  // Female
    }

    public class RegisterUserViewModel
    {

        [Required(ErrorMessage = "Please,enter the UserName")]
        [MaxLength(15, ErrorMessage = "Please,the UserName must not contain more than or equal 15 characters")]
        [MinLength(4, ErrorMessage = "Please,the UserName must not contain less than 10 characters")]
        [Remote("CheckUserNameUnique", "Account", ErrorMessage = "UserName already exists, please select another name!!.")]
        public string UserName { get; set; }
        [Remote("CheckEmailUnique", "Account", ErrorMessage = "email already exists, please select another name!!.")]
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@#$%^&+=]).{8,}$", ErrorMessage = "Password must contain at least 8 characters including at least one uppercase letter, one lowercase letter, one digit, and one special character.")]

        public string Password { get; set; }

        //[Required]
        //[Compare("Password",ErrorMessage = "Password confirmation does not match!.")]
        //public string ConfirmPassword { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Address must be at least 12 characters.")]
        [Required(ErrorMessage = "The Address field is required.")]
        public string Address { get; set;}

        [Required(ErrorMessage = "The FirstName field is required.")]
        [MaxLength(8, ErrorMessage = "The FirstName field must be a maximum of 8 characters.")]
        [MinLength(3, ErrorMessage = "The FirstName field must be a minimum of 3 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The LastName field is required.")]
        [MaxLength(8, ErrorMessage = "The LastName field must be a maximum of 8 characters.")]
        [MinLength(3, ErrorMessage = "The LastName field must be a minimum of 3 characters.")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Please enter your date of birth")]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Please select your gender")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Please enter your phone number")]
        [RegularExpression(@"^01(0)?[1-9]\d{8}$", ErrorMessage = "Invalid Egyptian phone number")]

        [Remote("CheckPhoneUnique", "Account", ErrorMessage = "Phone number already exists, please select another name!!.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please,upload the UserImage")]
        [Display(Name = "UserImage")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new[] { ".png", ".jpg", ".jpeg" })]
        public IFormFile image { get; set; }
    }
}
