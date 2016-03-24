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
using System.IO;

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

        private List<OVClient> lstClient = new List<OVClient>();
        private List<OVAgent> lstAgent = new List<OVAgent>();
        private List<OVSuiviClientAgent> lstSuiviClientAgent = new List<OVSuiviClientAgent>();

        private OVClient dernierClientSelection;

        DBConnect connect = new DBConnect();

        public MainWindow()
        {
            InitializeComponent();
            EventsCommandes();

            connect.openConnect();

            this.tbUtilisateur.Text = Utilisateur;

            ChargerAgent();
            AlimenterListeClient();
            AlimenterListeClientAgent();

            lstSuiviClients.ItemsSource = lstClient.Where(x => x.IdentifiantTypeBase == 1);
        }

        #region Fonction
        private void ChargerAgent()
        {
            String loadAgent = "SELECT IdentifiantAgent, NomAgent, PrenomAgent, PseudoAgent, EmailAgent, WindowsUser FROM agent";
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = loadAgent;
            MySqlDataAdapter ad = new MySqlDataAdapter();
            ad.SelectCommand = cmd;
            cmd.Connection = connect.con;
            DataSet ds = new DataSet();
            ad.Fill(ds);
            foreach (DataRowView rowView in ds.Tables[0].DefaultView)
            {
                OVAgent ovAgent = new OVAgent();

                ovAgent.IdentifiantAgent = int.Parse(rowView["IdentifiantAgent"].ToString());
                ovAgent.NomAgent = rowView["NomAgent"].ToString();
                ovAgent.PrenomAgent = rowView["PrenomAgent"].ToString();
                ovAgent.PseudoAgent = rowView["PseudoAgent"].ToString();
                ovAgent.EmailAgent = rowView["EmailAgent"].ToString();
                ovAgent.WindowsUser = rowView["WindowsUser"].ToString();


                lstAgent.Add(ovAgent);
            }
            ovAgent = lstAgent.Where(x => x.WindowsUser == Environment.UserName).First();
        }

        private void AlimenterListeClient()
        {
            String loadClient = "SELECT IdentifiantClient, NomClient, RueClient, CPClient, VilleClient, EmailClient, TelephoneClient, SQLClient, DateCreationBase, IdBaseOrigine, IdentifiantTypeBase, baseclient.IdentifiantAgent, PseudoAgent FROM baseclient INNER JOIN agent ON baseclient.IdentifiantAgent = agent.IdentifiantAgent";
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
                ovClient.RueClient = rowView["RueClient"].ToString();
                ovClient.CPClient = rowView["CPClient"].ToString();
                ovClient.VilleClient = rowView["VilleClient"].ToString();
                ovClient.EmailClient = rowView["EmailClient"].ToString();
                ovClient.TelephoneClient = rowView["TelephoneClient"].ToString();
                ovClient.SQLClient = rowView["SQLClient"].ToString();
                ovClient.DateCreationBase = Convert.ToDateTime(rowView["DateCreationBase"].ToString());
                ovClient.IdBaseOrigine = int.Parse(rowView["IdBaseOrigine"].ToString());
                ovClient.IdentifiantTypeBase = int.Parse(rowView["IdentifiantTypeBase"].ToString());
                ovClient.IdentifiantAgent = int.Parse(rowView["IdentifiantAgent"].ToString());

                ovClient.OvAgent.PseudoAgent = rowView["PseudoAgent"].ToString();

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

        private void SupprimerBase()
        {
            OVClient ovClientSelect = new OVClient();
            if (dgDebug.SelectedItem != null)
            {
                ovClientSelect = (OVClient)dgDebug.SelectedItem;
            }
            else if (dgRecette.SelectedItem != null)
            {
                ovClientSelect = (OVClient)dgRecette.SelectedItem;
            }
            else if (dgFormation.SelectedItem != null)
            {
                ovClientSelect = (OVClient)dgFormation.SelectedItem;
            }

            string DatabaseName = ovClientSelect.NomClient;
            string connectionString = "SERVER=localhost; DATABASE=" + DatabaseName + "; UID=root; PASSWORD=;";
            string connectionString2 = "SERVER=localhost; DATABASE=gestionbase; UID=root; PASSWORD=;";

            if ((MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette base ?", "Warning ! Suppression d'une base", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {
                try
                {
                    //Supprime la base de données selectionnée
                    #region DROP DATABASE
                    string Query = @"DROP DATABASE " + DatabaseName + ";";
                    MySqlConnection MyConn = new MySqlConnection(connectionString);
                    MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                    MySqlDataReader MyReader;
                    MyConn.Open();
                    MyReader = MyCommand.ExecuteReader();
                    MyConn.Close();
                    #endregion

                    //Supprime les lignes dans les tables 'BaseClient' et 'SuiviClientAgent' de la base 'GestionBase'
                    #region DELETE FROM gestionbase
                    string Query2 = @"DELETE FROM suiviclientagent WHERE Commentaire Like '%" + DatabaseName + "%';";
                    MySqlConnection MyConn2 = new MySqlConnection(connectionString2);
                    MySqlCommand MyCommand2 = new MySqlCommand(Query2, MyConn2);
                    MySqlDataReader MyReader2;
                    MyConn2.Open();
                    MyReader2 = MyCommand2.ExecuteReader();
                    MyConn2.Close();

                    string Query3 = @"DELETE FROM baseclient WHERE NomClient Like '%" + DatabaseName + "%';";
                    MyCommand2 = new MySqlCommand(Query3, MyConn2);
                    MySqlDataReader MyReader3;
                    MyConn2.Open();
                    MyReader3 = MyCommand2.ExecuteReader();
                    MyConn2.Close();
                    #endregion

                    //Supprime la fichier .sql associé a la base présent dans le repertoire suivant 
                    if (File.Exists(@"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\Sauvegarde_DebugRecetteFormation\" + DatabaseName + ".sql"))
                    {
                        File.Delete(@"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\Sauvegarde_DebugRecetteFormation\" + DatabaseName + ".sql");
                    }

                    MessageBox.Show("Base supprimée correctement !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion


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

        //Bouton Quitter (avec deconnexion BDD)
        private void MenuQuitter_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            connect.closeConnect();
        }

        //Bouton vider textbox
        private void btnEmptyTxtbox_Click(object sender, RoutedEventArgs e)
        {
            txtFilter.Text = String.Empty;
        }

        //Bouton création nouvelle base
        private void btnCreateBaseDebug_Click(object sender, RoutedEventArgs e)
        {
            if (lstSuiviClients.SelectedItem == null)
            {
                MessageBox.Show("Selectionner une base client !");
            }
            else
            {
                OVClient ovClient = (OVClient)lstSuiviClients.SelectedItem;

                CreationBase creationBaseDebug = new CreationBase(ovClient, EnumTypeBase.Client_Debug, this.lstClient, ovAgent);
                creationBaseDebug.typeBase = EnumTypeBase.Client_Debug;
                this.dernierClientSelection = ovClient;
                creationBaseDebug.Show();
            }
        }

        private void btnCreateBaseRecette_Click(object sender, RoutedEventArgs e)
        {
            if (lstSuiviClients.SelectedItem == null)
            {
                MessageBox.Show("Selectionner une base client !");
            }
            else
            {
                OVClient ovClient = (OVClient)lstSuiviClients.SelectedItem;

                CreationBase creationBaseRecette = new CreationBase(ovClient, EnumTypeBase.Client_Recette, this.lstClient, ovAgent);
                creationBaseRecette.typeBase = EnumTypeBase.Client_Recette;
                this.dernierClientSelection = ovClient;
                creationBaseRecette.Show();
            }
        }

        private void btnCreateBaseFormation_Click(object sender, RoutedEventArgs e)
        {
            if (lstSuiviClients.SelectedItem == null)
            {
                MessageBox.Show("Selectionner une base client !");
            }
            else
            {
                OVClient ovClient = (OVClient)lstSuiviClients.SelectedItem;

                CreationBase creationBaseRecette = new CreationBase(ovClient, EnumTypeBase.Client_Formation, this.lstClient, ovAgent);
                creationBaseRecette.typeBase = EnumTypeBase.Client_Formation;
                this.dernierClientSelection = ovClient;
                creationBaseRecette.Show();
            }
        }

        //Bouton Suppression Base
        private void BtnDeleteBaseDebug_Click(object sender, RoutedEventArgs e)
        {
            SupprimerBase();
        }

        private void BtnDeleteBaseRecette_Click(object sender, RoutedEventArgs e)
        {
            SupprimerBase();
        }

        private void BtnDeleteBaseFormation_Click(object sender, RoutedEventArgs e)
        {
            SupprimerBase();
        }

        //Bouton "mes bases"
        private void btnMesBases_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Format("Mes bases ({0}) : ", ovAgent.PseudoAgent));
            sb.AppendLine("");
            foreach (OVClient ovClient in lstClient)
            {
                if (ovClient.OvAgent.PseudoAgent == ovAgent.PseudoAgent)
                {
                    sb.AppendLine(String.Format("- {0}", ovClient.NomClient));
                }
            }

            MessageBox.Show(sb.ToString(), "Mes bases de tests", MessageBoxButton.OK);
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
                //Base DEBUG
                List<OVClient> lstBaseDebug = new List<OVClient>();
                lstBaseDebug = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 4)).ToList<OVClient>();
                lstBaseDebug = lstBaseDebug.Select(x => { x.LstOvSuiviClientAgent = (this.lstSuiviClientAgent.Where(y => y.IdentifiantClient == x.IdentifiantClient)).ToList(); return x; }).ToList();

                //Base RECETTE
                List<OVClient> lstBaseRecette = new List<OVClient>();
                lstBaseRecette = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 3)).ToList<OVClient>();
                lstBaseRecette = lstBaseRecette.Select(x => { x.LstOvSuiviClientAgent = (this.lstSuiviClientAgent.Where(y => y.IdentifiantClient == x.IdentifiantClient)).ToList(); return x; }).ToList();


                //Base FORMATION
                List<OVClient> lstBaseFormation = new List<OVClient>();
                lstBaseFormation = lstClient.Where(x => x.IdBaseOrigine == ((OVClient)lstSuiviClients.SelectedItem).IdentifiantClient && (x.IdentifiantTypeBase == 2)).ToList<OVClient>();
                lstBaseFormation = lstBaseFormation.Select(x => { x.LstOvSuiviClientAgent = (this.lstSuiviClientAgent.Where(y => y.IdentifiantClient == x.IdentifiantClient)).ToList(); return x; }).ToList();

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

        private void dgRecette_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgRecette.SelectedItem != null)
            {
                OVClient ovBase = (OVClient)dgRecette.SelectedItem;

                dgReservationRecette.ItemsSource = ovBase.LstOvSuiviClientAgent;
            }
        }

        private void dgFormation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgFormation.SelectedItem != null)
            {
                OVClient ovBase = (OVClient)dgFormation.SelectedItem;

                dgReservationFormation.ItemsSource = ovBase.LstOvSuiviClientAgent;
            }
        }
        #endregion

        #region EventsCommandes
        private void EventsCommandes()
        {
            //DataGrid
            dgDebug.SelectionChanged += dgDebug_SelectionChanged;
            dgRecette.SelectionChanged += dgRecette_SelectionChanged;
            dgFormation.SelectionChanged += dgFormation_SelectionChanged;
        }

        #endregion

    }
}
