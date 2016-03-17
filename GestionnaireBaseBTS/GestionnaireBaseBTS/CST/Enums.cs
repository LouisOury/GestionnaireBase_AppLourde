using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionnaireBaseBTS.CST
{
    class Enums
    {
    }

    public enum EnumNameSQL
    {
        [Description("localhost")]
        localhost,
    }

    public enum EnumTypeBase
    {
        [Description("Production")]
        Client_Production = 1,

        [Description("Formation")]
        Client_Formation = 2,

        [Description("Recette")]
        Client_Recette = 3,        

        [Description("Debug")]
        Client_Debug = 4
    }
}
