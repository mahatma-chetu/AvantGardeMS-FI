
using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class LoginViewModel
    {
        public string UserType { get; set; }        
        public string PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string ResidentEmail { get; set; }
        public string EmployeeEmail { get; set; }
        public string EmployeePassword { get; set; }
        public string Unit { get; set; }
        public string BuildingNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<Property> Properties { get; set; }
    }
}