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
    public class API_Calls
    {
        private string AccessToken;
        string userID = "";
        string url = "";
        dynamic page_events = null;
        string current_page_info = "";

        public API_Calls(string accesstoken)
        {
            AccessToken = accesstoken;
        }

        public void Get_User_ID()
        {
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
                //return ErrorMessage;
            }
            //return userID ;
        }

        public string User_Events(int page_number)
        {
<<<<<<< HEAD
            string url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" +  page_number.ToString();
=======
            //string url = "";
            //if (page_number == 1)
            //     url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/";
            //else
             url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" +  page_number.ToString();
>>>>>>> 939a26878b4bc8b3cc3898d5b3b5c3ee2e45b880
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

        public int Create_Dictionaries(string Json_pagination_events, List<Tuple<string, string>> Name_Link)//, Dictionary<string, string> Name_Draft, Dictionary<string, string> Name_Ignore)
        {
            int count_alive = 0;
            int current_page = 0;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic pagination_events = serializer.DeserializeObject(Json_pagination_events);
            int event_count = pagination_events["pagination"]["object_count"];
            int page_count = pagination_events["pagination"]["page_count"];
            if (event_count == 0)
            {
                //Name_Link.Add(new Tuple<string, string>("", ""));
                return count_alive;
            }
            else
            {
                for (int events = 0; events < event_count; events++)
                {
                    if (events % 50 == 0)
                    {
                        current_page++; 
                        string current_page_info = User_Events(current_page);
                        page_events = serializer.DeserializeObject(current_page_info);
                    }

                    int event_index = events % 50;
                    string event_status = page_events["events"][event_index]["status"];
                    if (event_status == "live")
                    {
                        string event_name = page_events["events"][event_index]["name"]["text"];
                        string event_link = page_events["events"][event_index]["url"];
                        Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                        count_alive++;
                    }
                }
            }
            return count_alive;
        }
    }
}