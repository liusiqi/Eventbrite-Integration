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
    public partial class Eventbrite_Redirect : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string test = Request.QueryString["code"];

            Response.Write(test);
        }
    }
}