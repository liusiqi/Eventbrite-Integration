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

        public string User_ID()
        {
            string userID = "";
            string url = "https://www.eventbriteapi.com/v3/users/me/";
            try
            {       
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                //request.Timeout = 200000;
                request.ContentType = "application/json; charset=UTF-8";
                request.Headers.Add("Authorization", "Bearer " + AccessToken);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream streamResponse = response.GetResponseStream();

                //Read Body of Response
                StreamReader sReader = new StreamReader(streamResponse);
                string userInfo = sReader.ReadToEnd();

                sReader.Close();
                response.Close();

                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic json = serializer.DeserializeObject(userInfo);
                userID = json["id"];
            }
            catch(WebException e)
            {
                string ErrorMessage = "Error : retrieving user ID failed. " + e;
                return ErrorMessage;
            }
            return userID ;
        }

        public string User_Events(string userID)
        {
            string url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/";
            string Pagination_Events = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "application/json; charset=UTF-8";
                request.Headers.Add("Authorization", "Bearer " + AccessToken);

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream streamResponse = response.GetResponseStream();

                StreamReader sReader = new StreamReader(streamResponse);
                string userInfo = sReader.ReadToEnd();

                sReader.Close();
                response.Close();

                Pagination_Events = userInfo;
                return Pagination_Events;
            }
            catch(WebException e)
            {
                string ErrorMessage = "Error : retrieving Pagination and Events failed. " + e;
                return ErrorMessage;
            }
        }

        public void Create_Dictionaries(string Json_pagination_events, Dictionary<string, string> Name_Link, Dictionary<string, string> Name_Draft, Dictionary<string, string> Name_Ignore)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic pagination_events = serializer.DeserializeObject(Json_pagination_events);
            int event_count = pagination_events["pagination"]["object_count"];
            int page_count = pagination_events["pagination"]["page_count"];
            if (event_count == 0)
            {
                Name_Link.Add("None", "");
                Name_Draft.Add("None", "");
                Name_Ignore.Add("None", "");
            }
            else
            {
                if (page_count == 1)
                {
                    for (int i = 0; i < event_count; i++)
                    {
                        string event_date = pagination_events["events"][i]["end"]["utc"];
                        string event_ymd = event_date.Substring(0, 9);
                        DateTime saveUtcNow = DateTime.UtcNow;
                        string today_date = saveUtcNow.ToString(@"yyyy-mm-dd");
                        // if old, store in ignore, else -> name_link
                    }
                }
            }
        }
    }
}