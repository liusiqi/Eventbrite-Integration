using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EventBrite0
{
    public partial class main : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //API_Methods Eventbrite_Object = null;
            try
            {
                API_Methods Eventbrite_Object = new API_Methods("5Z5FE6HF3BJ763FOJFOQ");
                string userID = Eventbrite_Object.User_ID();
                //Response.Write(userID + "\n\n");
                string Json_pagination_events = Eventbrite_Object.User_Events(userID);
                Response.Write(Json_pagination_events);
                Dictionary<string, string> Name_Link = new Dictionary<string, string>();
                Dictionary<string, string> Name_Draft = new Dictionary<string, string>();
                Dictionary<string, string> Name_Ignore = new Dictionary<string, string>();
                //Response.Write(pagination_events);

                //Eventbrite_Object.Create_Dictionaries(Json_pagination_events, Name_Link, Name_Draft, Name_Ignore);
            }
            catch (WebException me)
            {
                string ErrorMessage = "Error : retrieving alive event list failed. " + me;
                Response.Write(ErrorMessage);
            }
        }
    }
}