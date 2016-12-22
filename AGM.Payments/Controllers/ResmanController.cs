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
    public class ResmanController : Controller
    {
        
        // GET: Resman
        [AdminAuth]
        public ActionResult Index()
        {
            ResmanViewModel resmanViewModel = new ResmanViewModel();
            resmanViewModel.IntegrationPartnerID = AppSettingValues.IntegrationPartnerID;
            resmanViewModel.ApiKey = AppSettingValues.ApiKey;
            resmanViewModel.AccountID = AppSettingValues.AccountID;
            ResmanSession resman = Session["Resman"] as ResmanSession;
            Session["UserName"] = resman.AdminUser.UserName;
            return View(resmanViewModel);
        }

        [HttpPost]
        [AdminAuth]
        public ActionResult Index(ResmanViewModel resmanViewModel)
        {
            //sharbo
            if (ModelState.IsValid)
            {
                ResmanSession resmanSession = new ResmanSession();
                resmanSession.AccountID = resmanViewModel.AccountID;
                resmanSession.ApiKey = resmanViewModel.ApiKey;
                resmanSession.IntegrationPartnerID = resmanViewModel.IntegrationPartnerID;
                resmanSession.Subdomain = resmanViewModel.AccountID;
                Session["Resman"] = resmanSession;
                TempData["Message"] = "Resman configuration done!";
                return RedirectToActionPermanent("Index", "Config");
            }
            return View();
        }

        [AdminAuth]
        public ActionResult EmployeeConfiguration()
        {
            EmployeeViewModel employee = new EmployeeViewModel();

            //test 
            //employee.Employee.Properties.Add(new Option { DisplayText = "Demo", Value = "d1234", Checked = false });
            ResmanHandler.Employees().ForEach(x =>
            {
                employee.Employees.Add(new Employee
                {
                    EmployeeID = x.EmployeeID,
                    Name = x.Name,
                    Email = x.Email,
                    Password = x.Password,
                    Phone = x.Phone,
                    Address = x.Address,
                    Properties = x.Properties
                }
              );
            });
            return View(employee);
        }




        [HttpPost]
        [AdminAuth]
        public ActionResult SaveEmployee(Employee employeeViewModel)
        {
            if (ModelState.IsValid)
            {
                Model.DataModel.EmployeeDataModel emp = new Model.DataModel.EmployeeDataModel();
                emp.Name = employeeViewModel.Name;
                emp.Email = employeeViewModel.Email;
                emp.Password = employeeViewModel.Password;
                emp.Address = employeeViewModel.Address;
                emp.EmployeeID = employeeViewModel.EmployeeID;
                emp.Active = true;
                emp.Phone = employeeViewModel.Phone;
                employeeViewModel.Properties.ForEach(x =>
                {
                    if (x.Checked)
                    {
                        emp.Properties.Add(new Option { DisplayText = x.DisplayText, Value = x.Value });
                    }
                });
                int empID = ResmanHandler.AddUpdateEmployee(emp);
                if (empID > 0)
                {
                    TempData["Message"] = "Employee information successfully saved.";
                }
                else
                {
                    TempData["Message"] = "Fail to save the employee information.";
                }
                return RedirectToAction("EmployeeConfiguration", "Resman");
            }
            else
            {
                ModelState.AddModelError("", "Please check the validations.");
                return RedirectToAction("EmployeeConfiguration", "Resman");
            }


        }
        [AdminAuth]
        public PartialViewResult CreateEmployee(int employeeID)
        {
            Employee emp = new Employee();
            if (employeeID == 0)
            {
                var properties = PropertyHandler.GetProperties().Properties;
                properties.ForEach(x =>
                {
                    emp.Properties.Add(new Option { DisplayText = x.Name, Value = x.PropertyID, Checked = false });
                });
            }
            else
            {
                Model.DataModel.EmployeeDataModel editEmp = ResmanHandler.Employees().Where(x => x.EmployeeID == employeeID).FirstOrDefault();
                emp.Name = editEmp.Name;
                emp.Email = editEmp.Email;
                emp.Password = editEmp.Password;
                emp.Address = editEmp.Address;
                emp.Phone = editEmp.Phone;
                emp.Properties = editEmp.Properties;
                emp.EmployeeID = editEmp.EmployeeID;
            }

            return PartialView(emp);
        }
        [AdminAuth]
        public void DeleteEmployee(int employeeID)
        {
            Model.DataModel.EmployeeDataModel editEmp = ResmanHandler.Employees().Where(x => x.EmployeeID == employeeID).FirstOrDefault();
            editEmp.Active = false;
            int empID = ResmanHandler.AddUpdateEmployee(editEmp);

            if (empID > 0)
            {
                TempData["Message"] = "Employee record sucessfully removed.";
            }
            else
            {
                TempData["Message"] = "Error while removing the record of Employee.";
            }
            RedirectToAction("EmployeeConfiguration", "Resman");
        }
        [AdminAuth]
        public ActionResult PropertyToPayment()
        {
            PaymentToPropertiesViewModel pay = FillPaymentToPropertiesView();
            return View(pay);
        }
        [AdminAuth]
        private PaymentToPropertiesViewModel FillPaymentToPropertiesView()
        {
            PaymentToPropertiesViewModel pay = new PaymentToPropertiesViewModel();
            ResmanHandler.PaymentToProperties().ForEach(x =>
            {
                pay.PaymentsToProperties.Add(
                    new PaymentToProperties
                    {
                        AuthAccountID = x.AuthAccountID,
                        AuthAccountName = x.AuthAccountName,
                        PaymentID = x.PaymentID,
                        PropertyID = x.PropertyID,
                        PropertyName = x.PropertyName
                    });
            });
            return pay;
        }
        [AdminAuth]
        public PartialViewResult CreateEditPayment(int paymentID)
        {
            PaymentToPropertiesViewModel pay = new PaymentToPropertiesViewModel();
            var existingPaymentMap = FillPaymentToPropertiesView().PaymentsToProperties;
            var authAccounts = AccountHandler.AuthAccounts();
            var properties = PropertyHandler.GetProperties().Properties;
            authAccounts.ForEach(x =>
            {
                pay.AuthAccounts.Add(new Option { Value = x.AuthAccountID.ToString(), DisplayText = x.AccountName });
            });

            properties.ForEach(x =>
            {
                pay.Properties.Add(new Option { Value = x.PropertyID.ToString(), DisplayText = x.Name });
            });

            if (paymentID > 0)
            {
                var edit = ResmanHandler.PaymentToProperties().Where(x => x.PaymentID == paymentID).FirstOrDefault();
                pay.PaymentID = edit.PaymentID;
                pay.PropertyID = edit.PropertyID;
                pay.AuthAccountID = edit.AuthAccountID;
            }
            return PartialView(pay);
        }
        [AdminAuth]
        public ActionResult SavePayment(PaymentToPropertiesViewModel payment)
        {
            if (ModelState.IsValid)
            {
                Model.DataModel.PaymentToPropertyDataModel payData = new Model.DataModel.PaymentToPropertyDataModel();
                payData.PaymentID = payment.PaymentID;
                payData.AuthAccountID = payment.AuthAccountID;
                payData.PropertyID = payment.PropertyID;

                int paymentID = ResmanHandler.AddUpdatePaymentToProperty(payData);
                if (paymentID > 0)
                {
                    TempData["Message"] = "Payment to property information successfully saved.";
                }
                else
                {
                    if (paymentID == -1)
                    {
                        TempData["Message"] = "This payment or property is already mapped.";
                    }
                    else
                    {
                        TempData["Message"] = "Fail to save the Payment to property information.";
                    }
                }
                return RedirectToAction("PropertyToPayment", "Resman");
            }
            else
            {
                TempData["Message"] = "Fail to save the Payment to property information.";
                return RedirectToAction("PropertyToPayment", "Resman");
            }
        }
    }
}