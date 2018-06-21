using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonationUI.Models
{
    public class PersonalShowViewModel
    {
        public PersonalShowViewModel()
        {
        }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Vezetéknév")]
        public string LastName { get; set; }

        [Display(Name = "Keresztnév")]
        public string FirstName { get; set; }

        [Display(Name = "Születési dátum")]
        public string BirthDate { get; set; }

        [Display(Name = "Nem")]
        public string DonorSex { get; set; }

        [Display(Name = "TAJ azonosító")]
        public string SocialInsuranceNumber { get; set; }

        [Display(Name = "AB0 vércsoport")]
        public string ABOBloodGroup { get; set; }

        [Display(Name = "Rh vércsoport")]
        public string RhBloodGroup { get; set; }
    }

    public class PersonalEditViewModel
    {
        public PersonalEditViewModel()
        {
        }

        [Display(Name = "Születési dátum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Nem")]
        [Required]
        public int DonorSexId { get; set; }

        [Display(Name = "TAJ azonosító")]
        [RegularExpression("^[0-9]{3}[ ][0-9]{3}[ ][0-9]{3}$", ErrorMessage = "A TAJ azonosítót \"xxx xxx xxx\" formátumban kell megadni, ahol x egy számjegy.")]
        public string SocialInsuranceNumber { get; set; }

        [Display(Name = "AB0 vércsoport")]
        public Nullable<int> ABOBloodGroupId { get; set; }

        [Display(Name = "Rh vércsoport")]
        public Nullable<int> RhBloodGroupId { get; set; }

        [Display(Name = "Vezetéknév")]
        [Required]
        [MaxLength(50, ErrorMessage = "A {0} maximális hossza {1} karakter lehet.")]
        public string LastName { get; set; }

        [Display(Name = "Keresztnév")]
        [Required]
        [MaxLength(50, ErrorMessage = "A {0} maximális hossza {1} karakter lehet.")]
        public string FirstName { get; set; }
    }

}