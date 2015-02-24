using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventBrite0
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            API_Methods Eventbrite_Object = null;
            try
            {
                Eventbrite_Object = new API_Methods("5Z5FE6HF3BJ763FOJFOQ");
                string userID = Eventbrite_Object.User_ID();
                //Response.Write(userID + "\n\n");

                string events = Eventbrite_Object.User_Events(userID);
                Response.Write(events + "\n");
            }
            catch (WebException me)
            {
                string ErrorMessage = "Error : retrieving long term access token failed. " + me;
                Response.Write(ErrorMessage);
            }
        }
    }
}