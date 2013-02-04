﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Data;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace KryssGenerator
{
    public partial class Databas
    {
        // Deklarerar för vidare användning
        public DataSet myData;
        public MySqlDataAdapter Deltagare;
        public MySqlConnection conn;
        public DataSet DataContext { get; set; }

        public DataSet DBload()
        {
            OpenConn();
            return GetMembers();
        }

        public void OpenConn()
        {
            // Anslut till databasen
            conn = new MySqlConnection(@"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;");
            //conn = new MySqlConnection(@"Server = kryss-154741.mysql.binero.se; Database = 154741-kryss; Uid = 154741_oz70400; Pwd = 123456789; Port = 3306;");
        }

        private DataSet GetMembers()
        {
            try
            {
                conn.Open(); // Öppnar anslutningen
                myData = new DataSet(); // Där datan ska sparas

                // Laddar deltagare tabellen
                Deltagare = new MySqlDataAdapter();
                Deltagare.SelectCommand = new MySqlCommand("SELECT * FROM Namn", conn); // Väljer ID och Namn från tabellen Namn
                Deltagare.Fill(myData, "Namn");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close(); // Avslutar anslutningen

            }
            // Returnerar den data som hämtas till myData
            return myData;
        }

        public DataSet ChangePerson()
        {
            // Utför ändringar på deltagare
            try
            {
                conn.Open();
                myData = new DataSet();

                Deltagare = new MySqlDataAdapter();
                Deltagare.SelectCommand = new MySqlCommand("INSERT INTO Namn VALUES ('Kalle Olsson')", conn);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return myData;
        }

    }
}
