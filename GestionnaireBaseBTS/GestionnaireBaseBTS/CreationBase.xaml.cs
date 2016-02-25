using GestionnaireBaseBTS.CST;
using GestionnaireBaseBTS.OV;
using GestionnaireBaseBTS.RG;
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
    /// Logique d'interaction pour CreationBase.xaml
    /// </summary>
    public partial class CreationBase : Window
    {
        private CreationBase creationBase;
        internal OVClient ovClient;

        internal List<OVClient> lstClient = new List<OVClient>();
        private OVAgent ovAgent;

        internal EnumTypeBase typeBase = EnumTypeBase.Client_Debug;
        internal EnumNameSQL nameSQL = EnumNameSQL.SQL9901;

        public IEnumerable<string> lstEnumTypeBase { get { return RGEnums.GetDescriptions(typeof(EnumTypeBase)); } }
        public IEnumerable<string> lstEnumNameSQL { get { return RGEnums.GetDescriptions(typeof(EnumNameSQL)); } }

        public CreationBase(OVClient _ovClient, EnumTypeBase _typeBase, List<OVClient> _lstClient, OVAgent _ovAgent)
        {
            InitializeComponent();

            ovClient = _ovClient;
            typeBase = _typeBase;
            lstClient = _lstClient;
            ovAgent = _ovAgent;

            //InitialiserLesValeurs();
            //AbonnementAuxEvenements();
            //InitialisationComposants();
        }

        private void InitialisationComposants()
        {
            throw new NotImplementedException();
        }

        private void AbonnementAuxEvenements()
        {
            throw new NotImplementedException();
        }

        private void InitialiserLesValeurs()
        {
            //creationBase = new CreationBase();
            //this.creationBase.tbMail.Text = ovAgent.EmailAgent;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
        }
    }
}
