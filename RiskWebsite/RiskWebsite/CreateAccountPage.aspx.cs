﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
namespace RiskWebsite
{
    public partial class CreateAccountPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CreateAccountButton_Click(object sender, EventArgs e)
        {
            String username = UsernameTextBox.Text;
            String password = PasswordTextBox.Text;
            String phrase = PhraseTextBox.Text;
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
                SqlCommand comm = new SqlCommand("CREATE USER", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@Username", username.Trim()));
                comm.Parameters.Add(new SqlParameter("@Password", (username.Trim()+password.Trim()).GetHashCode()));
                comm.Parameters.Add(new SqlParameter("@Phrase", (username.Trim() + phrase.Trim()).GetHashCode()));
                comm.Parameters.Add(new SqlParameter("ReturnVal", System.Data.SqlDbType.Int)).Direction = System.Data.ParameterDirection.ReturnValue;
                try
                {
                    conn.Open();
                    comm.ExecuteNonQuery();


                    int returnval = (int)comm.Parameters["ReturnVal"].Value;
                    conn.Close();
                    if (returnval == 0)
                    {

                        SuccessLabel.Text = "Failed to created account";

                    }
                    else
                    {

                        SuccessLabel.Text = "Successfully created account";
                    }
                }
                catch (SqlException error)
                {

                }
            }
        }
    }
}