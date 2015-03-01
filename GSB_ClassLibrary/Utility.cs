using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSB_ClassLibrary
{
    internal class Utility
    {
        //Recup la chaine de connexion
        internal static string GetConnectionString()
        {
            string returnValue = null;
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["GSB"];

            if (settings != null) 
                returnValue = settings.ConnectionString;

            return returnValue;

        }
    }
}
