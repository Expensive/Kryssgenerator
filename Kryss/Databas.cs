using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;

namespace KryssGenerator
{
    public class Databas : AddRemove
    {
        // Deklarerar för vidare användning
        public String OurTable;
        public DataSet OurData;
        public OleDbDataAdapter DataAdapterSaveToDeltagare;

        public static OleDbCommand Command;
        public static OleDbDataReader DataReader;
        public static OleDbConnection Connection;

        // Skapar en anslutning till databasen.
        public static void OpenConn(string cmd)
        {
            // Anslut till databasen
            //Connection = new MySqlConnection(@"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;");
            Connection = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Kryss.accdb");
            //Connection = new MySqlConnection(@"Server = kryss-154741.mysql.binero.se; Database = 154741-kryss; Uid = 154741_oz70400; Pwd = 123456789;");
            Command = Connection.CreateCommand();
            Command.CommandText = cmd;
            Connection.Open();
        }


        // Uppdaterar databasen för att ladda in deltagare.
        public DataSet UpdateDatabase(int x) // Får ett värde x från t ex NrOfQuestions_LostFocus
        {
            OpenConn("SELECT * FROM Namn"); // Väljer Deltagare från tabellen Namn
            // Anslutning till databasen
            try
            {
                OurData = new DataSet(); // Sparar datan i OurData
                

                // Laddar deltagare till tabellen
                DataAdapterSaveToDeltagare = new OleDbDataAdapter();
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
            
            
            return OurData;
        }
    }
}
