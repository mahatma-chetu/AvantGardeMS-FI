using AuthorizeNet.Api.Contracts.V1;
using AuthorizeNet.Api.Controllers;
using AuthorizeNet.Api.Controllers.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGM.PaymentProcessor
{
    public class CardPaymentProcessor : ICardPaymentProcessor
    {
        public TranResponse MakePayment(TranRequest transactionRequest,string apiID, string apiKey)
        {
            TranResponse transactionResponse = null;
            switch (transactionRequest.TransactionType)
            {
                case TransactionType.Charge:
                    transactionResponse = ProcessSale(apiID, apiKey, transactionRequest.CardNumber, transactionRequest.CardExpiry,transactionRequest.CardVerificationValue, transactionRequest.Amount);
                    break;
                case TransactionType.Void:
                    transactionResponse = ProcessVoid(apiID, apiKey, transactionRequest.CardNumber, transactionRequest.CardExpiry, transactionRequest.TransactionID);
                    break;
                case TransactionType.Refund:
                    transactionResponse =ProcessRefund(apiID, apiKey, transactionRequest.CardNumber, transactionRequest.CardExpiry, transactionRequest.TransactionID, transactionRequest.Amount);
                    break;
                case TransactionType.Auth:
                    transactionResponse = ProcessAuthOnly(apiID, apiKey, transactionRequest.CardNumber, transactionRequest.CardExpiry, transactionRequest.Amount);
                    break;
                case TransactionType.CaptureOnly:
                    transactionResponse = ProcessCaptureOnly(apiID, apiKey, transactionRequest.Amount, transactionRequest.TransactionID);
                    break;
            }
            return transactionResponse;
        }
        private TranResponse ProcessVoid(string ApiLoginID, string ApiTransactionKey, string cardNumber, string cardExpiry, string transactionID)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = cardExpiry
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.voidTransaction.ToString(),    // refund type
                payment = paymentType,
                refTransId = transactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            return CreateResponse(response);
        }
        private TranResponse ProcessAuthOnly(string ApiLoginID, string ApiTransactionKey, string cardNumber, string cardExpiry, decimal amount)
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = cardExpiry
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authOnlyTransaction.ToString(),    // authorize only
                amount = amount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            return CreateResponse(response);

        }
        private TranResponse ProcessSale(string ApiLoginID, string ApiTransactionKey, string cardNumber, string cardExpiry,string cardCvv, decimal amount)
        {

            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = cardExpiry,
                cardCode=cardCvv
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // authorize only
                amount = amount,
                payment = paymentType
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();

            return CreateResponse(response);

        }
        private TranResponse ProcessRefund(string ApiLoginID, string ApiTransactionKey, string cardNumber, string cardExpiry, string transactionID, decimal amount)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };

            var creditCard = new creditCardType
            {
                cardNumber = cardNumber,
                expirationDate = cardExpiry
            };

            //standard api call to retrieve response
            var paymentType = new paymentType { Item = creditCard };

            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.refundTransaction.ToString(),    // refund type
                payment = paymentType,
                amount = amount ,
                refTransId = transactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            return CreateResponse(response);
        }
        private TranResponse ProcessCaptureOnly(string ApiLoginID, string ApiTransactionKey, decimal amount,string transactionID)
        {
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;

            // define the merchant information (authentication / transaction id)
            ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey
            };


            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.priorAuthCaptureTransaction.ToString(),    // capture prior only
                amount = amount,
                refTransId = transactionID
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };

            // instantiate the contoller that will call the service
            var controller = new createTransactionController(request);
            controller.Execute();

            // get the response from the service (errors contained if any)
            var response = controller.GetApiResponse();
            return CreateResponse(response);
        }
        private TranResponse CreateResponse(createTransactionResponse response)
        {
            TranResponse transactionResponse = new TranResponse();
            //validate
            if (response != null)
            {
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        transactionResponse.TransactionID = response.transactionResponse.transId;
                        transactionResponse.ResponseCode = response.transactionResponse.responseCode;
                        transactionResponse.MessageCode = response.transactionResponse.messages[0].code;
                        transactionResponse.Description = response.transactionResponse.messages[0].description;
                        transactionResponse.AuthCode = response.transactionResponse.authCode;
                    }
                    else
                    {
                        transactionResponse.TransactionMessage = "Failed Transaction.";

                        if (response.transactionResponse != null && response.transactionResponse.errors != null)
                        {
                            transactionResponse.ResponseCode = response.transactionResponse.errors[0].errorCode;
                            transactionResponse.TransactionMessage = response.transactionResponse.errors[0].errorText;
                        }
                    }
                }
                else
                {
                    transactionResponse.TransactionMessage = "Failed Transaction.";

                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        transactionResponse.ResponseCode = response.transactionResponse.errors[0].errorCode;
                        transactionResponse.TransactionMessage = response.transactionResponse.errors[0].errorText;
                    }
                    else
                    {
                        transactionResponse.ResponseCode = response.messages.message[0].code;
                        transactionResponse.TransactionMessage = response.messages.message[0].text;
                    }
                }
            }
            else
            {
                transactionResponse.TransactionMessage = "Null Response.";
            }
            return transactionResponse;
        }
    }
}
