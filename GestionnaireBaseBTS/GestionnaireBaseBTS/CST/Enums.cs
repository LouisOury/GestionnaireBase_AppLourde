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
        [Description("SQL99-01")]
        SQL9901,

        [Description("SQL89-04")]
        SQL8904,

        [Description("SQL99-04")]
        SQL9904
    }

    public enum EnumTypeBase
    {
        [Description("Production")]
        Client_Production = 1,

        [Description("Formation")]
        Client_Formation = 2,

        [Description("Recette")]
        Client_Recette_Maj = 3,        

        [Description("Debug")]
        Client_Debug = 4
    }
}
