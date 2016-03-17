using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.OV
{
    public class OVBaseTest
    {
        private string vModeCreation;
        private string vLogin;
        private string vPassword;
        private string mTypeBase;
        private string mSave;
        private string mNomOctave;
        private string mNomSQL;
        private OVClient ovClient;

        public string VModeCreation { get { return vModeCreation; } set { vModeCreation = value; } }

        public string VLogin { get { return vLogin; } set { vLogin = value; } }

        public string VPassword { get { return vPassword; } set { vPassword = value; } }

        public string MTypeBase { get { return mTypeBase; } set { mTypeBase = value; } }

        public string MSave { get { return mSave; } set { mSave = value; } }

        public string MNomOctave { get { return mNomOctave; } set { mNomOctave = value; } }

        public string MNomSQL { get { return mNomSQL; } set { mNomSQL = value; } }

        public OVClient OvClient { get { return ovClient; } set { ovClient = value; } }
    }
}
