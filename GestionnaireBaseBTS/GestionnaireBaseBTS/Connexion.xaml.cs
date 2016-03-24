using GestionnaireBaseBTS.OV;
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
        private List<OVAgent> lstAgent = new List<OVAgent>();

        DBConnect connect = new DBConnect();

        public Connexion()
        {
            InitializeComponent();

            connect.openConnect();

            //AlimenterListeAgent();


        }

        private void AlimenterListeAgent()
        {
            String loadAgent = "SELECT PseudoAgent, PasswordAgent FROM Agent WHERE PseudoAgent = @pseudoAgent and PasswordAgent = @passwordAgent";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Parameters.AddWithValue("@pseudoAgent", this.tbLogin.Text);
            cmd.Parameters.AddWithValue("@passwordagent", this.tbPassword.Password);
            MySqlDataReader myLoginReader = cmd.ExecuteReader();
            cmd.CommandText = loadAgent;
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = cmd;
            cmd.Connection = connect.con;
            DataSet ds = new DataSet();
            ad.Fill(ds);
            foreach (DataRowView rowView in ds.Tables[0].DefaultView)
            {
                OVAgent ovAgent = new OVAgent();
                ovAgent.PseudoAgent = rowView["PseudoAgent"].ToString();
                ovAgent.PasswordAgent = rowView["PasswordAgent"].ToString();

                lstAgent.Add(ovAgent);
            }
        }

        private void btnConnexion_Click(object sender, RoutedEventArgs e)
        {
            using (MySqlCommand myLoginCommand = new MySqlCommand("SELECT PseudoAgent, PasswordAgent FROM Agent WHERE PseudoAgent = @PseudoAgent and PasswordAgent = @PasswordAgent", connect.con))
            {
                myLoginCommand.Parameters.AddWithValue("PseudoAgent", tbLogin.Text);
                myLoginCommand.Parameters.AddWithValue("PasswordAgent", tbPassword.Password);

                MySqlDataReader myLoginReader = myLoginCommand.ExecuteReader();
                

                if (myLoginReader.Read())
                {
                    MainWindow mainWindow = new MainWindow();
                    this.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Login ou password incorrecte !");
                }
                myLoginReader.Close();
            }
            connect.con.Close();
        }
    }
}
