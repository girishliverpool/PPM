using PPM.Dto;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace PPM.DependencyInjection
{
    public class FrameworkSetup
    {
        public static FrameworkSettings GetSetFrameworkSettings()
        {
            FrameworkSettings frameworkSettings = null;
            if (HttpContext.Current.Session == null)
            {
                frameworkSettings = BuildFrameworkSettings();
            }
            else
            {
                frameworkSettings = (FrameworkSettings)HttpContext.Current.Session["FrameworkSettings"];
                if (frameworkSettings == null)
                {
                    frameworkSettings = BuildFrameworkSettings();
                    HttpContext.Current.Session["FrameworkSettings"] = frameworkSettings;
                }
            }
            return frameworkSettings;
        }

        private static FrameworkSettings BuildFrameworkSettings()
        {
            return new FrameworkSettings()
            {
                ConnectionString = ConfigurationManager.ConnectionStrings["KPMGEntities"].ConnectionString,
            };
        }
        
    }
}
