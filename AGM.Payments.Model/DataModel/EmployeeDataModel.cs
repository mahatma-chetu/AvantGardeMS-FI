using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model.DataModel
{
    public class EmployeeDataModel
    {
        public EmployeeDataModel()
        {
            Properties = new List<Option>();
        }
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public List<Option> Properties { get; set; }
    }
}
