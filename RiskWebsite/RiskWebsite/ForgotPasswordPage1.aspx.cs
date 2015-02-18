using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace RiskWebsite
{
    public partial class ForgotPasswordPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String username = UsernameTextBox.Text.Trim();
            String password = PasswordTextBox.Text.Trim();
            String phrase = PhraseTextBox.Text.Trim();
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "333Winter2014Risk";
            csBuilder.Password = "Password123";
            String connectionString = csBuilder.ToString();

            using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("FoRGottEN PAssword", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@Username", username.Trim()));
                comm.Parameters.Add(new SqlParameter("@NewPass", (username.Trim() + password.Trim()).GetHashCode()));
                comm.Parameters.Add(new SqlParameter("@Confirmation", (username.Trim() + phrase.Trim()).GetHashCode()));
                comm.Parameters.Add(new SqlParameter("ReturnVal", System.Data.SqlDbType.Int)).Direction = System.Data.ParameterDirection.ReturnValue;

                conn.Open();
                comm.ExecuteNonQuery();


                int returnval = (int)comm.Parameters["ReturnVal"].Value;
                conn.Close();
                if (returnval == 1)
                {

                    SuccessLabel.Text = "Failed to change password";

                }
                else
                {

                    SuccessLabel.Text = "Successfully changed password";
                }
            }
        }
    }
}