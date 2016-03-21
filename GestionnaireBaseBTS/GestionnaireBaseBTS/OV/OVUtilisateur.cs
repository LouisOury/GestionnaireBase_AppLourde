using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.OV
{
    public class OVUtilisateur
    {
        #region Membres
        private int identifiantAgent;
        private string nomAgent;
        private string prenomAgent;
        private string pseudoAgent;
        private string passwordAgent;
        private string civiliteAgent;
        private string emailAgent;
        private string windowsUser;
        #endregion

        #region Propriétés
        public int IdentifiantAgent { get { return identifiantAgent; } set { identifiantAgent = value; } }
        public string NomAgent { get { return nomAgent; } set { nomAgent = value; } }
        public string PrenomAgent { get { return prenomAgent; } set { prenomAgent = value; } }
        public string PseudoAgent { get { return pseudoAgent; } set { pseudoAgent = value; } }
        public string PasswordAgent { get { return passwordAgent; } set { passwordAgent = value; } }
        public string CiviliteAgent { get { return civiliteAgent; } set { civiliteAgent = value; } }
        public string EmailAgent { get { return emailAgent; } set { emailAgent = value; } }
        public string WindowsUser { get { return windowsUser; } set { windowsUser = value; } }
        #endregion
    }
}
