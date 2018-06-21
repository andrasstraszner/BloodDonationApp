using BloodDonationUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.Entity;


namespace BloodDonationUI.Controllers
{
    public class HomeController : Controller
    {
        private BloodDonationEntities bddb = new BloodDonationEntities();

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                var model = new IndexViewModel();

                var query = bddb.DonationLog
                            .Include(dl => dl.Donor)
                            .Where(dl => dl.Donor.Email == SessionState.Current.UserEmail)
                            .Select(dl => dl.NextDate)
                            .OrderByDescending(dl => dl);

                if (query.Count() == 0)
                {
                    model.DisplayNextDate = "Még nincs rögzített véradási alkalmad.<br/>Mehetsz vért adni!";
                }
                else
                {
                    var latestDate = query.Take(1).FirstOrDefault();
                    if (latestDate < DateTime.Now.AddDays(-1))
                    {
                        model.DisplayNextDate = "A legutóbbi várakozási időszak lejárt " + latestDate.ToShortDateString() + "-n.<br/>Mehetsz vért adni!";
                    }
                    else
                    {
                        model.DisplayNextDate = latestDate.ToShortDateString() + "<br/>Még " + (latestDate - DateTime.Now).Days.ToString() + " nap!";
                    }
                }

                return View(model);
            }
            else
            {
                var model = new IndexViewModel();
                return View(model);
            }
        }
    }
}