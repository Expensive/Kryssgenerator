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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Data;


namespace KryssGenerator
{
    public partial class MainMenu : UserControl, ISwitchable
    {
        // Variabler
        int inMatNr = -1; // Döljer uppgifter vid start
        bool stopChange = false; // False tills man anget antal uppgifter
        Databas load = null;
        Databas d = new Databas();
        private static int valkol = 0; // Ändrar vald kolumn. Börjar på 1, 2 osv

        // Publik för att man ska kunna komma åt rätt kolumn i RandomName.cs som använder valkol
        public static int valKolFromMain
        {
            get
            {
                return valkol;
            }
            set
            {
                valkol = value;
            }
        }

        public MainMenu()
        {
            InitializeComponent();
            // HUR LÄGGER MAN TILL BILDER ? 
            //string path = "Images/Alert.jpg";
            //BitmapImage bitmap = new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
            //AcceptWarningImageNrOfQuestions.Source = bitmap; 
        }


      
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            load = new Databas();
            this.DataContext = load.UpdateDatabase(0).Tables["Namn"].DefaultView; // 0 för att man är tvungen att skicka med ett "värde"
        }

        // ********************************SLUMP FUNKTION***********************************************

        // Startar slumpfunktionen
        private void Slumpa_Click(object sender, RoutedEventArgs e)
        {
            if (stopChange == true && valkol < inMatNr)
            {
                valkol++; // Börjar på 1. Ökar med 1 varje gång
                doRand(); // Kör slumpfunktion

                // Dölj inmatning och knapp vid första slump
                NrOfQuestions.Focusable = false;
                AddUser.Focusable = false;
                QRow1.Opacity = 0.5;
                QRow2.Opacity = 0.5;
            }
        }

        // Anropar RandomName klassen och utför random funktion
        private void doRand()
        {
            RandomName doRand = new RandomName(); //Går in i random funktionen

            int ID = doRand.DoRandom(load); // Skapar en lokal variabel
            int index = -1; // Sätter index tillfälligt till -1

            // Kör igenom loop och räknar antal rader tills den träffar rätt och hoppar då ur
            for (int i = 0; i < dataGrid1.Items.Count - 1; i++)
            {
                if (ID.ToString() == GetCell(dataGrid1, i, 0))
                {
                    index = i; // Sätter index till for loopens i
                    break;
                }
            }

            dataGrid1.SelectedIndex = index; // Markerar den valda personen i listan som index
        }

        // Plockar ut ett värde från en specifik rad och kolumn
        public static String GetCell(DataGrid dataGrid, int row, int column)
        {
            String cellValue = "";
            DataGridRow tempRow = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(row);
            DataRowView rowView = (DataRowView)tempRow.Item;
            cellValue = rowView[column].ToString();

            return cellValue;
        }

        // ********************************SLUT SLUMP FUNKTION***********************************************

        // ********************************LÄGG TILL DELTAGARE***********************************************

        private void AddUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddUser.Text != "")
            {
                AddUser.Text = String.Empty;
            }
        }

        private void AddUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Uppdatera_Click(null, null); // Tvungen att skicka med sender, KeyEventArgs = orkar inte så null
            }
        }

        private void AddUser_LostFocus(object sender, RoutedEventArgs e)
        {
            AddUser.Text = "Captain Awesome";
        }

        // ********************************SLUT LÄGG TILL DELTAGARE*******************************************

        // ********************************ANTAL UPPGIFTER****************************************************

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
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Enter && e.Key != Key.Tab)
            {
                e.Handled = true;
            }
            // Anropar funktionen Uppdatera_Click vid enter
            else if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Uppdatera_Click(null, null); // Tvungen att skicka med sender, KeyEventArgs = orkar inte så null
            }
            else
            {
            }
        }

        private void NrOfQuestions_LostFocus(object sender, RoutedEventArgs e)
        {
            NrOfQuestions.Text = "1, 2, 3 osv";

        }

        // ********************************SLUT ANTAL UPPGIFTER************************************************

        // ********************************GEMENSAM************************************************************

        // Kollar först så att rutan inte är tom eller innehållet ordet Endast siffror / Antal uppgifter
        private void Uppdatera_Click(object sender, RoutedEventArgs e)
        {
            if (AddUser.Text != "" && AddUser.Text != "Captain Awesome")
            {
                Databas.Command.CommandText = @"INSERT INTO Namn(Deltagare) VALUES ('" + AddUser.Text + "')";
                Databas.Connection.Open();
                Databas.Command.ExecuteNonQuery();
                Databas.Connection.Close();
                data();
                AddUser.Text = "Captain Awesome"; // Gör att man ej kan skriva samma namn flera gånger på raken
            }

            else if (NrOfQuestions.Text != "" && NrOfQuestions.Text != "1, 2, 3 osv")
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
            }

            // Gör att när man klickar på slump så döljs antal uppgift inmating och lägg till person
            stopChange = true;
        }

        // Anropar och skriver ut kryssrutor beroende på antalet deltagare och uppgifter
        private void data()
        {
            dataGrid1.DataContext = load.UpdateDatabase(inMatNr).Tables["Namn"].DefaultView; // Hämtar deltagare och antal kryssrutor igen
            dataGrid1.Items.Refresh(); // Laddar om dataGrid1
        }

        // ********************************SLUT GEMENSAM********************************************************

        // Ignorera.
        #region Event For Child Window
        private void loginWindowForm_SubmitClicked(object sender, EventArgs e)
        {
            //ShowMessageBox("Login Successful", "Welcome, " + loginForm.NameText, MessageBoxIcon.Information);

        }
        #endregion

        #region ISwitchable Members
        public void UtilizeState(object state)
        {
            throw new NotImplementedException();
        }
        #endregion

        private void delete_User_Click(object sender, RoutedEventArgs e)
        {
            Person p = new Person(); //
            p.ID1 = Convert.ToInt32(GetCell(dataGrid1, dataGrid1.SelectedIndex, 0)); //Hämtar ID:et från databasen, baserat på den markerade raden

            if (p.ID1 > -1) //Ifall det är någon rad som är markerad, så går den in i denna if-sats
            {
                d.delete_User(p); //Tar bort markerad rad från databasen

                data(); //Laddar om databasen
            }
        }
    }
}