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
            try
            {
                const int default_first_page = 1; // used to get the first page information from which we know the pagination. 
                API_Calls Eventbrite_Object = new API_Calls("UKBCL3D77A7VP5HIMNXR"); // Hard code access token.
                Eventbrite_Object.Get_User_ID(); // Get the ID from the user who gave authorization. The ID is to be used to get the user's owned events.
                string Json_pagination_events = Eventbrite_Object.User_Events(default_first_page); // Get the first page info of the user.

                // Create 2 lists tuple. 1st list for storing Live events, 2nd list for storing Draft events. The left item of the tuple is the name of the event, and the right of the tuple is the link of the event.
                List<Tuple<string, string>> Name_Live = new List<Tuple<string,string>>();
                List<Tuple<string, string>> Name_Draft = new List<Tuple<string, string>>();

                // After  check information of the user, add events to relative lists.
                Tuple<int, int> counts_Live_Draft = Eventbrite_Object.Create_Lists(Json_pagination_events, Name_Live, Name_Draft);

                // The below is just to showing up Live events and Draft events, and their amounts.
                foreach (var item in Name_Live)
                {
                    Response.Write(item.Item1 + ":" + item.Item2 + ",");
                    Response.Write(Environment.NewLine);
                }
                Response.Write("\nLive event count: " + counts_Live_Draft.Item1.ToString());
                Response.Write(Environment.NewLine);

                foreach (var item in Name_Draft)
                {
                    Response.Write(item.Item1 + ":" + item.Item2 + ",");
                    Response.Write(Environment.NewLine);
                }
                Response.Write("Draft event count: " + counts_Live_Draft.Item2.ToString());
            }
            catch (WebException me)
            {
                string ErrorMessage = "Error : retrieving alive event list failed. " + me;
                Response.Write(ErrorMessage);
            }
        }
    }
}