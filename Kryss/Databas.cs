using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace KryssGenerator
{
    public class Databas : AddRemove
    {
        // Deklarerar för vidare användning
        public DataSet myData;
        public MySqlDataAdapter Deltagare;

        public static MySqlCommand Command;
        public static MySqlDataReader DataReader;
        public static MySqlConnection Connection;

        // Skapar en anslutning till databasen.
        public static void OpenConn(string cmd)
        {
            // Anslut till databasen
            Connection = new MySqlConnection(@"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;");
            Command = Connection.CreateCommand();
            Command.CommandText = cmd;
            Connection.Open();
            DataReader = Command.ExecuteReader();
            Connection.Close();
        }

        // Uppdaterar databasen för att ladda in deltagare.
        public DataSet UpdateDatabase()
        {
            OpenConn("SELECT * FROM Namn");
            // Anslutning till databasen
            try
            {
                myData = new DataSet(); // Där datan ska sparas

                // Laddar deltagare tabellen
                Deltagare = new MySqlDataAdapter();
                Deltagare.SelectCommand = Command; // Väljer ID och Deltagare från tabellen Namn
                Deltagare.Fill(myData, "Namn");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close(); // Avslutar anslutningen

            }
            // Returnerar den data som hämtas till myData
            return myData;
        }

        // Lägga till en deltagare i databasen.
        public void ChangePerson()
        {
            // Chill man!
        }
        public static void GetNumberOfQuestions()
        {

        }
    }
}
