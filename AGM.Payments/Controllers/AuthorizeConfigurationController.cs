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
    [CustomError(View = "Error")]
    public class AuthorizeConfigurationController : Controller
    {
        // GET: AuthorizeConfiguration/Details/5
        [AdminAuth]
        public ActionResult List()
        {
            var a = AccountHandler.AuthAccounts();
            var accounts = new List<AuthAccount>();
            a.ForEach(x =>
            {
                accounts.Add(
                    new AuthAccount
                    {
                        AuthAccountName = x.AccountName,
                        ApiID = x.ApiID,
                        ApiKey = x.ApiKey,
                        AuthAccountID = x.AuthAccountID
                    });
            });
            return View(accounts);
        }

        // GET: AuthorizeConfiguration/Create
        [AdminAuth]
        public ActionResult Create(int id)
        {
            AuthAccount auth = new AuthAccount();
            if (id == 0)
            {

            }
            else
            {
                var edit = AccountHandler.AuthAccounts().Where(x => x.AuthAccountID == id).FirstOrDefault();
                auth.AuthAccountName = edit.AccountName;
                auth.ApiID = edit.ApiID;
                auth.ApiKey = edit.ApiKey;
                auth.AuthAccountID = edit.AuthAccountID;
            }
            return PartialView(auth);
        }

        // POST: AuthorizeConfiguration/Create
        [HttpPost]
        [AdminAuth]
        public ActionResult Create(AuthAccount authAccount)
        {
            if (ModelState.IsValid)
            {
                Model.DataModel.AuthorizeDataModel authData = new Model.DataModel.AuthorizeDataModel();
                authData.AccountName = authAccount.AuthAccountName;
                authData.ApiID = authAccount.ApiID;
                authData.ApiKey = authAccount.ApiKey;
                authData.Active = true;
                authData.Owner = "Admin";
                authData.AuthAccountID = authAccount.AuthAccountID;
                var authID = AccountHandler.AddUpdateAccount(authData);
                if (authID > 0)
                {
                    TempData["Message"] = "Authorize.net configuration saved.";
                }
                else
                {
                    TempData["Message"] = "Unable to save Authorize.net configuration.";
                }
                return RedirectToAction("List", "AuthorizeConfiguration");
            }
            else
            {
                TempData["Message"] = "Unable to save Authorize.net configuration.";
                return RedirectToAction("List", "AuthorizeConfiguration");
            }
        }
        [AdminAuth]
        public void Delete(int authID)
        {
            Model.DataModel.AuthorizeDataModel editEmp = AccountHandler.AuthAccounts().Where(x => x.AuthAccountID == authID).FirstOrDefault();
            editEmp.Active = false;
            editEmp.Owner = "Admin";
            int empID = AccountHandler.AddUpdateAccount(editEmp);
            if (empID > 0)
            {
                TempData["Message"] = "Authorize.Net account sucessfully removed.";
            }
            else
            {
                TempData["Message"] = "Error while removing the Authorize.Net account.";
            }
        }
    }
}
