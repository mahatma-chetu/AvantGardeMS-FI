using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model.DataModel
{
    public class ProfileDataModel
    {
        public int ProfileID { get; set; }        
        public string Name { get; set; }        
        public string Email { get; set; }   
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
