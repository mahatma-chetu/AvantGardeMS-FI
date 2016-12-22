using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments.Fliters
{
    public class CustomErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
            //Log the error 
            //Redirect page to error
            //filterContext.HttpContext.Server.TransferRequest("~/View/Error.cshtml");
            //Debug.WriteLine("Error Occured");
        }
    }
}