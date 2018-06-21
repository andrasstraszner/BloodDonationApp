using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationUI.Models
{

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [Display(Name = "Maradjon bejelentkezve")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        public RegisterViewModel_Account AccountInfo { get; set; }
        public Donor PersonalInfo { get; set; }

        public RegisterViewModel()
        {
            AccountInfo = new RegisterViewModel_Account();
            PersonalInfo = new Donor();
        }
    }

    public class RegisterViewModel_Account
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} legalább {2} karakter hosszú kell legyen.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Jelszó megerősítése")]
        [Compare("Password", ErrorMessage = "A két megadott jelszó nem egyezik.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}
