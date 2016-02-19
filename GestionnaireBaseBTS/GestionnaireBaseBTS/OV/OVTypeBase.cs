using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.OV
{
    public class OVTypeBase
    {
        #region Membres
        private int identifiantTypeBase;
        private string nomTypeBase;
        #endregion

        #region Propriétés
        public int IdentifiantTypeBase { get { return identifiantTypeBase; } set { identifiantTypeBase = value; } }
        public string NomTypeBase { get { return nomTypeBase; } set { nomTypeBase = value; } }
        #endregion
    }
}
