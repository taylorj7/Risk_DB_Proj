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
        private Dictionary<String, int> countryTroops;
        private Dictionary<String, int> countryOwners;
        private String connectionString;
        private object[] myCountries;
        protected void Page_Load(object sender, EventArgs e)
        {
            countryTroops = new Dictionary<string, int>();
            countryOwners = new Dictionary<string, int>();
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
                countryTroops.Add(country, numSoldiers);
                countryOwners.Add(country, userID);
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

        protected void YourCountriesAttack_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList borderCountries = getBorderingCountries(YourCountriesAttack.SelectedItem.Value);
            for (int i = 0; i < borderCountries.Count; i++)
            {
                if ((int) Application["id"] == countryOwners[((string)borderCountries[i])])
                {
                    borderCountries.RemoveAt(i);
                    i--;
                }
            }
            BorderingCountriesAttack.DataSource = borderCountries.ToArray();
            BorderingCountriesAttack.DataBind();
        }

        protected void AttackButton_Click(object sender, EventArgs e)
        {
            ArrayList attackNums = new ArrayList();
            ArrayList defendNums = new ArrayList();
            string yourCountry = YourCountriesAttack.SelectedItem.Value;
            string defendingCountry = BorderingCountriesAttack.SelectedItem.Value;
            int yourTroops = countryTroops[yourCountry];
            int defendingTroops = countryTroops[defendingCountry];
            int attackDice = yourTroops -1;
            int defendDice = defendingTroops -1;
            int attackLoss = 0;
            int defendLoss = 0;
            if (yourTroops <= 1)
            {
                AttackResult.Text = "You can't attack with so few troops!";
                return;
            }
            if (attackDice > 3)
            {
                attackDice = 3;
            }
            if (defendDice > 2)
            {
                defendDice = 2;
            }
            Random rand = new Random();
            for (int i = 0; i < attackDice; i++)
            {
                int roll = rand.Next(1, 7);
                attackNums.Add(roll);
            }
            for (int i = 0; i < defendDice; i++)
            {
                int roll = rand.Next(1, 7);
                defendNums.Add(roll);
            }
            for (int i = 0; i < defendDice; i++)
            {
                int maxAttack = calculateMaxIndex(attackNums);
                int maxDefend = calculateMaxIndex(defendNums);

                if ((int)defendNums[maxDefend] >= (int)attackNums[maxAttack])
                {
                    attackLoss++;
                }
                else
                {
                    defendLoss++;
                }
                attackNums.RemoveAt(maxAttack);
                defendNums.RemoveAt(maxDefend);
            }
            AttackResult.Text = "";
            

            if (countryTroops[defendingCountry] > defendLoss)
            {
                SqlConnection gameConnection = new SqlConnection(connectionString);
                SqlCommand gameCommand = new SqlCommand("Update_Garrison", gameConnection);
                gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
                gameCommand.Parameters.Add(new SqlParameter("@Owner", countryOwners[yourCountry]));
                gameCommand.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
                gameCommand.Parameters.Add(new SqlParameter("@Country", yourCountry));
                gameCommand.Parameters.Add(new SqlParameter("@newTroops", countryTroops[yourCountry] - attackLoss));
                gameConnection.Open();
                gameCommand.ExecuteNonQuery();
                gameConnection.Close();

                SqlConnection gameConnection2 = new SqlConnection(connectionString);
                SqlCommand gameCommand2 = new SqlCommand("Update_Garrison", gameConnection2);
                gameCommand2.CommandType = System.Data.CommandType.StoredProcedure;
                gameCommand2.Parameters.Add(new SqlParameter("@Owner", countryOwners[defendingCountry]));
                gameCommand2.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
                gameCommand2.Parameters.Add(new SqlParameter("@Country", yourCountry));
                gameCommand2.Parameters.Add(new SqlParameter("@newTroops", countryTroops[defendingCountry] - defendLoss));
                gameConnection2.Open();
                gameCommand2.ExecuteNonQuery();
                gameConnection2.Close();
                AttackResult.Text = yourCountry + " lost " + attackLoss + " troops and " + defendingCountry + " lost " + defendLoss + " troops ";
            }
            else
            {
                AttackResult.Text += "You conquered " + defendingCountry;
                defendLoss = 0; //made zero since we are going to do the conquer query, don't need to update the defending country.
                //sql query to conquer country, subtract add one to attack loss
                SqlConnection gameConnection3 = new SqlConnection(connectionString);
                SqlCommand gameCommand3 = new SqlCommand("Change_Country_Owner", gameConnection3);
                gameCommand3.CommandType = System.Data.CommandType.StoredProcedure;
                gameCommand3.Parameters.Add(new SqlParameter("@attackerID", Application["id"]));
                gameCommand3.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
                gameCommand3.Parameters.Add(new SqlParameter("@Country", defendingCountry));
                gameConnection3.Open();
                gameCommand3.ExecuteNonQuery();
                gameConnection3.Close();

                SqlConnection gameConnection = new SqlConnection(connectionString);
                SqlCommand gameCommand = new SqlCommand("Update_Garrison", gameConnection);
                gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
                gameCommand.Parameters.Add(new SqlParameter("@Owner", countryOwners[yourCountry]));
                gameCommand.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
                gameCommand.Parameters.Add(new SqlParameter("@Country", yourCountry));
                gameCommand.Parameters.Add(new SqlParameter("@newTroops", countryTroops[yourCountry] - (attackLoss + 1)));
                gameConnection.Open();
                gameCommand.ExecuteNonQuery();
                gameConnection.Close();
            }
            setupDropDownLists();
            
        }

        private int calculateMaxIndex(ArrayList list) {
            if (list.Count == 0)
            {
                return -1;
            }
            int max = (int)list[0];
            int index = 0;
            for (int i = 0; i < list.Count; i++)
            {
                int num = (int)list[i];
                if (num > max)
                {
                    index = i;
                    max = num;
                }
            }
            return index;
        }
        private ArrayList getBorderingCountries(string country)
        {
            ArrayList borderCountries = new ArrayList();
            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("borderingCountries", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@Country", country));
            gameConnection.Open();
            SqlDataReader countryReader = gameCommand.ExecuteReader();
            while (countryReader.Read())
            {
                borderCountries.Add(countryReader.GetString(0));
            }
            gameConnection.Close();
            return borderCountries;
        }
    }

    
}