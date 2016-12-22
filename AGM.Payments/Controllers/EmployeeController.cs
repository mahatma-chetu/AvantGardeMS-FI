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
    public class EmployeeController : Controller
    {
        // GET: Employee
        [EmployeeAuth]
        public ActionResult Index()
        {
            ResmanSession resman = Session["Resman"] as ResmanSession;
            EmployeeProperties empProp = new EmployeeProperties();
            Session["UserName"] = resman.EmployeeUser.Name;
            empProp.Properties = resman.EmployeeUser.Properties;
            return View(empProp);
        }

        [EmployeeAuth]
        [HttpGet()]
        public JsonResult List(string propertyID)
        {
            ResidentListViewModel model = new ResidentListViewModel();
            ResmanSession resman = Session["Resman"] as ResmanSession;
            //model.PropertyName = resman.EmployeeUser.Properties.Where(x => x.Value == propertyID).FirstOrDefault().DisplayText;
            model.Accounts = AccountHandler.GetAccounts(propertyID);

            //model.Accounts = accounts.Where(x => x.Status == "Current").ToList();     
            var jsonResult = new
            {
                data = model.Accounts.ToList()
            };       
            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }
    }
}