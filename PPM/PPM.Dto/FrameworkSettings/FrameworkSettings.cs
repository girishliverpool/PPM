using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPM.Dto
{
    public class FrameworkSettings
    {
        public string ConnectionString { get; set; }
        public static FrameworkSettings GetEmpty(string connectionString)
        {
            return new FrameworkSettings()
            {
                ConnectionString = connectionString
            };
        }

    }
}
