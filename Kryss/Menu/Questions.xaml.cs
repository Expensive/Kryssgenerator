using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace KryssGenerator
{

    public partial class Questions : UserControl, ISwitchable
    {
        public static int sv = 11;
        public Questions()
        {
            // Required to initialize variables
            InitializeComponent();
        }
        public void NrSave_Click(object sender, RoutedEventArgs e)
        {

            // Kollar så att man endast matar in int.
            if (System.Text.RegularExpressions.Regex.IsMatch(@"^-*[0-9,\.]+$", NrBoxes.Text))
            {
                MessageBox.Show("Skriv in endast nummer!");
                NrBoxes.Text.Remove(NrBoxes.Text.Length - 1);
            }
            else
            {
                sv = int.Parse(NrBoxes.Text);
                MessageBox.Show("Värdet är:" + sv);
            }
            AddCheckboxes();
        }

        private void AddCheckboxes()
        {
            //int x_len = Questions.sv +1; // x_len and y_len can be any size >= 0
            //int y_len = 4;
            //CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            //for (int x = 1; x <= checkBoxes.GetUpperBound(0); x++)//Räknar upp hur många checkbox kolumner som ska skrivas ut
            //{
            //    DataGridCheckBoxColumn xLed = new DataGridCheckBoxColumn();
            //    DataGridTextColumn yLed = new DataGridTextColumn();

            //    xLed.Header = x.ToString();
                
            //    dataGrid1.Columns.Add(xLed);//lägger till checkbox kolumner till datagriden

            //    for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)//Räknar upp hur många textbox columner som ska skrivas ut
            //    {
            //        yLed.Header = y.ToString();
            //        CheckBox cb = new CheckBox();
            //        cb.Tag = String.Format("x={1}/y={1}", x, y);
            //        checkBoxes[x, y] = cb;
            //    }
            //}

            //for (int i = 0; i < 5; i++)//lägger till items till listan
            //{
            //    checkedBoxIte ite = new checkedBoxIte();
            //    ite.MyString = i.ToString();
            //    item.Add(ite); 
            //}
            ////dataGrid1.ItemsSource = item; VAD GÖR DENNA =!=!=#="¤("#=¤?
        }
        // Ignorera.
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