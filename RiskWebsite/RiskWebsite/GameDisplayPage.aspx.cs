using System;
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
            
        }

        public string getWhileLoopData() {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "mayja1";
            csBuilder.Password = "Jaminboy1313";
            String connectionString = csBuilder.ToString();
            string htmlStr = "";
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("Get Active Games", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_ID", Application["id"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

        while (reader.Read())
        {

            int id = reader.GetInt32(0);
            int CurrentPosition = reader.GetInt16(1);
            htmlStr += "<tr><td>" + id + "</td><td>" + CurrentPosition + "</td></tr>";                  
        }

        thisConnection.Close();
        return htmlStr;
}

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "mayja1";
            csBuilder.Password = "Jaminboy1313";
            String connectionString = csBuilder.ToString();
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("Create Game", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_ID", Application["id"]));
            thisConnection.Open();
            thisCommand.ExecuteNonQuery();
        }
    }
}