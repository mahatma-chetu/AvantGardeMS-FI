using AGM.Payments.Business;
using AGM.Payments.Model;
using AGM.Payments.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments.Controllers
{
    public class ProfileController : Controller
    {

        // GET: Profile
        public ActionResult Index(int profileID, string profileType)
        {
            string layout = string.Empty;
            var profile = GetProfile(profileID);
            if (profileType == AppStaticValues.Employee)
            {
                layout = AppStaticValues.Employee;
               
            }
            else if (profileType == AppStaticValues.Admin)
            {
                layout = AppStaticValues.Admin;
            }
            profile.ProfileType = layout;
            return View("Index",layout, profile);
        }
        [HttpPost]
        public ActionResult Index(ProfileViewModel profileViewModel)
        {
            Model.DataModel.EmployeeDataModel emp = new Model.DataModel.EmployeeDataModel();
            emp.Name = profileViewModel.Name;
            emp.Email = profileViewModel.Email;
            emp.Address = profileViewModel.Address;
            emp.EmployeeID = profileViewModel.ProfileID;
            emp.Active = true;
            emp.Phone = profileViewModel.Phone;
            var profileID=ResmanHandler.UpdateProfile(emp);
            if (profileID>0)
            {
                TempData["Message"] = "Profile information updated successfully.";
            }
            else
            {
                TempData["Message"] = "Fail to save the update information.";
            }

            return RedirectToAction("Index","Profile",new {profileID= profileViewModel.ProfileID, profileType= profileViewModel.ProfileType });
        }
        private ProfileViewModel GetProfile(int profileID)
        {
            var profileData = ProfileHandler.Profile(profileID);
            ProfileViewModel profileViewModel = new ProfileViewModel();
            profileViewModel.ProfileID = profileData.ProfileID;
            profileViewModel.Name = profileData.Name;
            profileViewModel.Address = profileData.Address;
            profileViewModel.Email = profileData.Email;
            profileViewModel.Phone = profileData.Phone;
            return profileViewModel;
        }

        public ActionResult ChangePassword()
        {
            return View();
        }
    }
}