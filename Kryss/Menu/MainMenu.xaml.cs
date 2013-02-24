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
    public partial class MainMenu 
    {
       

        
        System.Collections.Generic.List<checkedBoxIte> item = new System.Collections.Generic.List<checkedBoxIte>();
        public MainMenu()
        {
            InitializeComponent();
            int x_len = 10; // x_len and y_len can be any size >= 0
            int y_len = 4;
            CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            for (int x = 1; x <= checkBoxes.GetUpperBound(0); x++)
            {
                DataGridCheckBoxColumn xLed = new DataGridCheckBoxColumn();
                DataGridTextColumn yLed = new DataGridTextColumn();

                xLed.Header = x.ToString();
                
                dataGrid1.Columns.Add(xLed);


                for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)
                {
                    yLed.Header = y.ToString();
                    CheckBox cb = new CheckBox();
                    cb.Tag = String.Format("x={1}/y={1}", x, y);
                    checkBoxes[x, y] = cb;

                }


            }

            for (int i = 0; i < 5; i++)
            {
                checkedBoxIte ite = new checkedBoxIte();
                ite.MyString = i.ToString();
                item.Add(ite);
            }
            dataGrid1.ItemsSource = item;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        //    // Laddar in kopia med deltagare från databasen.
           Databas load = new Databas();
           this.DataContext = load.UpdateDatabase();

            // Laddar in en kopia med antal frågor från Questions.xaml.cs
            //Questions q = new Questions();
            //this.DataContext = q.dt();
        }

    }
    public class checkedBoxIte
    {
        public string MyString { get; set; }
        public bool MyBool { get; set; }
    }
}



        //public void dt()
        //{
        //    int x_len = 30; // x_len and y_len can be any size >= 0
        //    int y_len = 4;

        //    CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            

        //    for (int x = 0; x <= checkBoxes.GetUpperBound(0); x++)
        //    {
        //        DataGridCheckBoxColumn xLen = new DataGridCheckBoxColumn();
        //        DataGridTextColumn yLen = new DataGridTextColumn();

        //        xLen.Header = x.ToString();

        //        dataGrid1.Columns.Add(item);



        //        for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)
        //        {
        //            CheckBox cb = new CheckBox();
        //            cb.Tag = String.Format("x={1}/y={1}", x, y);
        //            checkBoxes[x, y] = cb;

        //            yLen.Header = y.ToString();
        //        }


        //    }
        
        //}

//        private void Slumpa_Click(object sender, RoutedEventArgs e)
//        {
//            // Inget just nu
//        }

//        // Knappar för att ta sig till andra sidor i programmet.
//        private void newGameButton_Click(object sender, System.Windows.RoutedEventArgs e)
//        {
//            Switcher.Switch(new AddRemove());
//        }

//        private void optionButton_Click(object sender, System.Windows.RoutedEventArgs e)
//        {
//            Switcher.Switch(new Questions());
//        }


//        // Ignorera.
//        #region Event For Child Window
//        private void loginWindowForm_SubmitClicked(object sender, EventArgs e)
//        {
//            //ShowMessageBox("Login Successful", "Welcome, " + loginForm.NameText, MessageBoxIcon.Information);

//        }

//        private void registerForm_SubmitClicked(object sender, EventArgs e)
//        {
//        }


//        #endregion

//        #region ISwitchable Members
//        public void UtilizeState(object state)
//        {
//            throw new NotImplementedException();
//        }
//        #endregion

//    }
//}