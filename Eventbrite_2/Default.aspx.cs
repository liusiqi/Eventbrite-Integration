using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Net;
using System.IO;

namespace Eventbrite_2
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string first_url = "https://www.eventbrite.com/oauth/authorize?response_type=code&client_id=YHASIDSTEN277KD7LK";
            Server.ClearError();
            Response.Redirect(first_url,false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}