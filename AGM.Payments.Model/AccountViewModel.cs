using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class AccountViewModel
    {
        public string BillingAccountID { get; set; }
        public string PersonID { get; set; }
        public string AccountType { get; set; }
        public string PropertyID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Unit { get; set; }
        public string Building { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string MoveInDate { get; set; }
        public string LeaseStartDate { get; set; }
        public string LeaseEndDate { get; set; }
        public int Balance { get; set; }
        public string PaymentStatus { get; set; }
        public string Status { get; set; }
    }
}