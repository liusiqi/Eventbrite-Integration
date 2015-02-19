using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace EventBrite0
{
    public class API_Methods
    {
        private string AccessToken;

        public API_Methods(string accesstoken)
        {
            AccessToken = accesstoken;
        }

        public string event_search()
        {
            string retVal = "";
            string url = "https://www.eventbriteapi.com//v3/users/110515278343/owned_events/";
            try
            {       
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                //request.Timeout = 200000;
                request.ContentType = "application/json; charset=UTF-8";
                //request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("Authorization", "Bearer " + AccessToken);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream streamResponse = response.GetResponseStream();

                StreamReader sReader = new StreamReader(streamResponse);
                retVal = sReader.ReadToEnd();

                sReader.Close();
                response.Close();
            }
            catch(WebException e)
            {
                string ErrorMessage = "Error : retrieving event search failed. " + e;
                return ErrorMessage;
            }
            return retVal ;
        }
    }
}