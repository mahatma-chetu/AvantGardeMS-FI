using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments.Fliters
{
    public class EmployeeAuth : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            
            HttpSessionStateBase httpSession = filterContext.HttpContext.Session;
            var resman = httpSession["Resman"] as ResmanSession;
            if (resman != null && resman.EmployeeUser != null)
            {
                resman.IsAuthorized = true;
                httpSession["Resman"] = resman;
            }
            else
            {
                resman = new ResmanSession();
                resman.IsAuthorized = false;
                httpSession["Resman"] = resman;
                filterContext.HttpContext.Server.TransferRequest("~/Home/Index");
            }            
        }
    }
}