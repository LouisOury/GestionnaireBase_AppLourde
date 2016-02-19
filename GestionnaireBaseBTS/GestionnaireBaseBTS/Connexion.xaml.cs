using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection con = new MySqlConnection("Server=localhost;Database=gestionbase;Uid=root;Pwd=;");
            MySqlCommand cmd = new MySqlCommand("SELECT PseudoAgent, PasswordAgent FROM Agent", con);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);

            //Faire une boucle sur Rows[0]
            if (tbPassword.Password == dt.Rows[0]["PasswordAgent"].ToString() && tbLogin.Text == dt.Rows[0]["PseudoAgent"].ToString())
            {
                MainWindow mainWindow = new MainWindow();
                this.Close();
                mainWindow.Show();
            }
            else
            {
                MessageBox.Show("Login ou password incorrecte !");
            }

        }
    }
}
