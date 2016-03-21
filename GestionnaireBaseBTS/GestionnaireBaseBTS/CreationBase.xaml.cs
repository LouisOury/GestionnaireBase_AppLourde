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
        internal OVClient ovClient;

        internal List<OVClient> lstClient = new List<OVClient>();
        private OVAgent ovAgent;

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

        private void InitialisationComposants()
        {
            cbTypeBase.ItemsSource = lstEnumTypeBase;

            if (typeBase == EnumTypeBase.Client_Debug)
            {
                cbNomSQL.ItemsSource = lstEnumNameSQL.Where(p => p.Contains("localhost"));
            }
            else if (typeBase == EnumTypeBase.Client_Recette)
            {
                cbNomSQL.ItemsSource = lstEnumNameSQL.Where(p => p.Contains("localhost"));
            }
            else
            {
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }

        private void btnCreerBase_Click(object sender, RoutedEventArgs e)
        {
            OVBaseTest ovBaseTest = new OVBaseTest();
            ovBaseTest.MNomOctave = tbNomOctave.Text;
            ovBaseTest.MNomSQL = String.Format(cbNomSQL.SelectedItem.ToString());

            if (this.lstClient.Where(x => x.NomClient == ovBaseTest.MNomOctave).ToList().Count > 0)
            {
                MessageBox.Show("Impossible de créer la base. Une autre base porte le même nom.", "Attention", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if ((MessageBox.Show("Êtes-vous sûr de vouloir créer cette nouvelle base ? \r\n\n Un mail sera envoyé à : " + tbMail.Text, "Warning ! Création d'une nouvelle base", MessageBoxButton.YesNo, MessageBoxImage.Exclamation) == MessageBoxResult.Yes))
            {

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
                tmestr = @"C:\Users\lo01.octave.OCTAVE\Documents\Cours\ProjetBTS\OctaveSaasSauvegarde\" + tmestr;
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

                try
                {
                    ovClient.NomClient = DBNameNewBase;
                    string rueClient = ovClient.RueClient;
                    string cpClient = ovClient.CPClient;
                    string villeClient = ovClient.VilleClient;
                    string emailClient = ovClient.EmailClient;
                    string telephoneClient = ovClient.TelephoneClient;
                    ovClient.SQLClient = "localhost";
                    ovClient.IdBaseOrigine = ovClient.IdentifiantClient;
                    int idTypeBase = ovClient.IdentifiantTypeBase;
                    int idAgent = ovAgent.IdentifiantAgent;

                    string connectionString = "SERVER=localhost" + ";" + "DATABASE=gestionbase" + ";" + "UID=root" + ";" + "PASSWORD=" + ";";
                    string Query = @"INSERT INTO baseclient (NomClient, RueClient, CPClient, VilleClient, EmailClient, TelephoneClient, SQLClient, DateCreationBase, IdBaseOrigine, IdentifiantTypeBase, IdentifiantAgent) values('" + ovClient.NomClient + "','" + rueClient + "','" + cpClient + "','" + villeClient + "','" + emailClient + "','" + telephoneClient + "','" + ovClient.SQLClient + "', CURDATE(),'" + ovClient.IdBaseOrigine + "','" + idTypeBase + "','" + idAgent + "');";


                    MySqlConnection MyConn = new MySqlConnection(connectionString);
                    MySqlCommand MyCommand = new MySqlCommand(Query, MyConn);
                    MySqlDataReader MyReader;
                    MyConn.Open();
                    MyReader = MyCommand.ExecuteReader();
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
    }
}
