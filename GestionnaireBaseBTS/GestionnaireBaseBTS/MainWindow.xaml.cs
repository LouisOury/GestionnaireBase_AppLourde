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
using GestionnaireBaseBTS.CST;

namespace GestionnaireBaseBTS
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool fromCoffeeMaker = false;

        public string Utilisateur = "Bonjour : " + Environment.UserName;

        private OVAgent ovAgent;
        private OVClient ovClient;
        private MainWindow mainWindow;

        private List<OVClient> lstClient = new List<OVClient>();
        private List<OVSuiviClientAgent> lstSuiviClientAgent = new List<OVSuiviClientAgent>();

        private OVClient dernierClientSelection;

        DBConnect connect = new DBConnect();

        public MainWindow()
        {
            //Initialisation
            InitializeComponent();
            EventsCommandes();

            connect.openConnect();

            this.tbUtilisateur.Text = Utilisateur;

            AlimenterListeClient();
            AlimenterListeClientAgent();

            lstSuiviClients.ItemsSource = lstClient.Where(x => x.IdentifiantTypeBase == 1);
        }

        private void AlimenterListeClient()
        {
            //Ajout nouveaux clients : nom, rue, CP, ville, email, telephone, sql
            String loadClient = "SELECT IdentifiantClient, NomClient, VilleClient, SQLClient, DateCreationBase, IdBaseOrigine, IdentifiantTypeBase FROM baseclient";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = loadClient;
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = cmd;
            cmd.Connection = connect.con;
            DataSet ds = new DataSet();
            ad.Fill(ds);
            foreach (DataRowView rowView in ds.Tables[0].DefaultView)
            {
                OVClient ovClient = new OVClient();

                ovClient.IdentifiantClient = int.Parse(rowView["IdentifiantClient"].ToString());
                ovClient.NomClient = rowView["NomClient"].ToString();
                ovClient.VilleClient = rowView["VilleClient"].ToString();
                ovClient.SQLClient = rowView["SQLClient"].ToString();
                ovClient.DateCreationBase = rowView["DateCreationBase"].ToString();
                ovClient.IdBaseOrigine = int.Parse(rowView["IdBaseOrigine"].ToString());
                ovClient.IdentifiantTypeBase = int.Parse(rowView["IdentifiantTypeBase"].ToString());

                lstClient.Add(ovClient);
            }
        }

        private void AlimenterListeClientAgent()
        {
            String loadClientAgent = "SELECT suiviclientagent.IdentifiantSuiviClientAgent, suiviclientagent.IdentifiantAgent, suiviclientagent.IdentifiantClient, suiviclientagent.Commentaire, suiviclientagent.DateExpiration, agent.WindowsUser, agent.PseudoAgent FROM suiviclientagent INNER JOIN baseclient ON suiviclientagent.IdentifiantClient = baseclient.IdentifiantClient INNER JOIN agent ON agent.IdentifiantAgent = suiviclientagent.IdentifiantAgent ";
            MySqlCommand cmd2 = new MySqlCommand();
            cmd2.CommandText = loadClientAgent;
            MySqlDataAdapter ad2 = new MySqlDataAdapter();
            ad2.SelectCommand = cmd2;
            cmd2.Connection = connect.con;
            DataSet ds2 = new DataSet();
            ad2.Fill(ds2);
            foreach (DataRowView rowView in ds2.Tables[0].DefaultView)
            {
                OVSuiviClientAgent ovSuiviClientAgent = new OVSuiviClientAgent();

                ovSuiviClientAgent.IdentifiantSuiviClientAgent = int.Parse(rowView["IdentifiantSuiviClientAgent"].ToString());
                ovSuiviClientAgent.IdentifiantAgent = int.Parse(rowView["IdentifiantAgent"].ToString());
                ovSuiviClientAgent.IdentifiantClient = int.Parse(rowView["IdentifiantClient"].ToString());
                ovSuiviClientAgent.Commentaire = rowView["Commentaire"].ToString();
                ovSuiviClientAgent.DateExpiration = Convert.ToDateTime(rowView["DateExpiration"].ToString());
                ovSuiviClientAgent.OvAgent.WindowsUser = rowView["WindowsUser"].ToString();
                ovSuiviClientAgent.OvAgent.PseudoAgent = rowView["PseudoAgent"].ToString();

                lstSuiviClientAgent.Add(ovSuiviClientAgent);
            }
        }

        private void CreerCommandeCreationBaseDebug()
        {
            CommandeCreationBaseDebug = new OVCommandeRoutee(CreationBaseDebug);
        }

        private void CreationBaseDebug()
        {
            OVClient ovClient = (OVClient)lstSuiviClients.SelectedItem;

            CreationBase creationBaseDebug = new CreationBase(ovClient, EnumTypeBase.Client_Debug, this.lstClient, ovAgent);
            creationBaseDebug.typeBase = EnumTypeBase.Client_Debug;
            this.dernierClientSelection = ovClient;
            if (creationBaseDebug.ShowDialog() == true)
            {
                AlimenterListeClient();
            }
        }

        #region Evenements
        //Créer Agent
        private void MenuAddAgent_Click(object sender, RoutedEventArgs e)
        {
            if ((MessageBox.Show("Êtes-vous sûr de vouloir créer un nouvelle agent ?", "Warning ! Ajout d'un nouvelle agent", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {
                CreateAgent createAgent = new CreateAgent();
                createAgent.ShowDialog();
            }
        }

        //Bouton Deconnexion
        private void MenuDeconnexion_Click(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            this.Close();
            connexion.Show();
        }

        //Bouton vider textbox
        private void btnEmptyTxtbox_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = String.Empty;
        }

        //Bouton création nouvelle base
        private void btnCreateBaseDebug_Click(object sender, RoutedEventArgs e)
        {
            CreationBase creationBase = new CreationBase(ovClient, EnumTypeBase.Client_Debug, this.lstClient, ovAgent);
            creationBase.Show();
        }

        //Textbox filtre client
        private void txtFilter_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.txtFilter.Text != "")
            {
                List<OVClient> listFiltre = new List<OVClient>();

                listFiltre = this.txtFilter.Text != "" ? lstClient.Where(x => (x.NomClient.ToUpper().Contains(this.txtFilter.Text.ToUpper()))).ToList() : lstClient;

                ListCollectionView ClientGroupe = new ListCollectionView(listFiltre.Where(x => x.IdentifiantTypeBase == 1).ToList());

                this.lstSuiviClients.ItemsSource = ClientGroupe;

                if (this.lstSuiviClients.Items.Count > 0)
                    this.lstSuiviClients.SelectedIndex = 0;

                this.lstSuiviClients.ItemsSource = ClientGroupe;
            }
            if (this.txtFilter.Text == "")
            {
                this.lstSuiviClients.ItemsSource = lstClient.Where(x => x.IdentifiantTypeBase == 1).ToList();
            }
        }

        //Selection client listview
        private void lstSuiviClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstSuiviClients.SelectedItem != null)
            {
                List<OVClient> lstBaseDebug = new List<OVClient>();
                lstBaseDebug = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 4)).ToList<OVClient>();

                List<OVClient> lstBaseRecette = new List<OVClient>();
                lstBaseRecette = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 3)).ToList<OVClient>();

                List<OVClient> lstBaseFormation = new List<OVClient>();
                lstBaseFormation = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 2)).ToList<OVClient>();

                dgDebug.ItemsSource = null;
                dgFormation.ItemsSource = null;
                dgRecette.ItemsSource = null;

                dgReservationDebug.ItemsSource = null;
                dgReservationRecette.ItemsSource = null;
                dgReservationFormation.ItemsSource = null;

                dgDebug.ItemsSource = lstBaseDebug;
                dgRecette.ItemsSource = lstBaseRecette;
                dgFormation.ItemsSource = lstBaseFormation;
            }
        }

        //DataGrid
        private void dgDebug_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDebug.SelectedItem != null)
            {
                OVClient ovBase = (OVClient)dgDebug.SelectedItem;

                dgReservationDebug.ItemsSource = ovBase.LstOvSuiviClientAgent;
            }
        }
        #endregion

        #region EventsCommandes
        private void EventsCommandes()
        {
            //DataGrid
            dgDebug.SelectionChanged += dgDebug_SelectionChanged;

            //DataGrid Selection
            CreerCommandeCreationBaseDebug();
        }
        #endregion

        #region Commandes
        public ICommand CommandeCreationBaseDebug { get; internal set; }
        #endregion
    }
}
