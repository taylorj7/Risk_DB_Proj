using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Collections;
namespace RiskWebsite
{
    public partial class GameDisplayPage : System.Web.UI.Page
    {
        Dictionary<int, Boolean> gameStarted;
        Dictionary<int, int> gameTurn;
        protected void Page_Load(object sender, EventArgs e)
        {
            getWhileLoopData();
        }

        public string getWhileLoopData() {
            gameStarted = new Dictionary<int, Boolean>();
            gameTurn = new Dictionary<int, int>();
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
            SqlCommand thisCommand = new SqlCommand("Get Active Games", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_ID", Application["id"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

        while (reader.Read())
        {

            int id = reader.GetInt32(1);
            int CurrentPosition = reader.GetInt16(0);
            Boolean started = reader.GetBoolean(2);
            gameStarted.Add(id, started);
            gameTurn.Add(id, CurrentPosition);
            htmlStr += "<tr><td>" + id + "</td><td>" + CurrentPosition + "</td><td>" + started + "</td></tr>";                  
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
            csBuilder.UserID = "333Winter2014Risk";
            csBuilder.Password = "Password123";
            String connectionString = csBuilder.ToString();
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("Create Game", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_ID", Application["id"]));
            thisConnection.Open();
            thisCommand.ExecuteNonQuery();
        }

        protected void AddUserButton_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "333Winter2014Risk";
            csBuilder.Password = "Password123";
            String connectionString = csBuilder.ToString();
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("add To Game", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@User_ID", Application["id"]));
            thisCommand.Parameters.Add(new SqlParameter("@Username", UsernameTextBox.Text));
            thisCommand.Parameters.Add(new SqlParameter("@Game_id", Convert.ToInt32(GameIDTextBox.Text)));
            thisCommand.Parameters.Add(new SqlParameter("ReturnVal", System.Data.SqlDbType.Int)).Direction = System.Data.ParameterDirection.ReturnValue;

            thisConnection.Open();
            thisCommand.ExecuteNonQuery();

            int returnval = (int)thisCommand.Parameters["ReturnVal"].Value;
            thisConnection.Close();
            if (returnval == 0)
            {
                Label1.Text = "User added successfully!";
            }
            else
            {
                Label1.Text = "User failed to be added";
            }
        }

        protected void EnterGameButton_Click(object sender, EventArgs e)
        {
            int gameID = Convert.ToInt32(GameIDTextBox2.Text);
            Application["game"] = gameID;
            Application["gameStarted"] = gameStarted[gameID];
            Application["turn"] = gameTurn[gameID];
            Response.Redirect("~/GameStatePage");

        }
    }
}