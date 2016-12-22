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
    public class PaymentController : Controller
    {
        // GET: Payment
        [PayAuth]
        public ActionResult Index(Account accountModel)
        {
            var account = AccountHandler.GetAccount(accountModel.PropertyID, accountModel.BillingAccountID);

            //copy value of each property to model
            var model = account.ConvertTo<PaymentViewModel>();
            if (account != null && account.Status == AppStaticValues.Current)
            {
                List<KeyValue> months = new List<KeyValue>();
                var mo = AppSettingValues.Months();
                foreach (var m in mo)
                {
                    months.Add(new KeyValue { Value = m.Key, Name = m.Value });
                }
                model.Months = months; 
                model.Years = AppSettingValues.Years();
                model.Amount = decimal.Parse(accountModel.Balance.ToString());
                return View(model);
            }
            else
                return View();
        }

        [HttpPost]
        [PayAuth]
        public ActionResult Index(PaymentViewModel paymentViewModel)
        {
            Account acc = new Account();
            ////Get the auth.net account associated with this property
            if (ModelState.IsValid)
            {
                ////Proceed to payment using this auth account
                string cardExipery = paymentViewModel.ExpiryMonth.PadLeft(2,'0') + paymentViewModel.ExpiryYear.Substring(2);
                var payment = AuthPayment.MakePayment(paymentViewModel.PropertyID, paymentViewModel.CardNumber, cardExipery, paymentViewModel.CardVerificationValue, paymentViewModel.Amount,"Charge");
                if (!string.IsNullOrEmpty(payment.Item2) && payment.Item2 != "0")
                {
                    ////Payment succeed 
                    ////Todo Resman api hit for completion of payment for employee on behalf of resident
                    //acc = paymentViewModel.ConvertTo<Account>();
                    //acc.Status = payment.Item1;
                    TempData["Message"] = payment.Item1;
                    ResmanSession resman = Session["Resman"] as ResmanSession;
                    
                    return RedirectToAction("PaymentConfirmation", "Payment", new { viewName= resman.ViewName });
                }
                else
                {
                    acc = paymentViewModel.ConvertTo<Account>();
                    //acc.Status = payment.Item1;
                    TempData["Account"] = acc;
                    TempData["Message"] = payment.Item1;
                    return RedirectToAction("PaymentFail", "Payment");
                    ////Payment failed return to page of payment fail
                }
            }
            else
            {
                acc = paymentViewModel.ConvertTo<Account>();
                return RedirectToAction("Index","Payment",acc);
            }

        }
        [PayAuth]
        public ActionResult PaymentConfirmation(string viewName)
        { 
                       
            return View("PaymentConfirmation", masterName: viewName);
        }
        [PayAuth]
        public ActionResult PaymentFail(string viewName)
        {
            return View("PaymentFail", masterName: viewName);
        }
    }
}