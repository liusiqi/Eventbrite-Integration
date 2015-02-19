using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Script.Serialization;

namespace EventBrite0
{
    public partial class Parse_Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //code=THE_USERS_AUTH_CODE&client_secret=YOUR_CLIENT_SECRET&client_id=YOUR_API_KEY&grant_type=authorization_code
            string THE_USERS_AUTH_CODE = Request.QueryString["code"];
            const string YOUR_CLIENT_SECRET = "Y3YA5HJD3EM6UX364MKPRQUCH7NDL2RSBIHG3PS3UCM3MYCB6M";
            const string YOUR_API_KEY = "YHASIDSTEN277KD7LK";
            //string test = Request.QueryString["code"];
            /* http://developer.eventbrite.com/docs/auth/
             * You must then exchange this access code for an OAuth token. Send a POST request to::

                https://www.eventbrite.com/oauth/token
                This POST must contain the following urlencoded data, along with a “Content-type: application/x-www-form-urlencoded“ header::

                code=THE_USERS_AUTH_CODE&client_secret=YOUR_CLIENT_SECRET&client_id=YOUR_API_KEY&grant_type=authorization_code
                The subsequent “POST“ will contain the user’s access_token.
             */
            const string AccessTokenRequest = "https://www.eventbrite.com/oauth/token";
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                request = (HttpWebRequest)WebRequest.Create(AccessTokenRequest);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string UrlEncodedData = "";

                UrlEncodedData = "code=" + HttpUtility.UrlEncode(THE_USERS_AUTH_CODE) + "&";
                UrlEncodedData += "client_secret=" + HttpUtility.UrlEncode(YOUR_CLIENT_SECRET) + "&";
                UrlEncodedData += "client_id=" + HttpUtility.UrlEncode(YOUR_API_KEY) + "&";
                UrlEncodedData += "grant_type=" + HttpUtility.UrlEncode("authorization_code");

                request.ContentLength = UrlEncodedData.Length;

                using (StreamWriter RequestStream = new StreamWriter(request.GetRequestStream()))
                {
                    RequestStream.Write(UrlEncodedData);
                    RequestStream.Close();
                }

                response = (HttpWebResponse)request.GetResponse();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                string PostResponse = "";
                using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                {
                    PostResponse = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                response.Close();
                //Response.Write(PostResponse);
                dynamic json = serializer.DeserializeObject(PostResponse);
                string AccessToken = json["access_token"];
                Response.Write(AccessToken);
            }
            catch
            {
                string ErrorMessage = "Error : retrieving long term access token failed. ";
                Response.Write(ErrorMessage);
            }
        }
    }
}