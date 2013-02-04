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

namespace KryssGenerator
{
    public partial class Questions : UserControl, ISwitchable
    {
        public Questions()
        {
            // Required to initialize variables
            InitializeComponent();
        }
        
        // Variabler som behövs. 
        int sv;
        public DataGrid dataGrid1;

        // Kollar så att inmatat är ett nummer. Borde kanske ligga i en annan klass.
        public void OK_Click(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]", NrBoxes.Text))
            {

                MessageBox.Show("Please enter only numbers.");
                NrBoxes.Text.Remove(NrBoxes.Text.Length - 1);
            }
            else
            {
                sv = int.Parse(NrBoxes.Text);
                MessageBox.Show("Värdet är:" + sv);
            }
        }

        // Själva räknaren som kollar hur många deltagare/frågor det finns. Ska hämta sina max värden någon annanstans. 
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
        public class checkedBoxIte
        {
            public string MyString { get; set; }
            public bool MyBool { get; set; }
        }

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