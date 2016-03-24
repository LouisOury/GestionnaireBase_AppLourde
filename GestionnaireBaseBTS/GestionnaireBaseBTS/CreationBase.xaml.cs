using GestionnaireBaseBTS.CST;
using GestionnaireBaseBTS.OV;
using GestionnaireBaseBTS.RG;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace GestionnaireBaseBTS
{
    /// <summary>
    /// Logique d'interaction pour CreationBase.xaml
    /// </summary>
    public partial class CreationBase : Window
    {
        private OVClient ovClient;
        private OVAgent ovAgent;

        OVBaseTest ovBaseTest = new OVBaseTest();

        internal List<OVClient> lstClient = new List<OVClient>();
        
        internal EnumTypeBase typeBase = EnumTypeBase.Client_Debug;
        internal EnumNameSQL nameSQL = EnumNameSQL.localhost;

        public IEnumerable<string> lstEnumTypeBase { get { return RGEnums.GetDescriptions(typeof(EnumTypeBase)); } }
        public IEnumerable<string> lstEnumNameSQL { get { return RGEnums.GetDescriptions(typeof(EnumNameSQL)); } }

        public CreationBase(OVClient _ovClient, EnumTypeBase _typeBase, List<OVClient> _lstClient, OVAgent _ovAgent)
        {
            InitializeComponent();

            ovClient = _ovClient;
            typeBase = _typeBase;
            lstClient = _lstClient;
            ovAgent = _ovAgent;

            InitialisationComposants();
        }

        #region Initialisation
        private void InitialisationComposants()
        {
            cbTypeBase.ItemsSource = lstEnumTypeBase;

            if (typeBase == EnumTypeBase.Client_Debug)
            {
                cbTypeBase.SelectedIndex = 3;
                cbNomSQL.ItemsSource = lstEnumNameSQL.Where(p => p.Contains("localhost"));
            }
            else if (typeBase == EnumTypeBase.Client_Recette)
            {
                cbTypeBase.SelectedIndex = 2;
                cbNomSQL.ItemsSource = lstEnumNameSQL.Where(p => p.Contains("localhost"));
            }
            else if (typeBase == EnumTypeBase.Client_Formation)
            {
                cbTypeBase.SelectedIndex = 1;
                cbNomSQL.ItemsSource = lstEnumNameSQL.Where(p => p.Contains("localhost"));
            }

            if (typeBase != EnumTypeBase.Client_Production)
            {
                this.cbTypeBase.SelectedItem = typeBase;
                List<String> lstSauvegarde = Directory.GetFiles(@"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde", "*.sql").ToList();
                lstSauvegarde = lstSauvegarde.Select(x => x.Replace(@"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\", "").Replace(".sql", "").Replace("octave_", "")).ToList();
                cbSave.Items.Clear();
                if (lstSauvegarde.Count > 0)
                {
                    cbSave.ItemsSource = lstSauvegarde;
                    cbSave.SelectedIndex = lstSauvegarde.Count - 1;
                }
                if (lstClient.Where(x => x.NomClient.Contains(ovClient.NomClient)).Count() > 1)
                {
                    int result;
                    string dbName = lstClient.Where(x => x.NomClient.Contains(ovClient.NomClient)).OrderByDescending(x => x.DateCreationBase).First().NomClient;
                    bool numero = int.TryParse(dbName.Substring(dbName.Length - 2, 2), out result);
                    result++;
                    tbNomOctave.Text = String.Format("{0}{1:00}", ovClient.NomClient, result);
                }
                else
                {
                    tbNomOctave.Text = String.Format("{0}01", ovClient.NomClient);
                }
            }
        }
        #endregion

        #region Events
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void btnCreerBase_Click(object sender, RoutedEventArgs e)
        {
            ovBaseTest.MNomOctave = tbNomOctave.Text;
            ovBaseTest.MNomSQL = String.Format(cbNomSQL.SelectedItem.ToString());

            if (this.lstClient.Where(x => x.NomClient == ovBaseTest.MNomOctave).ToList().Count > 0)
            {
                MessageBox.Show("Impossible de créer la base. Une autre base porte le même nom.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if ((MessageBox.Show("Êtes-vous sûr de vouloir créer cette nouvelle base ? \r\n\n Un mail sera envoyé à : " + tbMail.Text, "Warning ! Création d'une nouvelle base", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {
                //Backup to a new base
                ovBaseTest.MTypeBase = cbTypeBase.SelectedItem.ToString();
                ovBaseTest.MSave = cbSave.SelectedItem.ToString();

                ovBaseTest.OvClient = ovClient;
                ovBaseTest.OvClient.IdentifiantTypeBase = (int)typeBase;

                if (tbMail.Text != "")
                {
                    ovAgent.EmailAgent = tbMail.Text;
                    if (!EmailValide(ovAgent.EmailAgent))
                    {
                        MessageBox.Show("L'adresse email n'est pas valide.");
                        return;
                    }
                }

                string DBNameNewBase = tbNomOctave.Text;
                string DBNameOldBase = "octave_" + cbSave.Text;
                string ExeLocation = @"C:\wamp\bin\mysql\mysql5.6.17\bin\mysqldump.exe";
                string tmestr = "";

                tmestr = DBNameNewBase + ".sql";
                tmestr = tmestr.Replace("/", "-");
                tmestr = @"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\Sauvegarde_DebugRecetteFormation\" + tmestr;
                StreamWriter file = new StreamWriter(tmestr);
                ProcessStartInfo proc = new ProcessStartInfo();
                string cmd = string.Format(@"-u{0} -p{1} -h{2} {3}", "root", "", "localhost", DBNameOldBase);
                proc.FileName = ExeLocation;
                proc.RedirectStandardInput = false;
                proc.RedirectStandardOutput = true;
                proc.Arguments = cmd;
                proc.UseShellExecute = false;
                Process p = Process.Start(proc);
                string res;
                res = p.StandardOutput.ReadToEnd();

                file.WriteLine(res);
                p.WaitForExit();
                file.Close();
                MessageBox.Show("Creation réussi !");

                //Create new database
                string connect = "SERVER=localhost" + ";" + "DATABASE=gestionbase" + ";" + "UID=root" + ";" + "PASSWORD=" + ";";
                MySqlConnection MySQLConn = new MySqlConnection(connect);
                MySQLConn.Open();
                string stgCreate = "CREATE DATABASE IF NOT EXISTS " + DBNameNewBase + ";";
                MySqlCommand cmd2 = new MySqlCommand(stgCreate, MySQLConn);
                cmd2.ExecuteNonQuery();
                MySQLConn.Close();

                //Restore new database
                string constring = "server=localhost;user=root;pwd=;database=" + DBNameNewBase + ";";
                string file2 = @"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\Sauvegarde_DebugRecetteFormation\" + DBNameNewBase + ".sql";
                using (MySqlConnection conn = new MySqlConnection(constring))
                {
                    using (MySqlCommand cmd3 = new MySqlCommand())
                    {
                        using (MySqlBackup mb = new MySqlBackup(cmd3))
                        {
                            cmd3.Connection = conn;
                            conn.Open();
                            mb.ImportFromFile(file2);
                            conn.Close();
                        }
                    }
                }

                //INSERT dans les tables BaseClient & SuiviClientAgent de la base GEstionBase
                try
                {
                    OVSuiviClientAgent ovSuiviClientAgent = new OVSuiviClientAgent();

                    ovClient.NomClient = DBNameNewBase;
                    string rueClient = ovClient.RueClient;
                    string cpClient = ovClient.CPClient;
                    string villeClient = ovClient.VilleClient;
                    string emailClient = ovClient.EmailClient;
                    string telephoneClient = ovClient.TelephoneClient;
                    int idTypeBase = ovClient.IdentifiantTypeBase;
                    int idAgent = ovAgent.IdentifiantAgent;
                    string commentaire = "Base de " + DBNameNewBase;

                    string connectionString = "SERVER=localhost" + ";" + "DATABASE=gestionbase" + ";" + "UID=root" + ";" + "PASSWORD=" + ";";

                    ovClient.SQLClient = "localhost";
                    ovClient.IdBaseOrigine = ovClient.IdentifiantClient;

                    string Query = @"INSERT INTO baseclient (NomClient, RueClient, CPClient, VilleClient, EmailClient, TelephoneClient, SQLClient, DateCreationBase, IdBaseOrigine, IdentifiantTypeBase, IdentifiantAgent) values('" + ovClient.NomClient + "','" + rueClient + "','" + cpClient + "','" + villeClient + "','" + emailClient + "','" + telephoneClient + "','" + ovClient.SQLClient + "', CURDATE(),'" + ovClient.IdBaseOrigine + "','" + idTypeBase + "','" + idAgent + "');";

                    MySqlConnection MyConn = new MySqlConnection(connectionString);
                    MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                    MySqlDataReader MyReader;
                    MyConn.Open();
                    MyReader = MyCommand.ExecuteReader();
                    long idClient = MyCommand.LastInsertedId;
                    MyConn.Close();

                    string Query2 = @" INSERT INTO suiviclientagent (Commentaire, DateExpiration, IdentifiantClient, IdentifiantAgent) values('" + commentaire + "', DATE_ADD(CURDATE(), INTERVAL 2 month ) ,'" + idClient + "','" + idAgent + "');";
                    MyCommand = new MySqlCommand(Query2, MyConn);
                    MySqlDataReader MyReader2;
                    MyConn.Open();
                    MyReader2 = MyCommand.ExecuteReader();
                    MyConn.Close();

                }
                catch (MySqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }

                this.Dispatcher.Invoke(new Action(() =>
                {
                    this.loadGrid.Visibility = Visibility.Collapsed;
                }), DispatcherPriority.ContextIdle);

                this.Close();
            }
        }
        #endregion

        #region Validation
        public bool EmailValide(string adresseEmail)
        {
            if (String.IsNullOrEmpty(adresseEmail))
                return false;

            try
            {
                adresseEmail = Regex.Replace(adresseEmail, @"(@)(.+)$", this.DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(adresseEmail,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private string DomainMapper(Match match)
        {
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {

            }
            return match.Groups[1].Value + domainName;
        }
        #endregion

    }
}
