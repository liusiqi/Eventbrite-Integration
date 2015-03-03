using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventBrite0
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // set up the request url for access code, this could be built up by using query string or string builder.
            // 1. This is the request url using query string:
            string FirstHandShake = "https://www.eventbrite.com/oauth/authorize?response_type=code&client_id=YHASIDSTEN277KD7LK";
            
            // 2. Here is using string builder.
            //const string BaseURL = "https://www.eventbrite.com";
            //const string Resource = "/oauth/authorize";
            //const string QueryString = "?response_type=code&client_id=YHASIDSTEN277KD7LK";
            //StringBuilder builder = new StringBuilder();
            //builder.Append(BaseURL).Append(Resource).Append(QueryString);

            // An article about using redirect: http://blogs.msdn.com/b/tmarq/archive/2009/06/25/correct-use-of-system-web-httpresponse-redirect.aspx

            //The following line clears the last exception that was thrown. https://msdn.microsoft.com/en-us/library/system.web.httpserverutility.clearerror(v=vs.110).aspx
            Server.ClearError();
            
            // The folloing line response the url to 3rd party server.
            Response.Redirect(FirstHandShake);
            
            // If you don't want to use Server.ClearError(), the use false as the 2nd parameter for Redirect method is better.
            //Response.Redirect(builder.ToString(), false);

            // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            // Relative links: https://msdn.microsoft.com/en-us/library/system.web.httpapplication.completerequest(v=vs.110).aspx
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}