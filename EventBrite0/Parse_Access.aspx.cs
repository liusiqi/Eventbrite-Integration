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
            API_Calls Eventbrite_Object = null;
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
            //HttpWebRequest request = null; //put inside try catch
            //HttpWebResponse response = null; // put inside try catch

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(AccessTokenRequest);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                string UrlEncodedData  = "code=" + HttpUtility.UrlEncode(THE_USERS_AUTH_CODE) + "&" + "client_secret=" + HttpUtility.UrlEncode(YOUR_CLIENT_SECRET) + "&" + "client_id=" + HttpUtility.UrlEncode(YOUR_API_KEY) + "&" + "grant_type=" + HttpUtility.UrlEncode("authorization_code");
                request.ContentLength = UrlEncodedData.Length;

                using (StreamWriter RequestStream = new StreamWriter(request.GetRequestStream()))
                {
                    RequestStream.Write(UrlEncodedData);
                    RequestStream.Close();
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string PostResponse = "";
                using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                {
                    PostResponse = responseStream.ReadToEnd();
                    responseStream.Close();
                }
                response.Close();
                //Response.Write(PostResponse);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                dynamic json = serializer.DeserializeObject(PostResponse);
                string AccessToken = json["access_token"];
                Response.Write(AccessToken + "\n");

            }
            catch(WebException me)
            {
                string ErrorMessage = "Error : retrieving long term access token failed. " + me;
                Response.Write(ErrorMessage);
            }
        }
    }
}