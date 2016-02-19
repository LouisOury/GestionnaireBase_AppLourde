using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.OV
{
    public class OVSuiviClientAgent
    {
        #region Membres
        private int identifiantSuiviClientAgent;
        private string commentaire;
        private DateTime dateExpiration;
        private int identifiantClient;
        private int identifiantAgent;
        private bool estEnregistre = false;

        private OVAgent ovAgent = new OVAgent();
        private OVClient ovClient = new OVClient();
        #endregion

        #region Propriétés
        public int IdentifiantSuiviClientAgent { get { return identifiantSuiviClientAgent; } set { identifiantSuiviClientAgent = value; } }
        public string Commentaire { get { return commentaire; } set { commentaire = value; if (commentaire != null || commentaire != "") { estEnregistre = true; } else { estEnregistre = false; } } }
        public DateTime DateExpiration { get { return dateExpiration; } set { dateExpiration = value; } }
        public int IdentifiantClient { get { return identifiantClient; } set { identifiantClient = value; } }
        public int IdentifiantAgent { get { return identifiantAgent; } set { identifiantAgent = value; } }
        public OVAgent OvAgent { get { return ovAgent; } set { ovAgent = value; } }
        public OVClient OvClient { get { return ovClient; } set { ovClient = value; } }
        public bool EstEnregistre { get { return estEnregistre; } set { estEnregistre = value; } }
        #endregion
    }
}
