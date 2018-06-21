using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BloodDonationUI;
using BloodDonationUI.Models;


namespace BloodDonationUI.Controllers
{
    [Authorize]
    public class PersonalController : Controller
    {
        private BloodDonationEntities bddb = new BloodDonationEntities();
        private ApplicationDbContext iddb = new ApplicationDbContext();

        private void UpdateUserNameInSession()
        {
            var lastName = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).Select(u => u.LastName).FirstOrDefault();
            var firstName = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).Select(u => u.FirstName).FirstOrDefault();

            SessionState.Current.UserName = (lastName != null || firstName != null) ? ((lastName + " " + firstName).Trim(' ')) : (null) ?? SessionState.Current.UserEmail;
        }

        // GET: Personal
        public ActionResult Index()
        {
            var query = bddb.Donor
                .Include(d => d.DonorSex)
                .Include(d => d.ABOBloodGroup)
                .Include(d => d.RhBloodGroup)
                .Where(d => d.Email == SessionState.Current.UserEmail).FirstOrDefault();

            var model = new PersonalShowViewModel
            {
                Email = query.Email,
                LastName = query.LastName,
                FirstName = query.FirstName,
                BirthDate = query.BirthDate.ToShortDateString(),
                DonorSex = query?.DonorSex?.Name,
                SocialInsuranceNumber = query.SocialInsuranceNumber,
                ABOBloodGroup = query?.ABOBloodGroup?.Name,
                RhBloodGroup = query?.RhBloodGroup?.Name
            };

            return View(model);
        }
        // GET: Personal/Edit/
        public ActionResult Edit()
        {
            if (SessionState.Current.UserEmail == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Donor donor = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).FirstOrDefault();

            if (donor == null)
            {
                return HttpNotFound();
            }
            PersonalEditViewModel model = new PersonalEditViewModel
            {
                BirthDate = donor.BirthDate,
                DonorSexId = donor.DonorSexId,
                SocialInsuranceNumber = donor.SocialInsuranceNumber,
                ABOBloodGroupId = donor.ABOBloodGroupId,
                RhBloodGroupId = donor.RhBloodGroupId,
                LastName = donor.LastName,
                FirstName = donor.FirstName
            };

            ViewBag.ABOBloodGroupList = new SelectList(bddb.ABOBloodGroup, "Id", "Name", donor.ABOBloodGroupId);
            ViewBag.DonorSexList = new SelectList(bddb.DonorSex, "Id", "Name", donor.DonorSexId);
            ViewBag.RhBloodGroupList = new SelectList(bddb.RhBloodGroup, "Id", "Name", donor.RhBloodGroupId);

            return View(model);
        }

        // POST: Personal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(PersonalEditViewModel editedDonor)
        {
            var donor = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).FirstOrDefault();

            if (ModelState.IsValid)
            {
                using (var transaction = bddb.Database.BeginTransaction())
                {
                    donor.BirthDate = editedDonor.BirthDate;
                    donor.DonorSexId = editedDonor.DonorSexId;
                    donor.SocialInsuranceNumber = editedDonor.SocialInsuranceNumber;
                    donor.ABOBloodGroupId = editedDonor.ABOBloodGroupId;
                    donor.RhBloodGroupId = editedDonor.RhBloodGroupId;
                    donor.LastName = editedDonor.LastName;
                    donor.FirstName = editedDonor.FirstName;

                    bddb.Entry(donor).State = EntityState.Modified;
                    await bddb.SaveChangesAsync();
                    transaction.Commit();

                    UpdateUserNameInSession();

                    return RedirectToAction("Index");
                }
            }
            ViewBag.ABOBloodGroupId = new SelectList(bddb.ABOBloodGroup, "Id", "Name", donor.ABOBloodGroupId);
            ViewBag.DonorSexId = new SelectList(bddb.DonorSex, "Id", "Name", donor.DonorSexId);
            ViewBag.RhBloodGroupId = new SelectList(bddb.RhBloodGroup, "Id", "Name", donor.RhBloodGroupId);
            return View(donor);
        }

        //// GET: Personal/Delete/5 - action not used, as some personal data are obligatory for business logic
        //[HttpGet]
        //public ActionResult Delete()
        //{
        //    if (SessionState.Current.UserEmail == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Donor donor = bddb.Donor.Where(d => d.Email == SessionState.Current.UserEmail).FirstOrDefault();
        //    if (donor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    using (var transaction = bddb.Database.BeginTransaction())
        //    {
        //        donor.BirthDate = null;
        //        donor.DonorSexId = null;
        //        donor.SocialInsuranceNumber = null;
        //        donor.ABOBloodGroupId = null;
        //        donor.RhBloodGroupId = null;
        //        donor.LastName = null;
        //        donor.FirstName = null;

        //        bddb.Entry(donor).State = EntityState.Modified;
        //        bddb.SaveChanges();
        //        transaction.Commit();

        //        UpdateUserNameInSession();
        //    }

        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                bddb.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
