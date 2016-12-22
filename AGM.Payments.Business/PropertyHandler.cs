using AGM.Payments.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AGM.Payments.Business
{
    public class PropertyHandler
    {
        public static ResmanResponse GetProperties()
        {
            StringBuilder request = new StringBuilder();
            request.Append("IntegrationPartnerID=");
            request.Append(AppSettingValues.IntegrationPartnerID);
            request.Append("&ApiKey=");
            request.Append(AppSettingValues.ApiKey);
            request.Append("&AccountId=");
            request.Append(AppSettingValues.AccountID);
            //string data = string.Format("IntegrationPartnerID={0}&ApiKey={1}&AccountId={2}", intergrationPartnerId, apiKey, accountId);
            var response = HttpHandler.PostData(AppSettingValues.Properties, "POST", request.ToString());
            var Json = new System.Web.Script.Serialization.JavaScriptSerializer();
            ResmanResponse resmanResponse = Json.Deserialize<ResmanResponse>(response.Item2);         

            return resmanResponse;
        }
    }
}