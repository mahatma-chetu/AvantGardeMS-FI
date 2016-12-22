using AGM.Payments.Fliters;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AGM.Payments
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new CustomErrorAttribute());
        }
    }
}
