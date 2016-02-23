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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Data.SqlClient;
using System.Data;
using MySql.Data.MySqlClient;
using GestionnaireBaseBTS.OV;

namespace GestionnaireBaseBTS
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string Utilisateur = "Bonjour : " + Environment.UserName;

        private OVAgent ovAgent;
        private OVClient ovClient;
        private MainWindow mainWindow;

        private List<OVClient> lstClient = new List<OVClient>();
        private List<OVSuiviClientAgent> lstSuiviClientAgent = new List<OVSuiviClientAgent>();

        DBConnect connect = new DBConnect();

        public MainWindow()
        {
            InitializeComponent();
            
            connect.openConnect();

            this.tbUtilisateur.Text = Utilisateur;
            
            AlimenterListeClient();

            lstSuiviClients.ItemsSource = lstClient;
        }

        private void AlimenterListeClient()
        {
            //Ajout nouveaux clients : nom, rue, CP, ville, email, telephone, sql
            String loadClient = "SELECT NomClient, VilleClient, SQLClient FROM baseclient WHERE IdentifiantTypeBase = 1";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = loadClient;
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = cmd;
            cmd.Connection = connect.con;
            DataSet ds = new DataSet();
            ad.Fill(ds);
            foreach(DataRowView rowView in ds.Tables[0].DefaultView)
            {
                OVClient ovClient = new OVClient();
                ovClient.NomClient = rowView["NomClient"].ToString();
                ovClient.VilleClient = rowView["VilleClient"].ToString();
                ovClient.SQLClient = rowView["SQLClient"].ToString();

                lstClient.Add(ovClient);
            }
        }

        private void MenuAddClient_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Êtes-vous sûr de vouloir créer un nouvelle agent ?", "Warning ! Ajout d'un nouvelle agent", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {
                CreateAgent createAgent = new CreateAgent();
                createAgent.ShowDialog();
            }
        }

        private void MenuDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            this.Close();
            connexion.Show();
        }

        private void btnEmptyTxtbox_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = String.Empty;
        }

        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtFilter.Text != "")
            {
                List<OVClient> listFiltre = new List<OVClient>();

                listFiltre = this.txtFilter.Text != "" ? lstClient.Where(x => (x.NomClient.ToUpper().Contains(this.txtFilter.Text.ToUpper()))).ToList() : lstClient;
            
                ListCollectionView ClientGroupe = new ListCollectionView(listFiltre);
            
                this.lstSuiviClients.ItemsSource = ClientGroupe;
            
                if (this.lstSuiviClients.Items.Count > 0)
                    this.lstSuiviClients.SelectedIndex = 0;
            
                this.lstSuiviClients.ItemsSource = ClientGroupe;
            }
            if (this.txtFilter.Text == "")
            {
                this.lstSuiviClients.ItemsSource = lstClient;
            }
        }
    }
}
