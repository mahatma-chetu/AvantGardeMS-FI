using AGM.Payments.Business;
using AGM.Payments.Fliters;
using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments.Controllers
{
    //[HandleError(View ="Error")]
    [CustomError(View = "Error")]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult SignOut()
        {
            Session.Abandon();
            return RedirectToAction("Index","Home");
        }
        public ActionResult Index()
        {
            LoginViewModel login = new LoginViewModel();
            login.Properties = PropertyHandler.GetProperties().Properties;
            ResmanSession resman = new ResmanSession();
            resman.Properties = login.Properties;
            Session["Resman"] = resman;
            return View(login);
        }
        [HttpPost]
        public ActionResult Index(LoginViewModel loginViewModel)
        {
            ResmanSession resman = new ResmanSession();
            if (ModelState.IsValid)
            {
                if (loginViewModel.UserType == "Resident") //Check for validation
                {
                    var currentResident = ResidentHandler.CurrentResident(loginViewModel.PropertyID, loginViewModel.BuildingNo, loginViewModel.Unit, loginViewModel.ResidentEmail);
                    if (currentResident!=null)
                    {
                        var resident = ResidentHandler.ResidentAccount(loginViewModel.PropertyID, loginViewModel.BuildingNo, loginViewModel.Unit, loginViewModel.ResidentEmail, currentResident.PersonID);
                        if (resident.Item2)
                        {
                            resman.ResidentUser = resident.Item1;
                            resman.IsAuthorized = true;
                            resman.ViewName = "_Common";
                            Session["Resman"] = resman;
                            return RedirectToAction("Index", "Payment", new { BillingAccountID = resident.Item1.BillingAccountID, PropertyID = resident.Item1.PropertyID });
                        }
                        else
                        {
                            TempData["Message"] = "Invalid detail provided, please try again";
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Invalid detail provided, please try again";
                        return RedirectToAction("Index", "Home");
                    }
                    
                }
                else if (loginViewModel.UserType == "Employees") //Check for validation for employee
                {
                    if (ModelState.IsValid)
                    {
                        var emp=EmployeeHandler.ValidateEmployee(loginViewModel.EmployeeEmail, loginViewModel.EmployeePassword);
                        if (emp!=null)
                        {
                            resman.EmployeeUser = emp;
                            resman.IsAuthorized = true;
                            resman.ViewName = "Employee";
                            Session["Resman"] = resman;
                            return RedirectToAction("Index", "Employee");
                        }
                        else
                        {
                            TempData["Message"] = "Invalid detail provided, please try again";
                            return RedirectToAction("Index", "Home");
                        }                        
                    }
                    else
                    {
                        TempData["Message"] = "Invalid detail provided, please try again";
                        return RedirectToAction("Index", "Home");
                    }                    
                }
                else if (loginViewModel.UserType == "Admin") //Check for validation for admin from config values
                {
                    resman.AdminUser = new Model.DataModel.AdminDataModel();
                    resman.AdminUser.UserName = loginViewModel.UserName;
                    resman.AdminUser.Password = loginViewModel.Password;
                    Session["Resman"] = resman;
                    return RedirectToAction("Index", "Resman");
                }                
            }
            return View();
        }
    }
}