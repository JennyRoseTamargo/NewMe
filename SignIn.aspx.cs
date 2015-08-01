using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace ARMS.Security
{
	public partial class SignIn : System.Web.UI.Page 
	{
        bool isActive = true;

		protected void Page_Load(object sender, EventArgs e)
		{
            if (Session["gUserType"] != null)
			{
                if (Session["gUserType"].ToString() == "1") // Admin
                {
                    Response.Redirect("../Resource/Resource.aspx");
                }
                else if (Session["gUserType"].ToString() == "2") // Staff
                {
                    Response.Redirect("../Booking/BookingHistory.aspx");
                }
			}		
		}

        /// <summary>
        /// OnAuthenticate event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void OnAuthenticate(object sender, AuthenticateEventArgs e)
        {
            bool Authenticated = false;
            Authenticated = SiteSpecificAuthenticationMethod(Login1.UserName.Trim(), Login1.Password);
            e.Authenticated = Authenticated;

            //// *** FOR TESTING ***
            //e.Authenticated = false;
            //if (String.Compare(Login1.UserName, "foo", true) == 0 &&
            //    String.Compare(Login1.Password, "bar", false) == 0)
            //{
            //    Session["gUserName"] = Login1.UserName;
            //    e.Authenticated = true;
            //}
        }

		/// <summary>
		/// Verify username and password.
        /// Set global session vars on successful authentication.
		/// </summary>
		/// <param name="UserName"></param>
		/// <param name="Password"></param>
		/// <returns></returns>
		private bool SiteSpecificAuthenticationMethod(string UserName, string Password)
		{
			bool result = false;
			string connStr = ConfigurationManager.ConnectionStrings["DatabaseRMSData1"].ToString();
			SqlConnection dbConn = new SqlConnection(connStr);
			dbConn.Open();

			try
			{
                string query = @"select StaffId, Title, FirstName, LastName, UserType, IsActive from tblStaff 
                                where UserName = '" + UserName + "' and UserPassword = '" + Password + "'";

				SqlCommand cmdIns = new SqlCommand(query, dbConn);
				SqlDataReader rdr = null;
				rdr = cmdIns.ExecuteReader();

				if (rdr.HasRows)
				{
					while (rdr.Read())
					{
						// Store logged in user info to session vars
						Session["gUserName"] = UserName;
						Session["gStaffId"] = rdr["StaffId"].ToString();
                        Session["gFullName"] = rdr["Title"].ToString() + " " + rdr["FirstName"].ToString() + " " + rdr["LastName"].ToString();
                        Session["gUserType"] = rdr["UserType"].ToString();
                        isActive = Convert.ToBoolean(rdr["IsActive"]);
					}
					result = true;
				}
			}
			catch (Exception ex)
			{
				throw new Exception(ex.ToString(), ex);
			}
			finally
			{
				dbConn.Close();
			}

			return result;
		}
				
		/// <summary>
		/// Redirect user to start page once authenticated.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void OnLoggedIn(object sender, EventArgs e)
		{
            // If user account is deactivated
            if (!isActive)
            {
                // Remove session vars
                if (Session["gUserName"] != null)
                {
                    Session.Remove("gUserName");
                }
                if (Session["gUserType"] != null)
                {
                    Session.Remove("gUserType");
                }
                if (Session["gStaffId"] != null)
                {
                    Session.Remove("gStaffId");
                }
                if (Session["gFullName"] != null)
                {
                    Session.Remove("gFullName");
                }
                Session.Abandon();

                Response.Redirect("Forbidden.aspx");
            }
            else
            {

                if (Session["gUserType"].ToString() == "1") // Admin
                {
                    Response.Redirect("../Resource/Resource.aspx");
                }
                else if (Session["gUserType"].ToString() == "2") // Staff
                {
                    Response.Redirect("../Booking/BookingHistory.aspx");
                }
            }
		}
	}
}
