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
        // Variabler
        int inMatNr = -1; // Döljer uppgifter vid start
        Databas load = null;

        public MainMenu()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            // Laddar in kopia med deltagare från databasen.
            load = new Databas();
            this.DataContext = load.UpdateDatabase(0); // 0 för att man är tvungen att skicka med ett "värde"
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

        private void NrOfQuestions_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NrOfQuestions.Text != "")
            {
                NrOfQuestions.Text = String.Empty;
            }
            //NrOfQuestions.GotFocus -= NrOfQuestions_LostFocus;
        }

        private void NrOfQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Enter)
            {
                e.Handled = true;
                NrOfQuestions.Text = "Endast siffror!";
            }
            else if (e.Key == Key.Enter)
            {
                NrOfQuestions_LostFocus(null, null);
            }
        }

        private void NrOfQuestions_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NrOfQuestions.Text != "")
            {
                inMatNr = Convert.ToInt32(NrOfQuestions.Text); // Inmatat nr 
                dataGrid1.DataContext = null; // Nollar dataGrid1
                dataGrid1.ItemsSource = null; // Nollar källan för dataGrid1
                dataGrid1.ItemsSource = load.UpdateDatabase(inMatNr).Tables["Namn"].DefaultView; // Hämtar deltagare och antal kryssrutor igen
                dataGrid1.Items.Refresh(); // Laddar om dataGrid1
            }
            
        }
    }
}
