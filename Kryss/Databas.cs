using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Windows;
using System.Data;

namespace Kryss
{
    public partial class Databas
    {   
        // Deklarerar för vidare användning
        public DataSet myData;
        public MySqlDataAdapter Namn;
        public DataSet DataContext { get; set; }

        public void DBload()
        {
            Initialize();
            //ChangePerson();
        }

        private void Initialize()
        {
            // Anslut till databasen
            string connStr = @"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;";
            MySqlConnection conn = new MySqlConnection(connStr);

            try
            {
                conn.Open(); // Öppnar anslutningen
                myData = new DataSet(); // Där datan ska sparas

                // Laddar Namn tabellen
                Namn = new MySqlDataAdapter();
                Namn.SelectCommand = new MySqlCommand("SELECT * FROM Namn", conn);
                Namn.Fill(myData, "Namn");


                this.DataContext = myData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close(); // Avslutar anslutningen
            }
        }

        private void ChangePerson()
        {
            // Utför ändringar på deltagare 
        }
    }
}
