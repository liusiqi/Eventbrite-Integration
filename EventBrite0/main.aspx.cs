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
                API_Calls Eventbrite_Object = new API_Calls("5Z5FE6HF3BJ763FOJFOQ");
                //string userID = Eventbrite_Object.User_ID();
                Eventbrite_Object.Get_User_ID();
                //Response.Write(userID + "\n\n");
                string Json_pagination_events = Eventbrite_Object.User_Events(1);
                //Response.Write(Json_pagination_events);
                //Dictionary<string, Tuple<string, string>> Name_Link = new Dictionary<string, Tuple<string, string>>();
                List<Tuple<string, string>> Name_Link = new List<Tuple<string,string>>();
                //Dictionary<string, string> Name_Draft = new Dictionary<string, string>();
                //Dictionary<string, string> Name_Ignore = new Dictionary<string, string>();

                int count_alive = Eventbrite_Object.Create_Dictionaries(Json_pagination_events, Name_Link);

                foreach (var item in Name_Link)
                {
                    Response.Write(item.Item1 + " : " + item.Item2);
                    Response.Write(Environment.NewLine);
                }

                Response.Write(count_alive);
            }
            catch (WebException me)
            {
                string ErrorMessage = "Error : retrieving alive event list failed. " + me;
                Response.Write(ErrorMessage);
            }
        }
    }
}