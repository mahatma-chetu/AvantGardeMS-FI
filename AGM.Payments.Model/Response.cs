using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class ResmanResponse
    {
        public string AccountID { get; set; }
        public string CompanyName { get; set; }
        public string MethodName { get; set; }
        public string Status { get; set; }
        public string ErrorDescription { get; set; }
        public string ModifiedSince { get; set; }
        public bool IncludeTransactions { get; set; }
        public List<Account> Accounts { get; set; }
        public List<Property> Properties { get; set; }
        public List<Resident> Residents { get; set; }
    }
    public class Resident
    {
        public string ParentLeaseGroupID { get; set; }
        public string PersonID { get; set; }
        public string Unit { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string LeaseStartDate { get; set; }
        public string LeaseEndDate { get; set; }
        public string MoveInDate { get; set; }
        public string MoveOutDate { get; set; }
    }
    public class Property
    {
        public string PropertyID { get; set; }
        public string Name { get; set; }        
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }        
    }
    public class Account
    {
        public string BillingAccountID { get; set; }
        public string PersonID { get; set; }
        public string AccountType { get; set; }
        public string PropertyID { get; set; }
        public string PropertyName { get; set; }
        public string FirstName { get; set; }
        public object MiddleName { get; set; }
        public string LastName { get; set; }
        public string Unit { get; set; }
        public string Building { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public object HomePhone { get; set; }
        public string MoveInDate { get; set; }
        public object MoveOutDate { get; set; }
        public string LeaseStartDate { get; set; }
        public string LeaseEndDate { get; set; }
        public string Status { get; set; }
        public float Balance { get; set; }
        public string PaymentStatus { get; set; }
        public List<object> Transactions { get; set; }
    }
}