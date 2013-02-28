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

        // Tömmer boxen när man markerar den
        private void NrOfQuestions_GotFocus(object sender, RoutedEventArgs e)
        {
            if (NrOfQuestions.Text != "")
            {
                NrOfQuestions.Text = String.Empty;
            }
        }
        // Kollar inmatat värde om det är endast siffror och man trycker på enter
        private void NrOfQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Enter)
            {
                e.Handled = true;
                NrOfQuestions.Text = "Endast siffror!"; // Skriv ut om annat än siffror
            }
            // Skicka vidare till NrOfQuestions_LostFocus vid enter
            else
            {
                NrOfQuestions_LostFocus(null, null);
            }
        }

        private void NrOfQuestions_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NrOfQuestions.Text != "" && NrOfQuestions.Text != "Endast siffror!")
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