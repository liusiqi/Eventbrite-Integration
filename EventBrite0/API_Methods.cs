﻿using System;
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

        public int User_Event_Count(string userID)
        {
            string url = "https://www.eventbriteapi.com/v3/users/110515278343/owned_events/";// +userID + "/owned_events/";
            int eventCount = 0;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
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
                eventCount = json["pagination"]["page_size"];
                return eventCount;
            }
            catch(WebException e)
            {
                string ErrorMessage = "Error : retrieving Event Count failed. " + e;
                return 0;
            }
        }
    }
}