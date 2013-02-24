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
    public partial class MainMenu : UserControl, ISwitchable
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Laddar in kopia med deltagare från databasen.
            Databas load = new Databas();
            this.DataContext = load.UpdateDatabase();
        }

        // Startar slumpfunktionen
        private void Slumpa_Click(object sender, RoutedEventArgs e)
        {
            RandomName doRand = new RandomName();
            doRand.Start();
        }

        // Skickar användaren till sidan för att lägga till eller ta bort deltagare ur listan
        private void LaggTillTaBort_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new AddRemove());
        }

        // Skickar användaren vidare till sidan för att ange antal uppgifter
        private void antalFragor_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Switcher.Switch(new Questions());
        }

        // Ignorera.
        #region Event For Child Window
        private void loginWindowForm_SubmitClicked(object sender, EventArgs e)
        {
            //ShowMessageBox("Login Successful", "Welcome, " + loginForm.NameText, MessageBoxIcon.Information);

        }

        private void registerForm_SubmitClicked(object sender, EventArgs e)
        {
        }


        #endregion

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}