using System;
using System.Collections.Generic;
using System.Linq;
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

            Response.Write(THE_USERS_AUTH_CODE);
        }
    }
}