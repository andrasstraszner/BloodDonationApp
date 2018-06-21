using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonationUI
{
    [MetadataType(typeof(Metadata))]
    public partial class Donor
    {
        private sealed class Metadata
        {

            [Display(Name = "Id")]
            public int Id { get; set; }

            [Display(Name = "Email")]
            [EmailAddress]
            [MaxLength(100, ErrorMessage = "Az {0} maximális hossza {1} karakter lehet.")]
            public string Email { get; set; }

            [Display(Name = "Születési dátum")]
            [DataType(DataType.Date)]
            [Required]
            public System.DateTime BirthDate { get; set; }

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
            [MaxLength(50, ErrorMessage = "A {0} maximális hossza {1} karakter lehet.")]
            [Required]
            public string LastName { get; set; }

            [Display(Name = "Keresztnév")]
            [MaxLength(50, ErrorMessage = "A {0} maximális hossza {1} karakter lehet.")]
            [Required]
            public string FirstName { get; set; }
        }
    }
}