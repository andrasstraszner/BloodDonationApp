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
    [Authorize]
    public class DonationController : Controller
    {
        //db entities
        private BloodDonationEntities bddb = new BloodDonationEntities();
        private ApplicationDbContext iddb = new ApplicationDbContext();

        //business logic constants
        private const int donationLimitMale = 5;
        private const int donationLimitFemale = 4;
        private const int donationInterval = 56;
        
        //controller helper methods

        /// <summary>
        /// creates list of blood donation centers 
        /// from db table
        /// for use in create type views
        /// places list in viewbag
        /// </summary>
        private void AddToViewBag_DonationCenterList()
        {
            var query = bddb.DonationCenter
                          .Include(dc => dc.DonationCenterType)
                          .Select(dc => new { Text = dc.Name + " " + dc.DonationCenterType.Name, Value = dc.Id });

            ViewBag.DonationCenterList = query.ToList();
        }

        //validation methods

        /// <summary>
        /// validates model for create type post actions
        /// adds model errors
        /// TODO: check validation need for all model properties
        /// </summary>
        /// <param name="model"></param>
        private void ValidateDonationLogEntry(DonationCreateViewModel model)
        {
            if (model.DonationDate > DateTime.Now)
                ModelState.AddModelError(nameof(model.DonationDate), 
                    "Véradás nem rögzíthető jövőbeli dátummal.");

            var query = bddb.DonationLog
                .Include(dl => dl.Donor)
                .Where(dl => dl.Donor.Email == SessionState.Current.UserEmail);

            if (query.Count() != 0)
            {
                var latestNextDate = query
                        .Select(dl => dl.NextDate)
                        .Max();

                if (latestNextDate > model.DonationDate)
                    ModelState.AddModelError(nameof(model.DonationDate), 
                        "Véradás nem rögzíthető az előző várakozási időszak végét megelőző dátummal.");
            }
        }

        /// <summary>
        /// validate manually input date of next donation 
        /// add model error
        /// </summary>
        /// <param name="model"></param>
        private void ValidateManualNextDate(DonationCreateViewModel model)
        {
            if (model.NextDate < model.DonationDate)
            {
                ModelState.AddModelError(nameof(model.NextDate), 
                    "A következő véradás időpontja nem lehet korábbi a jelenlegi véradás időpontjánál.");
            }
            if (model.NextDate < DateTime.Now)
            {
                ModelState.AddModelError(nameof(model.NextDate), 
                    "A következő véradás időpontja nem lehet múltbeli időpont.");
            }
        }

        //business logic methods

        /// <summary>
        /// calculate earliest date of next donation from this donation's date, donor's sex and donation history
        /// used only if current donation is successful; else manual entry of next date is requierd
        /// </summary>
        /// <param name="model"></param>
        /// <param name="donor"></param>
        private void CalculateNextDonationDate(DonationCreateViewModel model, Donor donor)
        {
            //get number of donor's successful donations for actual year
            var countOfThisYearsSuccessfulDonations = bddb.DonationLog
                .Where(l => l.DonorId == donor.Id && l.IsSuccessful && l.DonationDate.Year == DateTime.Now.Year).Count();

            //check if yearly donation limit is reached, depending on sex
            //if so, return next date as the more distant of 56 day limit and next year's start
            //else return date 56 days after current donation date

            if ((donor.DonorSexId == 1 && countOfThisYearsSuccessfulDonations < donationLimitMale) || 
                (donor.DonorSexId == 2  && countOfThisYearsSuccessfulDonations < donationLimitFemale))
            {
                model.NextDate = model.DonationDate.AddDays(donationInterval);
            }
            else
            {
                var newYearStart = new DateTime(DateTime.Now.Year + 1, 1, 1);
                model.NextDate = (model.DonationDate.AddDays(donationInterval) > newYearStart ?
                    model.DonationDate.AddDays(donationInterval) : newYearStart);
            }
        }

        // GET: Donation
        public ActionResult Index()
        {
            var model = new DonationShowFilterViewModel();
            return View(model);
        }

        //GET: partial table view on Index
        public PartialViewResult Index_DonationLogTable()
        {
            var query = bddb.DonationLog
                            .Include(dl => dl.Donor)
                            .Include(dl => dl.DonationCenter)
                            .Include(dl => dl.DonationCenter.DonationCenterType)
                            .Where(dl => dl.Donor.Email == SessionState.Current.UserEmail);

            var model = new DonationShowTableViewModel
            {
                Header = new DonationShowTableRowViewModel(),
                Rows = query.Select(q => new DonationShowTableRowViewModel
                {
                    Id = q.Id,
                    DonationDate = q.DonationDate,
                    DonationCenter = q.DonationCenter.Name + " " + q.DonationCenter.DonationCenterType.Name,
                    IsSuccessful = q.IsSuccessful ? "&#10004;" : "&#10008;",
                    NextDate = q.NextDate
                }).ToList()
            };

            return PartialView("_DonationLogTable", model);
        }

        //POST: partial table view on Index
        [HttpPost]
        public PartialViewResult Index_DonationLogTable_Post(DonationShowFilterViewModel model)
        {
            var query = bddb.DonationLog
                .Include(dl => dl.Donor)
                .Include(dl => dl.DonationCenter)
                .Include(dl => dl.DonationCenter.DonationCenterType)
                .Include(dl => dl.DonationCenter.Address)
                .Where(dl => dl.Donor.Email == SessionState.Current.UserEmail);

            if (model.MinDate != DateTime.MinValue)
            {
                query = query.Where(dl => dl.DonationDate >= model.MinDate);
            }
            if (model.MaxDate != DateTime.MinValue)
            {
                query = query.Where(dl => dl.DonationDate <= model.MaxDate);
            }
            if (model.DonationCenter != null)
            {
                query = query.Where(dl => (dl.DonationCenter.Name + (" ") + dl.DonationCenter.DonationCenterType.Name + (" ") + dl.DonationCenter.Address.City).ToLower().Contains(model.DonationCenter.ToLower()));
            }
            if (model.IsSuccessful != null)
            {
                query = query.Where(dl => dl.IsSuccessful == model.IsSuccessful);
            }
            if (model.IsNextDatePassed != null)
            {
                if ((bool)model.IsNextDatePassed)
                {
                    query = query.Where(dl => dl.NextDate <= DateTime.Now);
                }
                else
                {
                    query = query.Where(dl => dl.NextDate >= DateTime.Now);
                };
            }

            var DonationLogTable = new DonationShowTableViewModel
            {
                Header = new DonationShowTableRowViewModel(),
                Rows = query.Select(q => new DonationShowTableRowViewModel
                {
                    Id = q.Id,
                    DonationDate = q.DonationDate,
                    DonationCenter = q.DonationCenter.Name + " " + q.DonationCenter.DonationCenterType.Name,
                    IsSuccessful = q.IsSuccessful ? "&#10004;" : "&#100008;",
                    NextDate = q.NextDate
                }).ToList()
            };
            return PartialView("_DonationLogTable", DonationLogTable);
        }

        // GET: Donation/Details/5
        public ActionResult Details(int id)
        {
            var donationLogEntry = bddb.DonationLog.Find(id);

            var model = new DonationDetailsViewModel
            {
                DonationDate = donationLogEntry.DonationDate,
                DonationCenter = donationLogEntry.DonationCenter.Name + " " + donationLogEntry.DonationCenter.DonationCenterType.Name,
                IsSuccessful = donationLogEntry.IsSuccessful ? "&#10004;" : "&#10008;",
                NextDate = donationLogEntry.NextDate,
                Comments = donationLogEntry.Comments
            };

            return PartialView("_Details", model);
        }

        // GET: Donation/Create
        public PartialViewResult Create()
        {
            var model = new DonationCreateViewModel();

            AddToViewBag_DonationCenterList();

            return PartialView("_Create", model);
        }

        // POST: Donation/Create
        [HttpPost]
        public ActionResult Create(DonationCreateViewModel newData)
        {
            ValidateDonationLogEntry(newData);

            var donor = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (newData.IsSuccessful)
                {
                    CalculateNextDonationDate(newData, donor);
                }
                else
                {
                    if (newData.NextDate == DateTime.MinValue)
                    {
                        return PartialView("_Create_AddNextDate", newData);
                    }

                    ValidateManualNextDate(newData);

                    if (!ModelState.IsValid)
                    {
                        return PartialView("_Create_AddNextDate", newData);
                    }
                }

                var newDonationLogEntry = new DonationLog
                {
                    DonorId = donor.Id,
                    DonationCenterId = newData.DonationCenterId,
                    DonationDate = newData.DonationDate,
                    IsSuccessful = newData.IsSuccessful,
                    NextDate = newData.NextDate,
                    Comments = newData.Comments
                };

                using (var transaction = bddb.Database.BeginTransaction())
                {
                    bddb.DonationLog.Add(newDonationLogEntry);
                    bddb.SaveChanges();
                    transaction.Commit();
                    return Json(new { success = true });
                }
            }

            AddToViewBag_DonationCenterList();

            return PartialView("_Create", newData);
        }

        // GET: Donation/Edit/5
        public ActionResult Edit(int id)
        {
            var donationLogEntry = bddb.DonationLog.Find(id);

            var dataToModify = new DonationCreateViewModel
            {
                DonationDate = donationLogEntry.DonationDate,
                DonationCenterId = donationLogEntry.DonationCenterId,
                IsSuccessful = donationLogEntry.IsSuccessful,
                Comments = donationLogEntry.Comments
            };

            AddToViewBag_DonationCenterList();

            return PartialView("_Edit", dataToModify);
        }

        // POST: Donation/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DonationCreateViewModel modifiedData)
        {
            ValidateDonationLogEntry(modifiedData);

            var donationLogEntry = bddb.DonationLog.Find(id);

            if (ModelState.IsValid)
            {

                using (var transaction = bddb.Database.BeginTransaction())
                {
                    donationLogEntry.DonationCenterId = modifiedData.DonationCenterId;
                    donationLogEntry.DonationDate = modifiedData.DonationDate;
                    donationLogEntry.IsSuccessful = modifiedData.IsSuccessful;
                    //TODO add nextdate logic
                    donationLogEntry.NextDate = modifiedData.DonationDate.AddDays(56);
                    donationLogEntry.Comments = modifiedData.Comments;

                    bddb.Entry(donationLogEntry).State = EntityState.Modified;
                    bddb.SaveChanges();
                    transaction.Commit();

                    return Json(new { success = true });
                }
            }

            AddToViewBag_DonationCenterList();

            return PartialView("_Edit", modifiedData);
        }

        // GET: Donation/Delete/5
        public ActionResult Delete(int id)
        {
            var donationLogEntry = bddb.DonationLog.Find(id);

            var dataToDelete = new DonationDetailsViewModel
            {
                DonationDate = donationLogEntry.DonationDate,
                DonationCenter = donationLogEntry.DonationCenter.Name + " " + donationLogEntry.DonationCenter.DonationCenterType.Name,
                IsSuccessful = donationLogEntry.IsSuccessful ? "&#10004;" : "&#10008;",
                NextDate = donationLogEntry.NextDate,
                Comments = donationLogEntry.Comments
            };

            return PartialView("_Delete", dataToDelete);
        }

        // POST: Donation/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var donationLogEntry = bddb.DonationLog.Find(id);

            try
            {
                bddb.DonationLog.Remove(donationLogEntry);
                bddb.SaveChanges();

                return Json(new { success = true });
            }
            catch
            {
                var dataToDelete = new DonationDetailsViewModel
                {
                    DonationDate = donationLogEntry.DonationDate,
                    DonationCenter = donationLogEntry.DonationCenter.Name + " " + donationLogEntry.DonationCenter.DonationCenterType.Name,
                    IsSuccessful = donationLogEntry.IsSuccessful ? "&#10004;" : "&#10008;",
                    NextDate = donationLogEntry.NextDate,
                    Comments = donationLogEntry.Comments
                };
                return PartialView("_Delete", dataToDelete);
            }
        }
    }
}
