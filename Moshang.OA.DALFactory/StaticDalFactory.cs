using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Moshang.OA.EFDAL;
using Moshang.OA.IDAL;

namespace Moshang.OA.DALFactory
{
    public partial class StaticDalFactory
    {
        public static string assemblyName = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

    }
}
