using AGM.PaymentProcessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AGM.Payments.Business
{
    public class AuthPayment
    {
        public static Tuple<string,string> MakePayment(string propertyID,string cardNumber, string expiry,string cvv, decimal amount, string transactionType)
        {
            var authAccount = AccountHandler.GetAuthAccounts().Where(x => x.PropertyID == propertyID).First();
            //string transactionType = "Charge";
            TransactionType typeOfTransaction = (TransactionType)Enum.Parse(typeof(TransactionType), transactionType);
            ICardPaymentProcessor payment = Processor.CreatePaymentProcessor();
           
            
            TranRequest tranRequest = new TranRequest { CardNumber = cardNumber, CardExpiry = expiry, Amount = amount, TransactionType = typeOfTransaction, TransactionID = "0",CardVerificationValue=cvv };
            ////Save the transaction request before proceeding
            ////Todo Save Request code
            var tranResponse = payment.MakePayment(tranRequest, authAccount.ApiID,authAccount.ApiKey);
            ////Save response
            ////Todo Save Response Code
            ////Return the response
            string message = string.IsNullOrEmpty(tranResponse.Description) ? tranResponse.TransactionMessage : tranResponse.Description;
            return new Tuple<string, string>(message, tranResponse.TransactionID);

        }

        private static int SaveTransactionRequest(TranRequest tranRequest)
        {
            return 0;
        }

        private static int SaveTransactionResponse(TranResponse tranResponse)
        {
            return 0;
        }
    }
}