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
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Windows.Media.Animation;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace KryssGenerator
{
    public partial class MainMenu : UserControl, ISwitchable
    {
        public MainMenu()
        {
            // ========= Skall egentligen hämtas från Question.cs men som vanligt krånglar skiten ===========
            InitializeComponent();
        }

        public class checkedBoxIte // klass som anger bool värdena till listan
        {
            public string MyString { get; set; }
            public bool MyBool { get; set; }
        }

        // Fråga om 
        // Public dataset, går det att ställa in så det blir int. DataGridView.Item Property (Int32, Int32)
        // Hur fungerar det med hämta klasser, {get; set;} osv ??

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

        private void changeNrQuestions_GotFocus(object sender, RoutedEventArgs e)
        {
            changeNrQuestions.Text = string.Empty;
            changeNrQuestions.GotFocus -= changeNrQuestions_LostFocus;
        }

        private void changeNrQuestions_LostFocus(object sender, RoutedEventArgs e)
        {
            if (changeNrQuestions.Text != "")
            {
                System.Collections.Generic.List<checkedBoxIte> item = new System.Collections.Generic.List<checkedBoxIte>();
                
                int inmatNr = int.Parse(changeNrQuestions.Text) + 1; // Inmatat nr + 1 för att skippa 0
                int y_len = 4;
                CheckBox[,] checkBoxes = new CheckBox[inmatNr, y_len];
                
                for (int x = 1; x <= checkBoxes.GetUpperBound(0); x++)//Räknar upp hur många checkbox kolumner som ska skrivas ut
                {
                    DataGridCheckBoxColumn xLed = new DataGridCheckBoxColumn();
                    DataGridTextColumn yLed = new DataGridTextColumn();

                    xLed.Header = x.ToString();

                    dataGrid1.Columns.Add(xLed);//lägger till checkbox kolumner till datagriden

                    for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)//Räknar upp hur många textbox columner som ska skrivas ut
                    {
                        yLed.Header = y.ToString();
                        CheckBox cb = new CheckBox();
                        cb.Tag = String.Format("x={1}/y={1}", x, y);
                        checkBoxes[x, y] = cb;
                    }
                }

                for (int i = 0; i < 5; i++)//lägger till items till listan
                {
                    checkedBoxIte ite = new checkedBoxIte();
                    ite.MyString = i.ToString();
                    item.Add(ite);
                }

                changeNrQuestions.Text = string.Empty; // Tömmer inmatningsfältet 
            }
        }

        private void changeNrQuestions_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                changeNrQuestions_LostFocus(null, null);
            }
        }
    }
}