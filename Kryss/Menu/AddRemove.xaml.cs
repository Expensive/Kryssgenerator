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
    public partial class AddRemove : UserControl, ISwitchable
    {
        public string chattextbox;

        public AddRemove()
        {
            InitializeComponent();
        }

        public void Uppdatera_Click(object sender, RoutedEventArgs e)
        {
            //Databas uppdatera = new Databas();
            //uppdatera.ChangePerson(); // Redigerar deltagare

            //string pass = TextBoxAdd.Text;
            //sql = new SqlConnection(@"Data Source=PC-PC\PC;Initial Catalog=P3;Integrated Security=True");
            //SqlCommand cmd = new SqlCommand();
            //cmd.Connection = sql;
            //cmd.CommandText = ("Insert [MyTable] ([MyColumn]) Values (@pass)");
            //cmd.Parameters.AddWithValue("@pass", pass);
            //sql.Open();
            //cmd.ExecuteNonQuery();
            //sql.Close();

            //OpenConn("INSERT INTO Namn (Deltagare) VALUES (@Deltagare)");

            //try
            //{
            //    Deltagare.SelectCommand = Command;
            //    cmdIns.Parameters.AddWithValue("@Deltagare", chattextbox);
            //    cmdIns.ExecuteNonQuery();

            //    cmdIns.Parameters.Clear();
            //    cmdIns.CommandText = "SELECT @@IDENTITY";

            //    // Get the last inserted id.
            //    int insertID = Convert.ToInt32(cmdIns.ExecuteScalar());

            //    cmdIns.Dispose();
            //    cmdIns = null;
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    UpdateDatabase();
            //}
        }

        //Ignorera.
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

        public void TextBoxAdd_TextChanged(object sender, TextChangedEventArgs e)
        {
            chattextbox = TextBoxAdd.Text;
        }
    }
}