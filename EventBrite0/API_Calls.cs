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
            string url = "";
            if (page_number == 1)
                 url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/";
            else
                url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" +  page_number.ToString();
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
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic pagination_events = serializer.DeserializeObject(Json_pagination_events);
            int event_count = pagination_events["pagination"]["object_count"];
            int page_count = pagination_events["pagination"]["page_count"];
            if (event_count == 0)
            {
                Name_Link.Add(new Tuple<string, string>("", ""));
            }
            else
            {
                if (page_count == 1)
                {
                    for (int i = 0; i < event_count; i++)
                    {
                        string event_status = pagination_events["events"][i]["status"];
                        if (event_status == "live")
                        {
                            string event_name = pagination_events["events"][i]["name"]["text"];
                            string event_id = pagination_events["events"][i]["id"];
                            string event_link = pagination_events["events"][i]["url"];
                            Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                            count_alive++;
                        }
                    }
                }
                else if (page_count == 2)
                {
                    for (int i = 0; i < 50; i++)
                    {
                        string event_status = pagination_events["events"][i]["status"];
                        if (event_status == "live")
                        {
                            string event_name = pagination_events["events"][i]["name"]["text"];
                            string event_id = pagination_events["events"][i]["id"];
                            string event_link = pagination_events["events"][i]["url"];
                            Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                            count_alive++;
                        }
                    }

                    string Json_Last_Page = User_Events(2);
                    dynamic last_page_events = serializer.DeserializeObject(Json_Last_Page);
                    for (int i = 0; i < event_count/50; i++)
                    {
                        string event_status = last_page_events["events"][i]["status"];
                        if (event_status == "live")
                        {
                            string event_name = last_page_events["events"][i]["name"]["text"];
                            string event_id = last_page_events["events"][i]["id"];
                            string event_link = last_page_events["events"][i]["url"];
                            Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                            count_alive++;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 50; i++)
                    {
                        string event_status = pagination_events["events"][i]["status"];
                        if (event_status == "live")
                        {
                            string event_name = pagination_events["events"][i]["name"]["text"];
                            string event_id = pagination_events["events"][i]["id"];
                            string event_link = pagination_events["events"][i]["url"];
                            Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                            count_alive++;
                        }
                    }

                    for (int page_number = 2; page_number < page_count; page_number++)
                    {
                        string Json_Middle_Pages = User_Events(page_number);
                        dynamic middle_page_events = serializer.DeserializeObject(Json_Middle_Pages);
                        for (int i = 0; i < 50; i++)
                        {
                            string event_status = middle_page_events["events"][i]["status"];
                            if (event_status == "live")
                            {
                                string event_name = middle_page_events["events"][i]["name"]["text"];
                                string event_id = middle_page_events["events"][i]["id"];
                                string event_link = middle_page_events["events"][i]["url"];
                                Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                                count_alive++;
                            }
                        }
                    }

                    string Json_Last_Page = User_Events(2);
                    dynamic last_page_events = serializer.DeserializeObject(Json_Last_Page);
                    for (int i = 0; i < event_count / 50; i++)
                    {
                        string event_status = last_page_events["events"][i]["status"];
                        if (event_status == "live")
                        {
                            string event_name = last_page_events["events"][i]["name"]["text"];
                            string event_id = last_page_events["events"][i]["id"];
                            string event_link = last_page_events["events"][i]["url"];
                            Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                            count_alive++;
                        }
                    }
                }
            }
            return count_alive;
        }
    }
}