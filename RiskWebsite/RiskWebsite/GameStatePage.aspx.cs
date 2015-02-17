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
            setupDropDownLists(false);
            GameIDLabel.Text = "" + Application["game"];
            
            if ((Boolean)Application["gameStarted"])
            {
                StartButton.Visible = false;
                hideTurnFields();
            }
            else
            {
                hideEverything();
            }
        }

        public void setupDropDownLists(Boolean redoList)
        {
            ArrayList countries = new ArrayList();
            countryTroops.Clear();
            countryOwners.Clear();
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
            if (!IsPostBack)
            {
                Application["conquered"] = false;
            }
            if (!IsPostBack || redoList)
            {
                Application["troops"] = 0;
                YourCountriesAttack.DataSource = myCountries;
                YourCountriesAttack.DataBind();
                YourCountriesMove.DataSource = myCountries;
                YourCountriesMove.DataBind();
                YourCountriesPlace.DataSource = myCountries;
                YourCountriesPlace.DataBind();
                BorderingCountriesAttack.Items.Clear();
                YourBorderingCountriesMove.Items.Clear();
            }
            RemainingTroops.Text = "" + Application["troops"];
        }

        public string getCards()
        {
            string htmlStr = "";
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("getHand", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));
            thisCommand.Parameters.Add(new SqlParameter("@Game_ID", Application["game"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();

            while (reader.Read())
            {
                int soldier = reader.GetInt16(0);
                int horse = reader.GetInt16(1);
                int cannon = reader.GetInt16(2);                
                int wild = reader.GetInt16(3);
                htmlStr += "<tr><td>" + "soldier" + "</td><td>" + soldier + "</td></tr>";
                htmlStr += "<tr><td>" + "horse" + "</td><td>" + horse + "</td></tr>";
                htmlStr += "<tr><td>" + "cannon" + "</td><td>" + cannon + "</td></tr>";
                htmlStr += "<tr><td>" + "wild" + "</td><td>" + wild + "</td></tr>";
            }

            thisConnection.Close();
            return htmlStr;
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
            int defendDice = defendingTroops;
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
            for (int i = 0; i < Math.Min(defendDice, attackDice); i++)
            {
                int maxAttack = calculateMaxIndex(attackNums);
                int maxDefend = calculateMaxIndex(defendNums);
                int defendNum = (int) defendNums[maxDefend];
                int attackNum = (int)attackNums[maxAttack];
                if (defendNum >= attackNum)
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
                gameCommand2.Parameters.Add(new SqlParameter("@Country", defendingCountry));
                gameCommand2.Parameters.Add(new SqlParameter("@newTroops", countryTroops[defendingCountry] - defendLoss));
                gameConnection2.Open();
                gameCommand2.ExecuteNonQuery();
                gameConnection2.Close();
                AttackResult.Text = yourCountry + " lost " + attackLoss + " troops and " + defendingCountry + " lost " + defendLoss + " troops ";
            }
            else
            {
                AttackResult.Text += "You conquered " + defendingCountry;
                Application["conquered"] = true;
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

                setupDropDownLists(true);
                return;
            }
            setupDropDownLists(false);
            
        }
        private int turnIn(int soldier, int horse, int cannon, int wild)
        {
            int bonus = 0;
            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("getRidOfHands", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));
            gameCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
            gameCommand.Parameters.Add(new SqlParameter("@soldier", soldier));
            gameCommand.Parameters.Add(new SqlParameter("@horse", horse));
            gameCommand.Parameters.Add(new SqlParameter("@cannon", cannon));
            gameCommand.Parameters.Add(new SqlParameter("@wild", wild));
            gameCommand.Parameters.Add(new SqlParameter("@Bonus", System.Data.SqlDbType.Int));
            gameCommand.Parameters["@Bonus"].Direction = System.Data.ParameterDirection.Output;
            gameConnection.Open();
            gameCommand.ExecuteNonQuery();
            bonus = (int)Convert.ToInt32(gameCommand.Parameters["@Bonus"].Value);
            gameConnection.Close();
            return bonus;
        }
        private int turnInTroops()
        {
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("getHand", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));
            thisCommand.Parameters.Add(new SqlParameter("@Game_ID", Application["game"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();
            int soldier = 0;
            int horse = 0;
            int cannon = 0;
            int wild = 0;
            while (reader.Read())
            {
                soldier = reader.GetInt16(0);
                horse = reader.GetInt16(1);
                cannon = reader.GetInt16(2);
                wild = reader.GetInt16(3);
            }

            thisConnection.Close();
            if((soldier + wild) >=3) {
                return turnIn(soldier, 0, 0, 3 - soldier);
            }
            else if ((horse + wild) >=3) {
                return turnIn(0, horse, 0, 3- horse);
            }
            else if ((cannon + wild)>=3) {
                return turnIn(0, 0, cannon, 3- cannon);
            }
            else if (wild>=3) {
                return turnIn(0, 0, 0, 3);
            }

            if(soldier >0 && horse > 0 && cannon > 0) {
                return turnIn(1, 1, 1, 0);
            }

            if(wild > 0) {
                if(soldier > 0) {
	                if(horse > 0) {
		                return turnIn(1, 1, 0, 1);
	                }
	                if(cannon > 0) {
                        return turnIn(1, 0, 1, 1);
                    }
                }
                else if (horse > 0 && cannon > 0) {
                    return turnIn(0, 1, 1, 1);
                }
            }
            return 0;


        }

        private void getPlaceTroops()
        {

            int bonus = this.turnInTroops();
            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("getBonus", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));
            gameCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
            gameCommand.Parameters.Add(new SqlParameter("@Bonus", System.Data.SqlDbType.Int));
            gameCommand.Parameters["@Bonus"].Direction = System.Data.ParameterDirection.Output;
            gameConnection.Open();
            gameCommand.ExecuteNonQuery();
            bonus += (int)Convert.ToInt32(gameCommand.Parameters["@Bonus"].Value);
            gameConnection.Close();
            int countryCount = 0;
            int[] owners = countryOwners.Values.ToArray();
            for (int i = 0; i < owners.Length; i++ )
            {
                if (owners[i] == (int)Application["id"])
                {
                    countryCount++;
                }
            }

            Application["troops"] = bonus + (countryCount / 3);
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

        private void hideTurnFields()
        {
            TurnLabel.Text = "It is your turn!";
            int turn;
            SqlConnection thisConnection = new SqlConnection(connectionString);
            SqlCommand thisCommand = new SqlCommand("getUserTurnPosition", thisConnection);
            thisCommand.CommandType = System.Data.CommandType.StoredProcedure;
            thisCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));
            thisCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));
            thisConnection.Open();
            SqlDataReader reader = thisCommand.ExecuteReader();
            while (reader.Read())
            {
                turn = reader.GetInt16(0);
                if (turn != (int)Application["turn"])
                {
                    hideEverything();
                    return;
                }
            }
            if (!IsPostBack)
            {
                getPlaceTroops();
            }
            RemainingTroops.Text = "" + Application["troops"];
            int maxTroops = (int)Application["troops"];
            if (maxTroops != 0)
            {
                AttackButton.Visible = false;
                EndTurn.Visible = false;
                MoveTroopsButton.Visible = false;
            }
            else
            {
                AttackButton.Visible = true;
                EndTurn.Visible = true;
                MoveTroopsButton.Visible = true;
            }
        }
        protected void hideEverything() {
            TurnLabel.Text = "It is not your turn!";
            AttackButton.Visible = false;
            AttackResult.Visible = false;
            YourBorderingCountriesMove.Visible = false;
            YourCountriesAttack.Visible = false;
            YourCountriesMove.Visible = false;
            YourCountriesPlace.Visible = false;
            BorderingCountriesAttack.Visible = false;
            PlaceTextBox.Visible = false;
            PlaceButton.Visible = false;
            MoveTroopsButton.Visible = false;
            MoveTroopsNumber.Visible = false;
            EndTurn.Visible = false;
                    
        }
        protected void EndTurn_Click(object sender, EventArgs e)
        {
            int random = (new Random()).Next(4);
            if(!((Boolean) Application["conquered"])) {
                random = 5;
            }
            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("ADV_TURN", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@GameID", Application["game"]));

            gameCommand.Parameters.Add(new SqlParameter("@UserID", Application["id"]));

            gameCommand.Parameters.Add(new SqlParameter("@Random", random));
            gameConnection.Open();
            gameCommand.ExecuteNonQuery();
            gameConnection.Close();
            hideEverything();
        }

        protected void PlaceButton_Click1(object sender, EventArgs e)
        {

            string yourCountry = YourCountriesPlace.SelectedValue;
            int troops = Convert.ToInt32(PlaceTextBox.Text);
            int placeTroops = (int) Application["troops"];
            setupDropDownLists(false);
            if (troops < 0 || troops > placeTroops)
            {
                PlaceLabel.Text = "You can't place that many troops";
                return;
            }

            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("Update_Garrison", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@Owner", Application["id"]));
            gameCommand.Parameters.Add(new SqlParameter("@Country", yourCountry));
            gameCommand.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
            gameCommand.Parameters.Add(new SqlParameter("@newTroops", (countryTroops[yourCountry] + troops)));
            gameConnection.Open();
            gameCommand.ExecuteNonQuery();
            gameConnection.Close();
            Application["troops"] = placeTroops - troops;
            if (placeTroops - troops == 0)
            {
                AttackButton.Visible = true;
                EndTurn.Visible = true;
                MoveTroopsButton.Visible = true;
            }
            setupDropDownLists(false);
        }



        protected void MoveTroopsButton_Click(object sender, EventArgs e)
        {
            string yourCountry = YourCountriesMove.SelectedValue;
            string borderCountry = YourBorderingCountriesMove.SelectedValue;
            int troops = Convert.ToInt32(MoveTroopsNumber.Text);
            setupDropDownLists(false);
            if (troops < 0 || troops >= countryTroops[yourCountry])
            {
                PlaceLabel.Text = "You can't move that many troops";
                return;
            }

            SqlConnection gameConnection = new SqlConnection(connectionString);
            SqlCommand gameCommand = new SqlCommand("Update_Garrison", gameConnection);
            gameCommand.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand.Parameters.Add(new SqlParameter("@Owner", Application["id"]));
            gameCommand.Parameters.Add(new SqlParameter("@Country", yourCountry));
            gameCommand.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
            gameCommand.Parameters.Add(new SqlParameter("@newTroops", (countryTroops[yourCountry] - troops)));
            gameConnection.Open();
            gameCommand.ExecuteNonQuery();
            gameConnection.Close();

            SqlConnection gameConnection2 = new SqlConnection(connectionString);
            SqlCommand gameCommand2 = new SqlCommand("Update_Garrison", gameConnection2);
            gameCommand2.CommandType = System.Data.CommandType.StoredProcedure;
            gameCommand2.Parameters.Add(new SqlParameter("@Owner", Application["id"]));
            gameCommand2.Parameters.Add(new SqlParameter("@Country", borderCountry));
            gameCommand2.Parameters.Add(new SqlParameter("@gameID", Application["game"]));
            gameCommand2.Parameters.Add(new SqlParameter("@newTroops", (countryTroops[borderCountry] + troops)));
            gameConnection2.Open();
            gameCommand2.ExecuteNonQuery();
            gameConnection2.Close();
            setupDropDownLists(false);
        }

        protected void YourCountriesMove_SelectedIndexChanged(object sender, EventArgs e)
        {
            ArrayList borderCountries = getBorderingCountries(YourCountriesMove.SelectedItem.Value);
            for (int i = 0; i < borderCountries.Count; i++)
            {
                if ((int)Application["id"] != countryOwners[((string)borderCountries[i])])
                {
                    borderCountries.RemoveAt(i);
                    i--;
                }
            }

            YourBorderingCountriesMove.DataSource = borderCountries.ToArray();
            YourBorderingCountriesMove.DataBind();
        }

    }

    
}