using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace KryssGenerator
{
    public partial class AddRemove : UserControl, ISwitchable
    {
        public AddRemove()
        {
            InitializeComponent();
        }

        private void Uppdatera_Click(object sender, RoutedEventArgs e)
        {
            Databas uppdatera = new Databas();
            uppdatera.ChangePerson(); // Redigerar deltagare
        }

        //Ignorera.
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