using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionnaireBaseBTS
{
    /// <summary>
    /// Logique d'interaction pour CreateClient.xaml
    /// </summary>
    public partial class CreateAgent : Window
    {
        public CreateAgent()
        {
            InitializeComponent();
        }

        private void btnValider_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string connectionString = "SERVER=localhost" + ";" + "DATABASE=gestionbase" + ";" + "UID=root" + ";" + "PASSWORD=" + ";";
                string Query = "INSERT INTO agent (NomAgent, PrenomAgent, PseudoAgent, PasswordAgent, CiviliteAgent, EmailAgent, WindowsUser) values('" + this.tbNomAgent.Text + "','" + this.tbPrenomAgent.Text + "','" + this.tbPseudoAgent.Text + "','" + this.tbPasswordAgent.Text + "','" + this.tbCiviliteAgent.Text + "','" + this.tbEmailAgent.Text + "','" + Environment.UserName + "');";
                MySqlConnection MyConn = new MySqlConnection(connectionString);
                MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                MySqlDataReader MyReader;
                MyConn.Open();
                MyReader = MyCommand.ExecuteReader();

                this.Close();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
