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
        public String OurTable;
        public DataSet OurData;
        public MySqlDataAdapter DataAdapterSaveToDeltagare;

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
        }

        // Uppdaterar databasen för att ladda in deltagare.
        public DataSet UpdateDatabase()
        {
            OpenConn("SELECT Deltagare FROM Namn"); // Väljer Deltagare från tabellen Namn
            // Anslutning till databasen
            try
            {
                OurData = new DataSet(); // Sparar datan i OurData

                // Laddar deltagare tabellen
                DataAdapterSaveToDeltagare = new MySqlDataAdapter();
                DataAdapterSaveToDeltagare.SelectCommand = Command;
                DataAdapterSaveToDeltagare.Fill(OurData, "Namn");
                
                // Lägga till en kolumn i databasen
                //OurData.Tables["Namn"].Columns.Add();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                Connection.Close(); // Avslutar anslutningen

            }
            // Returnerar den data som hämtas till OurData
            return OurData;
        }

        // Lägga till en deltagare i databasen.
        public void ChangePerson()
        {
            // Chill man!
        }
        public static void GetNumberOfQuestions()
        {
            // I said chill man!
        }
    }
}
