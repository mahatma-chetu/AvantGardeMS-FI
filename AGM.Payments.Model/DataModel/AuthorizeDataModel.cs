using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model.DataModel
{
    public class AuthorizeDataModel
    {
        public int AuthAccountID { get; set; }
        public string AccountName { get; set; }
        public string ApiID { get; set; }
        public string ApiKey { get; set; }
        public bool Active{ get; set; }
        public string Owner { get; set; }
        
    }
    public class AuthPropertyPermission
    {
        public int AuthAccountID { get; set; }
        public string PropertyID { get; set; }
    }
}
