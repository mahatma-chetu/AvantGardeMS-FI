using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AGM.Payments.Model;
using System.Text;

namespace AGM.Payments.Business
{
    public class ResidentHandler
    {
        public static List<Resident> AllResident(string propertyID)
        {
            StringBuilder request = new StringBuilder();
            request.Append("IntegrationPartnerID=");
            request.Append(AppSettingValues.IntegrationPartnerID);
            request.Append("&ApiKey=");
            request.Append(AppSettingValues.ApiKey);
            request.Append("&AccountId="+AppSettingValues.AccountID);
            request.Append("PropertyID=" + propertyID);
            var response = HttpHandler.PostData(AppSettingValues.CurrentResident, "POST", request.ToString());
            var Json = new System.Web.Script.Serialization.JavaScriptSerializer();

            return Json.Deserialize<ResmanResponse>(response.Item2).Residents;
        }

        public static Resident CurrentResident(string propertyID, string building, string unit,string email)
        {
            StringBuilder parameters = new StringBuilder();
            parameters.Append("IntegrationPartnerID="+ AppSettingValues.IntegrationPartnerID);
            parameters.Append("&ApiKey="+AppSettingValues.ApiKey);
            parameters.Append("&AccountId=" + AppSettingValues.AccountID);
            parameters.Append("&PropertyID=" + propertyID);
            //parameters.Append("&Email=" + email);
            //parameters.Append("&Unit=" + unit);
            var response = HttpHandler.PostData(AppSettingValues.CurrentResident, "POST", parameters.ToString());
            var Json = new System.Web.Script.Serialization.JavaScriptSerializer();
            var data= Json.Deserialize<ResmanResponse>(response.Item2).Residents;
            Resident resident=data.Where(x => x.Email == email && x.Unit == unit).FirstOrDefault();
            return resident;
        }

        public static Tuple<Account,bool> ResidentAccount(string propertyID,string building, string unit, string email, string personID)
        {
            string dataFrom = DateTime.Now.AddYears(-1).ToString("yyyy-MM-dd h:mm tt");
            StringBuilder request = new StringBuilder();
            request.Append("IntegrationPartnerID=" + AppSettingValues.IntegrationPartnerID);
            request.Append("&ApiKey=" + AppSettingValues.ApiKey);
            request.Append("&AccountId=" + AppSettingValues.AccountID);
            request.Append("&PropertyId=" + propertyID);
            request.Append("&ModifiedSince=" + dataFrom);
            request.Append("&IncludeTransactions=" + "true");
            request.Append("&PersonID=" + personID);
            var response = HttpHandler.PostData(AppSettingValues.Accounts, "POST", request.ToString());
            var Json = new System.Web.Script.Serialization.JavaScriptSerializer();
            ResmanResponse resmanResponse = Json.Deserialize<ResmanResponse>(response.Item2);
            var account = resmanResponse.Accounts.Where(x=>x.Building== building && x.Email==email && x.Unit==unit).FirstOrDefault();
            return new Tuple<Account, bool>(account, account == null ? false : true);
        }

        
    }
}