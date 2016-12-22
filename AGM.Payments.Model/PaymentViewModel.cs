using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AGM.Payments.Model
{
    public class PaymentViewModel
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
        public List<KeyValue> Months { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Credit card number must be numeric")]
        [DataType(DataType.CreditCard,ErrorMessage ="Enter valid credit card number.")]
        [MaxLength(16,ErrorMessage ="Card length must be 16 digit.")]
        [MinLength(16,ErrorMessage = "Card length must be 16 digit")]
        [Required(ErrorMessage = "Card number is required.")]
        public string CardNumber { get; set; }
        [Required(ErrorMessage = "Card expiry month is required.")]
        public string ExpiryMonth { get; set; }
        [Required(ErrorMessage = "Card expiry year is required.")]
        public string ExpiryYear { get; set; }
        [RegularExpression("^[0-9]*$", ErrorMessage = "Card code must be numeric")]
        [Required(ErrorMessage = "Card code is required.")]
        [DataType(DataType.Password)]        
        public string CardVerificationValue { get; set; }
        [Required(ErrorMessage = "Amount is required.")]
        public decimal Amount { get; set; }
        public List<string> Years { get; set; }
    }
    public class KeyValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class TransactionModel
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string TransactionCategoryID { get; set; }
        public string Description { get; set; }
        public string Reference { get; set; }
        public float Amount { get; set; }
    }
}