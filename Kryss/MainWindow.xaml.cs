using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;

namespace Kryss
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
           
        }
     
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Skapar en lokal kopia load som anropar DBload i Databas.cs
            //Databas load = new Databas();
            //load.DBload();

            // ********** Allt nedan skall egentligen ligga i Databas.cs . Kopplingen fungerar inte. **********
            DataSet myData;
            MySqlDataAdapter Deltagare;
            MySqlConnection conn;
            conn = new MySqlConnection(@"Server = projweb.hj.se; Database = olde1103; Uid = olde1103; Pwd = Tdlf278; Port = 3306;");

            try
            {
                conn.Open(); // Öppnar anslutningen
                myData = new DataSet(); // Där datan ska sparas

                // Laddar deltagare tabellen
                Deltagare = new MySqlDataAdapter();
                Deltagare.SelectCommand = new MySqlCommand("SELECT * FROM Namn", conn); // Väljer ID och Namn från tabellen Namn
                Deltagare.Fill(myData, "Namn");

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
            // ******************************************************************************************************
        }


        private void Importera_knapp_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Random_knapp_Click(object sender, RoutedEventArgs e)
        {
            //Slump slump; // Deklarerar klassen Slump

            //slump = new Slump(); // Anropar klassen Slump.cs

            //slump.KorSlump(); // Kör metoden KorSlump i Slump.cs

        }
        //Lägga till en person i textboxen
        private void add_person_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();

            //string item = textBox1.Text;

            //comboPeople.Items.Add(item);

            //addPeople secondForm = new addPeople();

            //if (secondForm.ShowDialog() == DialogResult)
            //{
            //    comboPeople.Items.Add(secondForm.comboPeople.ToArray());
            //}

        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            //string path = @"G:\Programmering i Grafiskmiljö\PROJEKT\Kryss\bin\Debug";
            //if (!File.Exists(path))
            //{
            //FileStream fs = File.Create(path);
            //fs.Close();
            //}
            //using (TextWriter tw = File.AppendText(path))
            //{
            //foreach (string item in comboPeople.Items)
            //tw.WriteLine(item);



        }

        int sv;

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]", AntalFragor.Text))
            {

                MessageBox.Show("Please enter only numbers.");
                AntalFragor.Text.Remove(AntalFragor.Text.Length - 1);
            }
            else
            {
                sv = int.Parse(AntalFragor.Text);
                MessageBox.Show("Värdet är:" + sv);
            }
        }

        private void comboPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x_len = 30; // x_len and y_len can be any size >= 0
            int y_len = 4;
    
           

            CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            List<checkedBoxIte> item = new List<checkedBoxIte>();

            for (int x = 0; x <= checkBoxes.GetUpperBound(0); x++)
            {
                DataGridCheckBoxColumn col = new DataGridCheckBoxColumn();
                DataGridTextColumn hihi = new DataGridTextColumn();
                
               
                col.Header = x.ToString();
                hihi.Header = sv.ToString();
                dataGrid1.Columns.Add(col);
               
           
                for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Tag = String.Format("x={1}/y={1}", x, y);
                    checkBoxes[x, y] = cb;
                    
            }
         
       
        }
            for (int i = 0; i < 4; i++)
            {
                checkedBoxIte ite = new checkedBoxIte();
                ite.MyString = i.ToString();
                item.Add(ite);
            }
        dataGrid1.ItemsSource = item;
                    
                }
            }
    public class checkedBoxIte
    {
        public string MyString { get; set; }
        public bool MyBool { get; set; }
    }
               

}



            



        


    




