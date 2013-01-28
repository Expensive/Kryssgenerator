using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kryss
{
    /// <summary>
    /// Interaction logic for addPeople.xaml
    /// </summary>
    public partial class addPeople : Window
    {
        public List<string> comboPeople = new List<string>();
        public addPeople()
        {
            InitializeComponent();
        }

        private void cancel_Btn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void addP_Btn_Click(object sender, RoutedEventArgs e)
        {
            

            string item = txtBoxFornamn.Text + " " + txtBoxEfternamn.Text;

            comboPeople.Add(item);
            
            this.Close();
        }
    }
}
