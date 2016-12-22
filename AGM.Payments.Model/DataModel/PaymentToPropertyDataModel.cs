using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.Payments.Model.DataModel
{
    public class PaymentToPropertyDataModel
    {
        public int PaymentID { get; set; }
        public int AuthAccountID { get; set; }
        public string AuthAccountName { get; set; }
        public string PropertyID { get; set; }
        public string PropertyName { get; set; }
    }
}
