using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventBrite0
{
    public partial class Parse_Access : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //code=THE_USERS_AUTH_CODE&client_secret=YOUR_CLIENT_SECRET&client_id=YOUR_API_KEY&grant_type=authorization_code
            string THE_USERS_AUTH_CODE = Request.QueryString["code"];
            string YOUR_CLIENT_SECRET = "Y3YA5HJD3EM6UX364MKPRQUCH7NDL2RSBIHG3PS3UCM3MYCB6M";
            string YOUR_API_KEY = "YHASIDSTEN277KD7LK";
            string authorization_code = "";
            //string test = Request.QueryString["code"];
            /* http://developer.eventbrite.com/docs/auth/
             * You must then exchange this access code for an OAuth token. Send a POST request to::

                https://www.eventbrite.com/oauth/token
                This POST must contain the following urlencoded data, along with a “Content-type: application/x-www-form-urlencoded“ header::

                code=THE_USERS_AUTH_CODE&client_secret=YOUR_CLIENT_SECRET&client_id=YOUR_API_KEY&grant_type=authorization_code
                The subsequent “POST“ will contain the user’s access_token.
             */
            string longTermUrl = "https://www.eventbrite.com/oauth/token";
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                //create the request
                request = (HttpWebRequest)WebRequest.Create(longTermUrl);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";

                string postFields = "";

                postFields = "code=" + HttpUtility.UrlEncode(THE_USERS_AUTH_CODE) + "&";
                postFields += "client_secret=" + HttpUtility.UrlEncode(YOUR_CLIENT_SECRET) + "&";
                postFields += "client_id=" + HttpUtility.UrlEncode(YOUR_API_KEY) + "&";
                postFields += "grant_type=" + HttpUtility.UrlEncode("authorization_code");

                //Get lenght of postfields and set to Content Length
                request.ContentLength = postFields.Length;

                //Post data is sent as a stream
                StreamWriter sWriter = null;
                sWriter = new StreamWriter(request.GetRequestStream());

                //Add 'postfields' to request
                sWriter.Write(postFields);
                sWriter.Close();
                response = (HttpWebResponse)request.GetResponse();
                string post_response = "";
                using (StreamReader responseStream = new StreamReader(response.GetResponseStream()))
                {
                    post_response = responseStream.ReadToEnd();
                    responseStream.Close();
                }

                //Close response stream
                response.Close();
                Response.Write(post_response);
            }
            catch
            {
                string[] p = new string[2];
                p[0] = "error";
                p[1] = "Error Message (retrieving long term access token): ";
                Response.Write(p);
            }
        }
    }
}