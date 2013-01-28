using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Kryss
{
    class Databas
    {        

        public DBConnect()
        {
            Initialize();
            OpenConnection();
        }

        MySqlDataAdapter namn;

        //Initialize values
        private void Initialize()
        {
            string connectionString = @"Server=projweb.hj.se;Database=guni1189;Uid=guni1189;Pwd=Bkij614;Port=3306;";

            string connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        //Insert statement
        public void Insert()
        {
        }

        //Select statement
        public List <string> [] Select()
        {
        }

        private void ImporteraDokument()
        {



            //public string FirstName { get; set; }
            //public string LastName { get; set; }
            
            //public Deltagare(string fName, string lName)
            //{
            //    FirstName = fName;
            //    LastName = lName;
            //}

            ////ObservableCollection<Peoples> GPeoples;
            ////var Peoples = new ObservableCollection<Person>(search.Peoples());

            ////comboPeople.ItemsSource = comboP;
            ////GPeoples = Peoples;

            //StreamReader f = new StreamReader("Personer.txt");
            ////Räknar hur många rader det är i textfilen
            //int x = File.ReadAllLines("Personer.txt").Length;

            //string[] a = null;
            //Person[] people = new Person[x];
            //int antal = 0;
            //while (true)
            //{
            //    string rad = f.ReadLine();
            //    if (rad == null)
            //        break;
            //    a = rad.Split();
            //    people[antal] = new Person(a[0], a[1]);
            //    antal++;
            //}
            //InitializeComponent();
            //comboPeople.ItemsSource = people;
            //Binding nameBinding = new Binding("FirstName");
            //lblFName.SetBinding(ContentProperty, nameBinding);
            //Binding lnameBinding = new Binding("LastName");
            //lbllName.SetBinding(ContentProperty, lnameBinding);
        }
    }
}
