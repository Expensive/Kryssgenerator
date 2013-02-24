using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;


namespace KryssGenerator
{
    public partial class Questions : UserControl, ISwitchable
    {
        public Questions()
        {
            // Required to initialize variables
            InitializeComponent();
            
        }
        
  

        // Själva räknaren som kollar hur många deltagare/frågor det finns. Ska hämta sina max värden någon annanstans. 
        //public DataSet dt()
        //{     

        //    List<checkedBoxIte> item = new List<checkedBoxIte>();


            
        //    for (int i = 0; i < 5; i++)
        //    {
        //        checkedBoxIte ite = new checkedBoxIte();
        //        ite.MyString = i.ToString();
        //        item.Add(ite);
        //    }
        //    dataGrid1.ItemsSource = item;
        //}

  

        // Ignorera.
        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new MainMenu());
        }
        #endregion
    }
}