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
        const int page_size = 50;

        public API_Calls(string accesstoken)
        {
            // when the object is created, pass the access token to it.
            AccessToken = accesstoken;
        }

        // This method is used to send request and store response into Json string
        // The request method is GET, and the url given as an argument, and the access token is added to the Authorization header.
        public string request_response(string url, string failed_detail)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Timeout = 100000;
                request.Headers.Add("Authorization", "Bearer " + AccessToken);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamResponse = response.GetResponseStream();
                StreamReader sReader = new StreamReader(streamResponse);
                string userInfo = sReader.ReadToEnd();
                sReader.Close();
                response.Close();
                return userInfo;
            }
            catch (WebException e)
            {
                string ErrorMessage = "Error : retrieving " + failed_detail + " failed. " + e;
                return ErrorMessage;
            }
        }

        public void Get_User_ID()
        {
            // for getting the user's event ID, this is the API request: http://developer.eventbrite.com/docs/user-details/
            string url = "https://www.eventbriteapi.com/v3/users/me/";
            string userInfo = request_response(url, "user ID");

            // The user's event id is under the "id" keyword.
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic json = serializer.DeserializeObject(userInfo);
            userID = json["id"];
        }

        public string User_Events(int page_number)
        {
            // To get the user's event info, here is the url: http://developer.eventbrite.com/docs/user-owned-events/
            // if there is no query string following, then it will always return the 1st page info.
             url = "https://www.eventbriteapi.com/v3/users/" + userID + "/owned_events/?page=" +  page_number.ToString();

            // Create a string to store the return Json string.
            string Pagination_Events = request_response(url, "Pagination and Events");
            return Pagination_Events;
        }

        public Tuple<int, int> Create_Lists(string Json_pagination_events, List<Tuple<string, string>> Name_Link, List<Tuple<string, string>> Name_Draft)
        {
            int count_live = 0; // counting the live events
            int count_draft = 0; // counting the draft events
            int current_page = 0; // current page number and ready to be used as the query string ?page = current_page

            // The first page info is given as an argument, decode it and we will see the pagination: how many events and pages we have.
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            dynamic pagination_events = serializer.DeserializeObject(Json_pagination_events);
            int event_count = pagination_events["pagination"]["object_count"]; // return the total event amount.
            int page_count = pagination_events["pagination"]["page_count"]; // return the total page amount.

            if (event_count == 0) // don't really needed if no events.
            {
                //Name_Link.Add(new Tuple<string, string>("", ""));
                return new Tuple<int,int>(count_live, count_draft);
            }
            else
            {
                // Since we already have the first page information, we pass it to a global object page_events with is to be parse for events.
                page_events = serializer.DeserializeObject(Json_pagination_events);

                // iterate through events details. 
                for (int events = 0; events < event_count; events++)
                {
                    if (events % page_size == 0) // event time it hits a new page.
                    {
                        current_page++; // update page number
                        if (page_count > 1 && current_page >= 2) // since we already have the 1st page info, there is no need to resend request for the 1st page. 
                        {
                            string current_page_info = User_Events(current_page); // for pages greater than 1, send the request and store the response 
                            page_events = serializer.DeserializeObject(current_page_info); // update the global object and ready to be parsed
                        } 
                    }

                    int event_index = events % page_size; // the event index in each page.
                    string event_status = page_events["events"][event_index]["status"]; // read event status. 
                    string event_name = page_events["events"][event_index]["name"]["text"]; // read event name.
                    string event_link = page_events["events"][event_index]["url"]; // read event link.

                    if (event_status == "live") // if the event is a live event, store its name and link into live event list.
                    {
                        Name_Link.Add(new Tuple<string, string>(event_name, event_link));
                        count_live++;
                    }
                    else if (event_status == "draft") // if the event is a draft event, store its name and link into draft event list.
                    {
                        Name_Draft.Add(new Tuple<string, string>(event_name, event_link));
                        count_draft++;
                    }
                }
            }
            return new Tuple<int,int>(count_live, count_draft);
        }
    }
}