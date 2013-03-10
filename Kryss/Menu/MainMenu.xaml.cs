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
        //Variabler
        int inMatNr = -1; //Döljer uppgifter vid start
        bool stopChange = false; //False tills man anget antal uppgifter
        //Databas load = null;
        Databas d = new Databas();
        private static int valkol = 0; //Ändrar vald kolumn. Börjar på 1, 2 osv

        //Publik för att man ska kunna komma åt rätt kolumn i RandomName.cs som använder valkol
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
            Slumpa.IsEnabled = false;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            d = new Databas();
            //0 för att man är tvungen att skicka med ett "värde"
            this.DataContext = d.UpdateDatabase(0).Tables["Namn"].DefaultView;
        }

        // ********************************SLUMP FUNKTION***********************************************

        // Startar slumpfunktionen
        private void Slumpa_Click(object sender, RoutedEventArgs e)
        {
            if (stopChange == true && valkol < inMatNr)
            {
                //Börjar på 1. Ökar med 1 varje gång
                valkol++;
                
                //Kör slumpfunktion
                doRand();

                // Räknar vilken uppgift man ska slumpa på
                Slumpa.Content = "Slumpa\nUppgift " + (valkol +1);

                //Dölj inmatning och knapp vid första slump
                NrOfQuestions.Focusable = false;
                AddUser.Focusable = false;
                QRow1.Opacity = 0.5;
                QRow2.Opacity = 0.5;
                //Döljer ta bort knappen vid första slump
            }
            if (valkol == inMatNr)
            {
                // Ändrar texten på knappen
                Slumpa.Content = "Slumpning\nKlar";
                Slumpa.IsEnabled = false;
            }
        }

        //Anropar RandomName klassen och utför random funktion
        private void doRand()
        {

            //Går in i random funktionen
            RandomName doRand = new RandomName(); 
            
            //Skapar en lokal variabel
            int ID = doRand.DoRandom(d);
            //Sätter index tillfälligt till -1
            int index = -1;

            //Kör igenom loop och räknar antal rader tills den träffar rätt och hoppar då ur
            for (int i = 0; i < dataGrid1.Items.Count - 1; i++)
            {
                if (ID.ToString() == GetCell(dataGrid1, i, 0))
                {
                    //Sätter index till for loopens i
                    index = i;
                    break;
                }
            }

            //Markerar den valda personen i listan som index
            dataGrid1.SelectedIndex = index;
        }

        //Plockar ut ett värde från en specifik rad och kolumn
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

        private void Add_User()
        {
            //Kollar först så att rutan inte är tom eller innehållet ordet Endast siffror / Antal uppgifter
            if (AddUser.Text != "" && AddUser.Text != "Captain Awesome")
            {
                //Lägger till deltagare i tabellen Namn, vars namn är ifyllt i textboxen
                Databas.Command.CommandText = @"INSERT INTO Namn(Deltagare) VALUES ('" + AddUser.Text + "')";
                Databas.Connection.Open();
                Databas.Command.ExecuteNonQuery();
                Databas.Connection.Close();
                //Laddar om databasen på nytt genom metoden data
                data();
                //Gör att man ej kan skriva samma namn flera gånger på raken
                AddUser.Text = "";
            }

            else if (NrOfQuestions.Text != "" && NrOfQuestions.Text != "1, 2, 3 osv")
            {
                //Inmatat nr
                inMatNr = Convert.ToInt32(NrOfQuestions.Text);

                //Spärr för att varna om mer än 30 uppgifter
                if (inMatNr <= 30)
                {
                    //Laddar om databasen på nytt genom metoden data
                    data();
                }
                //Varning dyker upp med ja / nej
                else if (MessageBox.Show("Är du säker på att du vill skapa " + inMatNr + " st uppgifter?", "Kontroll", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    //Laddar om databasen på nytt genom metoden data
                    data();
                }
                NrOfQuestions.Text = "";
                NrOfQuestionsWarning.Content = "";
                AcceptAlertImageNrOfQuestions.Visibility = Visibility.Hidden;
                Slumpa.IsEnabled = true;
            }

            // Gör att när man klickar på slump så döljs antal uppgift inmating och lägg till person
            stopChange = true;
        }

        private void AddUser_GotFocus(object sender, RoutedEventArgs e)
        {
            if (AddUser.Text != "")
            {
                AddUser.Text = String.Empty;
            }
        }

        private void AddUser_KeyDown(object sender, KeyEventArgs e)
        {
            //Kollar så att man endast matar in bokstäver
            if (Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Enter && e.Key != Key.Tab)
            {
                e.Handled = true;
                AcceptAlertImageAddUser.Visibility = Visibility.Visible;
                this.AcceptAlertImageAddUser.Source = AlertImage();
                AddUserWarning.Content = "Ofta du heter något på 1-10 ..";
                AddUserWarning.Foreground = Brushes.Red;
            }
            
            //Grön bock och text när man matar in text
            else if (AddUser.Text != " ")
            {
                AcceptAlertImageAddUser.Visibility = Visibility.Visible;
                this.AcceptAlertImageAddUser.Source = AcceptImage();
                AddUserWarning.Content = "Looking good!";
                AddUserWarning.Foreground = Brushes.Green;
            }

            if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                //Anropar metoden Add_User, för att lägga till användare
                Add_User();
                AcceptAlertImageAddUser.Visibility = Visibility.Hidden;
                AddUserWarning.Content = "";
            }
        }

        private void AddUser_LostFocus(object sender, RoutedEventArgs e)
        {
            AddUser.Text = "Captain Awesome";
            AcceptAlertImageAddUser.Visibility = Visibility.Hidden;
            AddUserWarning.Content = "";
        }

        // ********************************SLUT LÄGG TILL DELTAGARE*******************************************

        // ********************************ANTAL UPPGIFTER****************************************************

        
        private void NrOfQuestions_GotFocus(object sender, RoutedEventArgs e)
        {
            // Tömmer boxen när man markerar den
            if (NrOfQuestions.Text != "")
            {
                NrOfQuestions.Text = String.Empty;
            }
        }
        
        private void NrOfQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            //Kollar inmatat värde om det är endast siffror och man trycker på enter
            if (!Char.IsDigit((char)KeyInterop.VirtualKeyFromKey(e.Key)) && e.Key != Key.Enter && e.Key != Key.Tab)
            {
                e.Handled = true;
                AcceptAlertImageNrOfQuestions.Visibility = Visibility.Visible;
                this.AcceptAlertImageNrOfQuestions.Source = AlertImage();
                NrOfQuestionsWarning.Content = "Endast siffor!";
                NrOfQuestionsWarning.Foreground = Brushes.Red;
            }
            
            //Anropar metoden Add_User, för att lägga till användare, vid enter
            else if (e.Key == Key.Enter || e.Key == Key.Tab)
            {
                Add_User();
            }
            else
            {
                NrOfQuestionsWarning.Content = "Ser bra ut!";
                NrOfQuestionsWarning.Foreground = Brushes.Green;
                AcceptAlertImageNrOfQuestions.Visibility = Visibility.Visible;
                this.AcceptAlertImageNrOfQuestions.Source = AcceptImage();
            }
        }

        private void NrOfQuestions_LostFocus(object sender, RoutedEventArgs e)
        {
            NrOfQuestions.Text = "1, 2, 3 osv";
            AcceptAlertImageNrOfQuestions.Visibility = Visibility.Hidden;
            NrOfQuestionsWarning.Content = "";
        }

        // ********************************SLUT ANTAL UPPGIFTER************************************************

        // ********************************GEMENSAM************************************************************

        private void data()
        {
            //Hämtar deltagare och antal kryssrutor igen
            dataGrid1.DataContext = d.UpdateDatabase(inMatNr).Tables["Namn"].DefaultView;
            // Laddar om dataGrid1
            dataGrid1.Items.Refresh();
        }

        // ********************************SLUT GEMENSAM********************************************************

        // ********************************TAR BORT ANVÄNDARE***************************************************
        private void delete_User_Click(object sender, RoutedEventArgs e)
        {
            //Skapar en lokal objekt(p), utav klassen Person
            Person p = new Person();
            //Hämtar ID:et från databasen, baserat på den markerade raden
            p.ID1 = Convert.ToInt32(GetCell(dataGrid1, dataGrid1.SelectedIndex, 0));

            //Ifall det är någon rad som är markerad, så går den in i denna if-sats
            if (p.ID1 > -1)
            {
                //Tar bort markerad rad från databasen
                d.delete_User(p);
                //Laddar om databasen på nytt genom metoden data
                data();
            }
        }

        // ********************************SLUT TAR BORT ANVÄNDARE**********************************************

        // ********************************BILD KÄLLOR**********************************************************

        // Anger källa för Alert bilden
        private ImageSource AlertImage()
        {
            Uri uri = new Uri("/Images/Alert.jpg", UriKind.Relative);
            ImageSource myImage = new BitmapImage(uri);
            return myImage;
        }
        // Anger källa för Accept bilden
        private ImageSource AcceptImage()
        {
            Uri uri = new Uri("/Images/Accept.jpg", UriKind.Relative);
            ImageSource myImage = new BitmapImage(uri);
            return myImage;
        }

        // ********************************SLUT BILD KÄLLOR*****************************************************

        private void delete_AllUser_Click(object sender, RoutedEventArgs e)
        {
            //Varning dyker upp med ja / nej
            if (MessageBox.Show("OBS! Detta gör att tabellen töms på deltagare.", "Kontroll", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                //Kör metoden för att ta bort alla användare från databasen
                d.delete_AllUser();
                //Laddar om databasen på nytt genom metoden data
                data();
            }
        }

        private void StartaOmProgram_Click(object sender, RoutedEventArgs e)
        {
            //Varning dyker upp med ja / nej
            if (MessageBox.Show("OBS! Programmet startas om, deltagarna sparas.", "Kontroll", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                // Startar om hela programmet. 
                System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                Application.Current.Shutdown();
            }
        }

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
    }
}