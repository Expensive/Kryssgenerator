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
        System.Collections.Generic.List<checkedBoxIte> item = new System.Collections.Generic.List<checkedBoxIte>();
        public MainMenu()
        {
            // ========= Skall egentligen hämtas från Question.cs men som vanligt krånglar skiten ===========
            InitializeComponent();
            int x_len = Questions.sv +1; // x_len and y_len can be any size >= 0
            int y_len = 4;
            CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
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
            //dataGrid1.ItemsSource = item; VAD GÖR DENNA =!=!=#="¤("#=¤?
        }

        public class checkedBoxIte // klass som anger bool värdena till listan
        {
            public string MyString { get; set; }
            public bool MyBool { get; set; }
        }

        // ========= OVAN SKALL EGENTLIGEN VARA I QUESTION.CS ====================

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
    }
}