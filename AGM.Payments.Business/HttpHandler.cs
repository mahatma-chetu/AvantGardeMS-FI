using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace AGM.Payments.Business
{
    public static class HttpHandler
    {
        /// <summary>
        /// Used to post the data to specified URL and return the Tuple 
        /// object which consist of response recieved status and data.
        /// </summary>
        /// <param name="data">source data to send on specified URL</param>
        /// <param name="url">source url on which data to be send</param>
        /// <param name="requestMethod">source method to get data</param>
        /// <returns>Tuple object which consist of response recieved status and data.</returns>
        public static Tuple<bool, string> PostData(string url, string requestMethod, string data)
        {
            string responseData = string.Empty;
            bool isResponseRecieved = false;
            try
            {
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(new Uri(url));
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Accept = "application/json";
                HttpWebResponse httpResponse = null;
                if (requestMethod == "POST")
                {
                    httpRequest.Method = requestMethod;
                    httpRequest.ContentLength = byteArray.Length;
                    using (var stream = httpRequest.GetRequestStream())
                    {
                        stream.Write(byteArray, 0, byteArray.Length);
                    }

                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                else if (requestMethod == "GET")
                {
                    //httpRequest.Method = requestMethod;
                    httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                }
                responseData = new StreamReader(httpResponse.GetResponseStream()).ReadToEnd();
                if (responseData.Length > 0)
                {
                    isResponseRecieved = true;
                }
            }
            catch
            {
                isResponseRecieved = false;
            }

            return new Tuple<bool, string>(isResponseRecieved, responseData);
        }

    }
}