using AGM.Payments.Model;
using AGM.Payments.Model.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace AGM.Payments.Business
{
    public static class AccountHandler
    {
        public static Account GetAccount(string propertyID, string personID)
        {
            return GetAccounts(propertyID).Where(x => x.BillingAccountID == personID).FirstOrDefault();
        }

        public static List<Account> GetAccounts(string propertyID)
        {
            string dataFrom = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd h:mm tt");
            StringBuilder request = new StringBuilder();
            request.Append("IntegrationPartnerID=" + AppSettingValues.IntegrationPartnerID);
            request.Append("&ApiKey=" + AppSettingValues.ApiKey);
            request.Append("&AccountId=" + AppSettingValues.AccountID);
            request.Append("&PropertyId=" + propertyID);
            request.Append("&ModifiedSince=" + dataFrom);
            request.Append("&IncludeTransactions=" + "true");
            //string data = string.Format(@"IntegrationPartnerID={0}&ApiKey={1}&AccountId={2}&PropertyId={3}&ModifiedSince={4}&IncludeTransactions=true", AppSettingValues.IntegrationPartnerID, AppSettingValues.ApiKey, AppSettingValues.AccountID, propertyID, dataFrom);
            var response = HttpHandler.PostData(AppSettingValues.Accounts, "POST", request.ToString());
            var Json = new System.Web.Script.Serialization.JavaScriptSerializer();
            ResmanResponse resmanResponse = Json.Deserialize<ResmanResponse>(response.Item2);
            return resmanResponse.Accounts;
        }

        public static List<AuthAccount> GetAuthAccounts()
        {
            List<AuthAccount> authAccounts = new List<AuthAccount>();
            Data.AvantGrandeMSEntities avant = new Data.AvantGrandeMSEntities();
            var authAccountMapping = (from account in avant.AuthorizeAccounts
                     join property in avant.PaymentToProperties
                     on account.AuthAccountID equals property.AuthAccountID
                     into payments
                     select payments).ToList();
            authAccountMapping.ForEach(x =>
            {
                x.ToList().ForEach(y =>
                {
                    authAccounts.Add(new AuthAccount
                    {
                        AuthAccountName = y.AuthorizeAccount.Name,
                        ApiID = y.AuthorizeAccount.ApiID,
                        ApiKey = y.AuthorizeAccount.ApiKey,
                        AuthAccountID=y.AuthAccountID,
                        PropertyID = y.PropertyID,
                    });
                });
            });
            //authAccounts.Add(new AuthAccount { AuthAccountName = "Giridhar Test Auth.net", ApiID = "8k4gSGmh57L4", ApiKey = "5Pwvrq95j3Jp2Q64", PropertyID = "1e2fdae5-12b0-4aa4-af09-74875e4df4bf", PropertyName = "AVANT-GARDE MARKETING SOLUTIONS" });
            return authAccounts;
        }


        public static List<AuthorizeDataModel> AuthAccounts()
        {
            List<AuthorizeDataModel> authorizeDataModels = new List<AuthorizeDataModel>();
            Data.AvantGrandeMSEntities avant = new Data.AvantGrandeMSEntities();
            var accounts = avant.AuthorizeAccounts.ToList();
            accounts.ForEach(x =>
            {
                authorizeDataModels.Add(
                    new AuthorizeDataModel
                    {
                        AccountName = x.Name,
                        ApiID = x.ApiID,
                        ApiKey = x.ApiKey,
                        AuthAccountID = x.AuthAccountID,
                        Active = (bool)x.Active,
                        Owner = x.CreateBy
                    });
            });
            return authorizeDataModels.Where(x => x.Active == true).ToList();
        }

        public static int AddUpdateAccount(AuthorizeDataModel authorizeDataModel)
        {
            Data.AvantGrandeMSEntities avant = new Data.AvantGrandeMSEntities();
            Data.AuthorizeAccount auth = new Data.AuthorizeAccount();

            if (authorizeDataModel.AuthAccountID == 0)
            {
                FillAuthFromDataModel(authorizeDataModel, auth);
                avant.AuthorizeAccounts.Add(auth);
            }
            else
            {
                auth = avant.AuthorizeAccounts.Where(x => x.AuthAccountID == authorizeDataModel.AuthAccountID).FirstOrDefault();

                FillAuthFromDataModel(authorizeDataModel, auth);
            }
            avant.SaveChanges();
            return auth.AuthAccountID;
        }

        private static void FillAuthFromDataModel(AuthorizeDataModel authorizeDataModel, Data.AuthorizeAccount auth)
        {
            auth.Name = authorizeDataModel.AccountName;
            auth.ApiID = authorizeDataModel.ApiID;
            auth.ApiKey = authorizeDataModel.ApiKey;
            auth.CreateBy = authorizeDataModel.Owner;
            auth.Active = authorizeDataModel.Active;
            auth.CreateOn = DateTime.Now;
            if (authorizeDataModel.AuthAccountID > 0)
            {
                auth.ModifiedBy = authorizeDataModel.Owner;
                auth.ModifiedOn = DateTime.Now;
            }
            auth.AuthAccountID = auth.AuthAccountID;
        }
    }
}