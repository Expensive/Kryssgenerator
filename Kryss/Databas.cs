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
        public MySqlConnection conn;

        public void DBload()
        {
            OpenConn();
            GetMembers();
            //ChangePerson();
        }

        public void OpenConn()
        {
            // Anslut till databasen
            string connStr = @"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;";
            //string connStr = @"Server = kryss-154741.mysql.binero.se; Database = 154741-kryss; Uid = 154741_oz70400; Pwd = 123456789; Port = 3306;";
            conn = new MySqlConnection(connStr);

        }

        private void GetMembers()
        {
            try
            {
                conn.Open(); // Öppnar anslutningen
                myData = new DataSet(); // Där datan ska sparas

                // Laddar deltagare tabellen
                Namn = new MySqlDataAdapter();
                Namn.SelectCommand = new MySqlCommand("SELECT * FROM Namn", conn); // Väljer ID och Namn från tabellen Namn
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
            try
            {
                conn.Open();
                myData = new DataSet();

                Namn = new MySqlDataAdapter();
                Namn.SelectCommand = new MySqlCommand("INSERT INTO Namn VALUES ('Kalle Olsson')", conn);

                this.DataContext = myData;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
