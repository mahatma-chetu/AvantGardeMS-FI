using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class PaymentToPropertiesViewModel
    {
        public PaymentToPropertiesViewModel()
        {
            Properties = new List<Option>();
            AuthAccounts = new List<Option>();
            PaymentsToProperties = new List<PaymentToProperties>();
           
        }        
        public List<Option> Properties { get; set; }
        public List<Option> AuthAccounts { get; set; }
        public int PaymentID { get; set; }
        [Required(ErrorMessage = "Auth Account is required.")]
        public int AuthAccountID { get; set; }
        [Required(ErrorMessage = "Property is required.")]
        public string PropertyID { get; set; }
        public List<PaymentToProperties> PaymentsToProperties { get; set; }
    }

    public class PaymentToProperties
    {
        public int PaymentID { get; set; }
        public int AuthAccountID { get; set; }
        public string PropertyID { get; set; }
        public string AuthAccountName { get; set; }
        public string PropertyName { get; set; }
    }
    
}