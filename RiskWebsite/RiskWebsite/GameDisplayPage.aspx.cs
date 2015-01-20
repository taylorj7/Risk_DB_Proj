﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace RiskWebsite
{
    public partial class GameDisplayPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "mayja1";
            csBuilder.Password = "Jaminboy1313";
            String connectionString = csBuilder.ToString();

            using (var conn = new System.Data.SqlClient.SqlConnection(connectionString))
            {
                SqlCommand comm = new SqlCommand("Login", conn);
                comm.CommandType = System.Data.CommandType.StoredProcedure;
                comm.Parameters.Add(new SqlParameter("@UserID", System.Data.SqlDbType.Int));
                comm.Parameters["@UserID"].Direction = System.Data.ParameterDirection.Output;
                comm.Parameters.Add(new SqlParameter("ReturnVal", System.Data.SqlDbType.Int)).Direction = System.Data.ParameterDirection.ReturnValue;

                conn.Open();
                comm.ExecuteNonQuery();


                int returnval = (int)comm.Parameters["ReturnVal"].Value;
                conn.Close();
                if (returnval == 0)
                {
                    int id = (int)Convert.ToInt32(comm.Parameters["@UserID"].Value);
                    Console.WriteLine("" + id);
                    System.Diagnostics.Debug.WriteLine("" + id);
                    Response.Redirect("~/GameDisplayPage");
                }
            }
        }
    }
}