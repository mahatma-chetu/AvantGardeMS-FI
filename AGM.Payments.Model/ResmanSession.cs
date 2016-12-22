using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGM.Payments.Model.DataModel;

namespace AGM.Payments.Model
{
    [Serializable]
    public class ResmanSession
    {
        public string AccountID { get; set; }
        public string ApiKey { get; set; }
        public string IntegrationPartnerID { get; set; }
        public string Subdomain { get; set; }
        public List<Property> Properties { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Resident> Residents { get; set; }
        public EmployeeDataModel EmployeeUser { get; set; }
        public Account ResidentUser { get; set; }
        public AdminDataModel AdminUser { get; set; }
        public bool IsAuthorized { get; set; }
        public string ViewName { get; set; }
    }
}