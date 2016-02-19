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
        private OVAgent ovAgent;
        private MainWindow mainWindow;

        private List<OVClient> lstSuiviClient = new List<OVClient>();
        private List<OVSuiviClientAgent> lstSuiviClientAgent = new List<OVSuiviClientAgent>();

        private OVClient dernierClientChoisi;

        DispatcherTimer filtreTimer;

        DBConnect connect = new DBConnect();

        public MainWindow()
        {
            InitializeComponent();            
            connect.openConnect();

            InitialiserLesValeurs();

            ChargerListClients();

            //Ajout nouveaux clients : nom, rue, CP, ville, email, telephone, sql
            String loadClient = "SELECT * FROM baseclient";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = loadClient;
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = cmd;
            cmd.Connection = connect.con;
            DataSet ds = new DataSet();
            ad.Fill(ds);
            lstSuiviClients.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void InitialiserLesValeurs()
        {
            filtreTimer = new DispatcherTimer();
            filtreTimer.Interval = new TimeSpan(0, 0, 0, 0, 250);
            filtreTimer.Tick += filtretimer_Tick;
        }

        private void filtretimer_Tick(object sender, EventArgs e)
        {
            filtreTimer.Stop();
            RafraichirListClient();
        }

        private void ChargerListClients(string vLike = "")
        {
            
        }

        private void RafraichirListClient()
        {
            lstSuiviClients.ItemsSource = null;
            List<OVClient> listFiltre = new List<OVClient>();
            listFiltre = txtFilter.Text != "" ? lstSuiviClient.FindAll(x => (x.NomClient.ToUpper().Contains(txtFilter.Text.ToUpper()))) : lstSuiviClient;
            listFiltre = listFiltre.Where(x => x.OvTypeBase.NomTypeBase == "Production").ToList();
            lstSuiviClients.ItemsSource = listFiltre;
            if (listFiltre.Count > 0) { this.lstSuiviClients.SelectedIndex = 0; }
        }

        private void MenuAddClient_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Êtes-vous sûr de vouloir créer un nouveau Client ?", "Warning ! Ajout d'un nouveau client", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {
                //Client client = new Client("", "", "", "", "", "", "");
                //lstSuiviClients.Items.Add(client);

                //On affiche la fenêtre de l'élément.
                //CreateClient createClient = new CreateClient();

                //createClient.DataContext = client;

                //createClient.ShowDialog();
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
            filtreTimer.Stop();
            filtreTimer.Start();
        }
    }
}
