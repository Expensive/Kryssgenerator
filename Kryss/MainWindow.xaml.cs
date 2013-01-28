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
namespace Kryss
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            //ObservableCollection<Peoples> GPeoples;
            //var Peoples = new ObservableCollection<Person>(search.Peoples());

            //comboPeople.ItemsSource = comboP;
            //GPeoples = Peoples;

            StreamReader f = new StreamReader("Personer.txt");
            //Räknar hur många rader det är i textfilen
            int x = File.ReadAllLines("Personer.txt").Length;
            
            string[] a = null;
            Person[] people = new Person[x];
            int antal = 0;
            while (true)
            {
                string rad = f.ReadLine();
                if (rad == null)
                    break;
                a = rad.Split();
                people[antal] = new Person(a[0], a[1]);
                antal++;
            }
            InitializeComponent();
            comboPeople.ItemsSource = people;
            Binding nameBinding = new Binding("FirstName");
            lblFName.SetBinding(ContentProperty, nameBinding);
            Binding lnameBinding = new Binding("LastName");
            lbllName.SetBinding(ContentProperty, lnameBinding);
       
        }

        private void Random_knapp_Click(object sender, RoutedEventArgs e)
        {
            //Startar random funktion
           Random random = new Random();

            //Väljer mellan alla i listboxen
           int rnd = comboPeople.Items.Count;

            //Randomar vilken den väljer i listan
           int choseItem = random.Next(rnd);

            //Visar den slumpmässiga deltagaren
           comboPeople.SelectedIndex = choseItem;

        }
        //Lägga till en person i textboxen
        private void add_person_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            
            //string item = textBox1.Text;

            //comboPeople.Items.Add(item);

            addPeople secondForm = new addPeople();

            if (secondForm.ShowDialog() == DialogResult)
            {
                comboPeople.Items.Add(secondForm.comboPeople.ToArray());
            }
        
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
        }

       
    }



