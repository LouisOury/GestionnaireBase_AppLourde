using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.OV
{
    public class OVClient
    {
        #region Membres
        private int identifiantClient;
        private string nomClient;
        private string rueClient;
        private string cPClient;
        private string villeClient;
        private string emailClient;
        private string telephoneClient;
        private string sQLClient;
        private string dateCreationBase;
        private int idBaseOrigine;
        private int identifiantTypeBase;
        private int identifiantAgent;

        private OVAgent ovAgent = new OVAgent();
        private OVTypeBase ovTypeBase = new OVTypeBase();
        private List<OVSuiviClientAgent> lstOvSuiviClientAgent = new List<OVSuiviClientAgent>();
        #endregion

        #region Propriétés
        public int IdentifiantClient { get { return identifiantClient; } set { identifiantClient = value; } }
        public string NomClient { get { return nomClient; } set { nomClient = value; } }
        public string RueClient { get { return rueClient; } set { rueClient = value; } }
        public string CPClient { get { return cPClient; } set { cPClient = value; } }
        public string VilleClient { get { return villeClient; } set { villeClient = value; } }
        public string EmailClient { get { return emailClient; } set { emailClient = value; } }
        public string TelephoneClient { get { return telephoneClient; } set { telephoneClient = value; } }
        public string SQLClient { get { return sQLClient; } set { sQLClient = value; } }
        public string DateCreationBase { get { return dateCreationBase; } set { dateCreationBase = value; } }
        public int IdBaseOrigine { get { return idBaseOrigine; } set { idBaseOrigine = value; } }
        public int IdentifiantTypeBase { get { return identifiantTypeBase; } set { identifiantTypeBase = value; } }
        public int IdentifiantAgent { get { return identifiantAgent; } set { identifiantAgent = value; } }

        public OVTypeBase OvTypeBase { get { return ovTypeBase; } set { ovTypeBase = value; } }
        public List<OVSuiviClientAgent> LstOvSuiviClientAgent { get { return lstOvSuiviClientAgent; } set { lstOvSuiviClientAgent = value; } }
        #endregion
    }
}
