using AGM.Payments.Fliters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments.Controllers
{
    [CustomError(View = "Error")]
    public class TransactionsController : Controller
    {
        // GET: Transactions
        [AdminAuth]
        public ActionResult Index()
        {
            return View();
        }
    }
}