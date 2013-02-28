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
        bool stopChange = false; // False tills man anget antal uppgifter
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
            RandomName doRand = new RandomName(); //Går in i random funktionen
            doRand.Start(load); //Uppdaterar databasen och hämtar deltagare

            int ShowRnd = RandomName.finishComboPeople; //Hämtar det slumpade värdet
            dataGrid1.SelectedIndex = ShowRnd; //markerar den slumpade personen

            if (stopChange == true)
            {
                NrOfQuestions.Visibility = System.Windows.Visibility.Hidden;
                Uppdatera.Visibility = System.Windows.Visibility.Hidden;
            }
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
                NrOfQuestions.Text = "Endast siffror!"; // Skriv ut om annat än siffror. Funderar på att anropa en metod som tar bort texten efter X sekunder och skickar till GotFocus igen??
            }
            // Anropar funktionen Uppdatera_Click vid enter
            else if (e.Key == Key.Enter)
            {
                Uppdatera_Click(null, null); // Tvungen att skicka med sender, KeyEventArgs = orkar inte så null
            }
        }

        // Kollar först så att rutan inte är tom eller innehållet ordet Endast siffror / Antal uppgifter
        private void Uppdatera_Click(object sender, RoutedEventArgs e)
        {
            if (NrOfQuestions.Text != "" && NrOfQuestions.Text != "Endast siffror!" && NrOfQuestions.Text != "Antal uppgifter")
            {
                inMatNr = Convert.ToInt32(NrOfQuestions.Text); // Inmatat nr 

                if (inMatNr <= 30) // Spärr för att varna om mer än 30 uppgifter
                {
                    data(); // Anropar data metoden
                }
                // Varning dyker upp med ja / nej 
                else if (MessageBox.Show("Är du säker på att du vill skapa " + inMatNr + " st uppgifter?", "Kontroll", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    data(); // Anropar data metoden
                }
                // Gör att när man klickar på slump så döljs antal uppgift inmating
                stopChange = true;
            }
        }

        // Anropar och skriver ut kryssrutor beroende på antalet deltagare och uppgifter
        private void data()
        {
            dataGrid1.DataContext = null; // Nollar dataGrid1
            dataGrid1.ItemsSource = null; // Nollar källan för dataGrid1
            dataGrid1.ItemsSource = load.UpdateDatabase(inMatNr).Tables["Namn"].DefaultView; // Hämtar deltagare och antal kryssrutor igen
            dataGrid1.Items.Refresh(); // Laddar om dataGrid1

        }
    }
}