using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class ResidentListViewModel
    {
        public ResidentListViewModel()
        {
            Accounts = new List<Account>();
            Residents = new List<Resident>();
        }
        public List<Account> Accounts { get; set; }
        public string PropertyName { get; set; }
        public List<Resident> Residents { get; set; }
    }

    public class EmployeeProperties
    {
        public EmployeeProperties()
        {
            Properties = new List<Option>();
        }
        public List<Option> Properties { get; set; }
    }
}