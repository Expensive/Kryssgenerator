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
        public DataSet UpdateDatabase(int x) // Får ett värde x från t ex NrOfQuestions_LostFocus
        {
            OpenConn("SELECT Deltagare FROM Namn"); // Väljer Deltagare från tabellen Namn
            // Anslutning till databasen
            try
            {
                OurData = new DataSet(); // Sparar datan i OurData

                // Laddar deltagare till tabellen
                DataAdapterSaveToDeltagare = new MySqlDataAdapter();
                DataAdapterSaveToDeltagare.SelectCommand = Command;
                DataAdapterSaveToDeltagare.Fill(OurData, "Namn");
                
                // Hämtar antal rader som ska skrivas ut från "Antal uppgifter" i MainMenu
                for(int i=1;i<=x;i++) // Kör upp till x som hämtas från MainMenu
                { 
                    DataColumn column = new DataColumn();
                    column.DataType = System.Type.GetType("System.Boolean"); // Gör en kolumn som antingen är true/false
                    column.DefaultValue = true; // Sätter standardvärdet till false. Tömmer alla kryssrutor
                    column.ColumnName = i.ToString(); // Skriv ut i som nr på uppgift ovanför rutorna
                    OurData.Tables["Namn"].Columns.Add(column); // Lägg till i DataSet OurData
                }
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
    }
}
