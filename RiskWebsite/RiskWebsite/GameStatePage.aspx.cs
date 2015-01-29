using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace RiskWebsite
{
    public partial class GameStatePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            GameIDLabel.Text = "" + Application["game"];
        }

        public string getWhileLoopData()
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "333Winter2014Risk";
            csBuilder.Password = "Password123";
            String connectionString = csBuilder.ToString();
            string htmlStr = "";
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("getGameState", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_id", Application["id"]));
            thisCommand.Parameters.Add(new SqlParameter("@Game_id", Application["game"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

            while (reader.Read())
            {

                int id = reader.GetInt32(0);
                int numSoldiers = reader.GetInt32(1);
                int userID = reader.GetInt32(2);
                string country = reader.GetString(3);
                htmlStr += "<tr><td>" + country + "</td><td>" + userID + "</td><td>" + numSoldiers + "</td></tr>";
            }

            thisConnection.Close();
            return htmlStr;
        }
    }
}