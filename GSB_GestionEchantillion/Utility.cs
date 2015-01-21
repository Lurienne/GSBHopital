using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace GSBHopital
{
    internal class Utility
    {
        internal static string GetConnectionString()
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["GSB"];
            if (settings != null) returnValue = settings.ConnectionString;
            return returnValue;
        }
    }
}
