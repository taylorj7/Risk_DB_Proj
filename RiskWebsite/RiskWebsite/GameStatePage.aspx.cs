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
    public partial class GameStatePage : System.Web.UI.Page
    {
        private String connectionString;
        private object[] myCountries;
        protected void Page_Load(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder csBuilder = new SqlConnectionStringBuilder();
            csBuilder.DataSource = "titan.csse.rose-hulman.edu";
            csBuilder.InitialCatalog = "Risk42";
            csBuilder.Encrypt = true;
            csBuilder.TrustServerCertificate = true;
            csBuilder.UserID = "333Winter2014Risk";
            csBuilder.Password = "Password123";
            connectionString = csBuilder.ToString();
            setupDropDownLists();
            GameIDLabel.Text = "" + Application["game"];
            if ((Boolean)Application["gameStarted"])
            {
                StartButton.Visible = false;
            }
        }

        public void setupDropDownLists()
        {
            ArrayList countries = new ArrayList();
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
                if (userID == (int)Application["id"])
                {
                   countries.Add(country);
                }
            }

            thisConnection.Close();
            myCountries = countries.ToArray();
            YourCountriesAttack.DataSource = myCountries;
            YourCountriesAttack.DataBind();
            YourCountriesMove.DataSource = myCountries;
            YourCountriesMove.DataBind();
            YourCountriesPlace.DataSource = myCountries;
            YourCountriesPlace.DataBind();
            
        }
        public string getWhileLoopData()
        {
            
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

        protected void StartButton_Click(object sender, EventArgs e)
        {
            ArrayList countryNames = new ArrayList();
            ArrayList players = new ArrayList();

            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("gET_cOUNTRIES", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

            while (reader.Read())
            {
                string country = reader.GetString(0);
                countryNames.Add(country);
            }
            thisConnection.Close();
            SqlConnection playerConnection = new SqlConnection(connectionString);
            SqlCommand playerCommand = new SqlCommand("GeT_gamerS", playerConnection);

            playerCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
            playerCommand.CommandType = System.Data.CommandType.StoredProcedure;
            playerConnection.Open();
            SqlDataReader playerReader = playerCommand.ExecuteReader();
            while (playerReader.Read())
            {
                players.Add(playerReader.GetInt32(0));
            }
            playerConnection.Close();
            Random rand = new Random();
            int numPlayers = players.Count;
            int playerIndex = 0;
            int troopCount = 2;
            if (numPlayers < 2)
            {
                return;
            }
            SqlConnection startConnection = new SqlConnection(connectionString);
            SqlCommand startCommand = new SqlCommand("start_game", startConnection);

            startCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
            startCommand.CommandType = System.Data.CommandType.StoredProcedure;
            startConnection.Open();
            startCommand.ExecuteNonQuery();
            startConnection.Close();
            while (countryNames.Count > 0)
            {
                string country = (string) countryNames[rand.Next(countryNames.Count)];
                countryNames.Remove(country);
                int player = (int)players[playerIndex];
                SqlConnection gameConnection = new SqlConnection(connectionString);
                SqlCommand gameCommand = new SqlCommand("ADD_cOuNtry_NeW_gamE", gameConnection);
                gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
                gameCommand.Parameters.Add(new SqlParameter("@UserID", player));
                gameCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
                gameCommand.Parameters.Add(new SqlParameter("@Country", country));
                gameCommand.Parameters.Add(new SqlParameter("@troopCount", troopCount));
                gameConnection.Open();
                gameCommand.ExecuteNonQuery();
                gameConnection.Close();
                playerIndex++;
                if (playerIndex == numPlayers)
                {
                    playerIndex = 0;
                }
            }
            StartButton.Visible = false;
            
        }
    }
}