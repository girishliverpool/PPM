using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PPMWebApplication
{
    public static class CurrencyCodeMapper
    {
       
        public static bool IsValidCurrencyCode(string code) 
        {
            bool retval = false;

            try
            {
                var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                              .Select(x => new RegionInfo(x.LCID));


                retval = regions.Where(x => x.ISOCurrencySymbol.Equals(code.ToUpper())).Count() > 0;
            }
            catch(Exception ex)
            {
                retval = false;
            }

            return retval;

        }
    }
}