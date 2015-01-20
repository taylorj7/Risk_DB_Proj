using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RiskWebsite
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String username = UsernameTextBox.Text;
            String password = PasswordTextBox.Text;
            String connectionString = "titan/titan.Risk42.dbo";
            using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                using (var comm = conn.CreateCommand())
                {
                    conn.Open();
                    comm.CommandText = "";

                    // command type, parameters, etc.

                    //pick one of the following
                    comm.ExecuteNonQuery();
                    int value = (int)comm.ExecuteScalar();
                    System.Data.SqlClient.SqlDataReader reader = comm.ExecuteReader();

                }
            }
            if (username == "admin" && password == "1234")
            {
                Response.Redirect("~/GameDisplayPage");
            }
        }
    }
}