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
        public static int sv = 11;
        public Questions()
        {
            // Required to initialize variables
            InitializeComponent();

        }
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