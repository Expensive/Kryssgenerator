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
            Databas dokument; // Deklarerar klassen dokument

            dokument = new Databas(); // Anropar klassen Databas.cs

            dokument.DBConnect; // Kör metoden Dokument
        }

        private void Random_knapp_Click(object sender, RoutedEventArgs e)
        {
            Slump slump; // Deklarerar klassen Slump

            slump = new Slump(); // Anropar klassen Slump.cs

            slump.KorSlump(); // Kör metoden KorSlump i Slump.cs

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
        }

       
    }



