using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kryss
{
    class CheckBoxes
    {
        int sv;

        public void OK_Click(object sender, RoutedEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch("[^0-9]", AntalFragor.Text))
            {

                MessageBox.Show("Please enter only numbers.");
                AntalFragor.Text.Remove(AntalFragor.Text.Length - 1);
            }
            else
            {
                sv = int.Parse(AntalFragor.Text);
                MessageBox.Show("Värdet är:" + sv);
            }
        }

        private void comboPeople_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int x_len = 30; // x_len and y_len can be any size >= 0
            int y_len = 4;



            CheckBox[,] checkBoxes = new CheckBox[x_len, y_len];
            List<checkedBoxIte> item = new List<checkedBoxIte>();

            for (int x = 0; x <= checkBoxes.GetUpperBound(0); x++)
            {
                DataGridCheckBoxColumn col = new DataGridCheckBoxColumn();
                DataGridTextColumn hihi = new DataGridTextColumn();


                col.Header = x.ToString();
                hihi.Header = sv.ToString();
                dataGrid1.Columns.Add(col);


                for (int y = 0; y <= checkBoxes.GetUpperBound(1); y++)
                {
                    CheckBox cb = new CheckBox();
                    cb.Tag = String.Format("x={1}/y={1}", x, y);
                    checkBoxes[x, y] = cb;

                }


            }
            for (int i = 0; i < 4; i++)
            {
                checkedBoxIte ite = new checkedBoxIte();
                ite.MyString = i.ToString();
                item.Add(ite);
            }
            dataGrid1.ItemsSource = item;

        }
        public class checkedBoxIte
        {
            public string MyString { get; set; }
            public bool MyBool { get; set; }
        }
    }
}
