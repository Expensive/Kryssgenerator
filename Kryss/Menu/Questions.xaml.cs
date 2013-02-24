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

        // Knappen för att spara antal frågor
        public void NrSave_Click(object sender, RoutedEventArgs e)
        {
            // Kollar så att man endast matar in int.
            if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]", NrBoxes.Text))
            {
                MessageBox.Show("Skriv in endast nummer!");
                NrBoxes.Text.Remove(NrBoxes.Text.Length - 1);
            }
            else
            {
                sv = int.Parse(NrBoxes.Text);
                MessageBox.Show("Värdet är:" + sv);
            }

            //Anropar metoden som sparar antal frågor
            dt();
        }

        // Själva räknaren som kollar hur många deltagare/frågor det finns. Ska hämta sina max värden någon annanstans. 
        private void dt()
        {
            int x_len = 30; // x_len and y_len can be any size >= 0
            int y_len = 4;

            CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            System.Collections.Generic.List<checkedBoxIte> item = new System.Collections.Generic.List<checkedBoxIte>();

            for (int x = 0; x <= checkBoxes.GetUpperBound(0); x++)
            {
                DataGridCheckBoxColumn xLed = new DataGridCheckBoxColumn();
                DataGridTextColumn yLed = new DataGridTextColumn();

                xLed.Header = x.ToString();
                //dataGrid1.Columns.Add(xLed);


                for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)
                {
                    yLed.Header = y.ToString();
                    CheckBox cb = new CheckBox();
                    cb.Tag = String.Format("x={1}/y={1}", x, y);
                    checkBoxes[x, y] = cb;

                }
            }
            for (int i = 0; i < 5; i++)
            {
                checkedBoxIte ite = new checkedBoxIte();
                ite.MyString = i.ToString();
                item.Add(ite);
            }
            //dataGrid1.ItemsSource = item;
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