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
    public partial class Databas: AddRemove
    {
        // Deklarerar för vidare användning
        public DataSet myData;
        public MySqlDataAdapter Deltagare;
        public MySqlConnection conn;
        //public DataSet DataContext { get; set; }
        

        // Skapar en anslutning till databasen.
        public void OpenConn()
        {
            // Anslut till databasen
            conn = new MySqlConnection(@"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;");
            //conn = new MySqlConnection(@"Server = kryss-154741.mysql.binero.se; Database = 154741-kryss; Uid = 154741_oz70400; Pwd = 123456789;");
            conn.Open();
        }

        // Uppdaterar databasen för att ladda in deltagare.
        public DataSet UpdateDatabase()
        {
            OpenConn(); // Anslutning till databasen
            try
            {
                myData = new DataSet(); // Där datan ska sparas

                // Laddar deltagare tabellen
                Deltagare = new MySqlDataAdapter();
                Deltagare.SelectCommand = new MySqlCommand("SELECT * FROM Namn", conn); // Väljer ID och Deltagare från tabellen Namn
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

        // Lägga till en deltagare i databasen.
        public void ChangePerson()
        {
            string sqlIns = "INSERT INTO Namn (Deltagare) VALUES (@Deltagare)";
            OpenConn();
            
            try
                {
                    MySqlCommand cmdIns = new MySqlCommand(sqlIns, conn);
                    cmdIns.Parameters.AddWithValue("@Deltagare", chattextbox);
                    cmdIns.ExecuteNonQuery();

                    cmdIns.Parameters.Clear();
                    cmdIns.CommandText = "SELECT @@IDENTITY";

                    // Get the last inserted id.
                    int insertID = Convert.ToInt32(cmdIns.ExecuteScalar());

                    cmdIns.Dispose();
                    cmdIns = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    UpdateDatabase();
                }
        }
    }
}
