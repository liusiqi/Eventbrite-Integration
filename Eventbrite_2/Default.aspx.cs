using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;
using System.Text;

namespace Eventbrite_2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string BaseURL = "https://www.eventbrite.com";
            string Resource = "/oauth/authorize";
            string QueryString = "?response_type=code&client_id=YHASIDSTEN277KD7LK";
            StringBuilder builder = new StringBuilder();
            builder.Append(BaseURL).Append(Resource).Append(QueryString);
            Server.ClearError();
            Response.Redirect(builder.ToString(),false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}