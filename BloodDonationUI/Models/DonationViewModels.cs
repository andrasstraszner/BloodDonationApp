using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BloodDonationUI.Models
{
    public class DonationShowViewModel
    {
        public DonationShowViewModel()
        {
            Filter = new DonationShowFilterViewModel();
            Table = new DonationShowTableViewModel();
        }

        public DonationShowFilterViewModel Filter { get; set; }
        public DonationShowTableViewModel Table { get; set; }
    }

    public class DonationShowFilterViewModel
    {
        public DonationShowFilterViewModel()
        {
        }

        [Display(Name = "Időszak kezdete")]
        [DataType(DataType.Date)]
        public DateTime MinDate { get; set; }

        [Display(Name = "Időszak vége")]
        [DataType(DataType.Date)]
        public DateTime MaxDate { get; set; }

        [Display(Name = "Helyszín")]
        public string DonationCenter { get; set; }

        [Display(Name = "Sikeres")]
        public bool? IsSuccessful { get; set; }

        [Display(Name = "Várakozás letelt")]
        public bool? IsNextDatePassed { get; set; }
    }


    public class DonationShowTableViewModel
    {
        public DonationShowTableViewModel()
        {
            Header = new DonationShowTableRowViewModel();
            Rows = new List<DonationShowTableRowViewModel>();
        }

        public DonationShowTableRowViewModel Header { get; set; }
        public IEnumerable<DonationShowTableRowViewModel> Rows { get; set; }
    }

    public class DonationShowTableRowViewModel
    {
        public DonationShowTableRowViewModel()
        {
        }

        [Display(Name = "Azonosító")]
        public int Id { get; set; }

        [Display(Name = "Dátum")]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        [Display(Name = "Helyszín")]
        public string DonationCenter { get; set; }

        [Display(Name = "Sikeres")]
        public string IsSuccessful { get; set; }

        [Display(Name = "Várakozás vége")]
        [DataType(DataType.Date)]
        public DateTime NextDate { get; set; }
    }

    public class DonationDetailsViewModel
    {
        public DonationDetailsViewModel()
        {
        }

        [Display(Name = "Dátum")]
        [DataType(DataType.Date)]
        public DateTime DonationDate { get; set; }

        [Display(Name = "Helyszín")]
        public string DonationCenter { get; set; }

        [Display(Name = "Sikeres")]
        public string IsSuccessful { get; set; }

        [Display(Name = "Várakozás vége")]
        [DataType(DataType.Date)]
        public DateTime NextDate { get; set; }

        [Display(Name = "Megjegyzés")]
        public string Comments { get; set; }
    }

    public class DonationCreateViewModel
    {
        public DonationCreateViewModel()
        {
        }

        [Display(Name = "Dátum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime DonationDate { get; set; }

        [Display(Name = "Helyszín")]
        [Required]
        public int DonationCenterId { get; set; }

        [Display(Name = "Sikeres")]
        [Required]
        public bool IsSuccessful { get; set; }

        [Display(Name = "Várakozás vége")]
        [DataType(DataType.Date)]
        public DateTime NextDate { get; set; }

        [Display(Name = "Megjegyzés")]
        public string Comments { get; set; }
    }
}