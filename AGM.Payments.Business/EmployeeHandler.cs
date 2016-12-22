using AGM.Payments.Model.DataModel;
using AGM.Payments.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGM.Payments.Business
{
    public static class EmployeeHandler
    {        
        public static EmployeeDataModel ValidateEmployee(string email, string password)
        {
            EmployeeDataModel emp = new EmployeeDataModel();
            emp= ResmanHandler.Employees().Where(x => x.Email == email && x.Password == password).FirstOrDefault();
            return emp;
        }
    }
}