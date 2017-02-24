using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace PPM.Dto
{
    public static class Extensions
    {
        public static Guid ToGuid(this string stringValue)
        {
            try
            {
                return new Guid(stringValue);
            }
            catch (FormatException ex)
            {
                throw new FormatException("string could not convert to Guid");
            }
            catch (ArgumentNullException ex)
            {

                return Guid.Empty;
            }
        }

        public static int ToInt32(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return -0;
            }
            else
            {
                return Convert.ToInt32(stringValue);
            }
        }

        public static bool ToBoolean(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return false;
            }
            else
            {
                return Convert.ToBoolean(stringValue.ToInt32());
            }
        }

        public static string ToProperCase(this string stringValue)
        {
            if (string.IsNullOrEmpty(stringValue))
            {
                return string.Empty;
            }
            else
            {
                var ti = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo;
                return ti.ToTitleCase(stringValue.ToLower());
            }
        }

        public static bool IsNull(this object o)
        {
            return o == null;
        }

        public static bool IsNotNull(this object o)
        {
            return !o.IsNull();
        }

        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        public static bool IsNotNullOrEmpty(this string s)
        {
            return !string.IsNullOrEmpty(s);
        }

        public static string ToValidFileName(this string value)
        {
            return Regex.Replace(Regex.Replace(value, @"\W", "_"), "_{2,}", "_");
        }

        public static DataSet ToDataSet<T>(this IList<T> value)
        {
            if (value != null && value.Count > 0)
            {
                var type = typeof(T);
                var properties = type.GetProperties();
                ConstructorInfo ci = type.GetConstructor(new Type[] { });
                T item = (T)ci.Invoke(new object[] { });
                var ds = new DataSet();
                var dt = new DataTable();
                foreach (PropertyInfo property in properties)
                {
                    var row = value.FirstOrDefault();
                    var dc = new DataColumn()
                    {
                        Caption = property.Name,
                        ColumnName = property.Name
                    };
                    if (row.IsNotNull())
                    {
                        var itemValue = property.GetValue(row, null);
                        if (itemValue.IsNotNull())
                        {
                            dc.DataType = itemValue.GetType();
                        }
                    }
                    dt.Columns.Add(dc);
                }
                foreach (T listItem in value)
                {
                    var dr = dt.NewRow();
                    foreach (var property in properties)
                    {
                        var listItemValue = property.GetValue(listItem, null);
                        if (listItemValue != null)
                        {
                            dr[property.Name] = property.GetValue(listItem, null);
                        }
                    }
                    dt.Rows.Add(dr);
                }
                ds.Tables.Add(dt);
                return ds;
            }
            else
            {
                return new DataSet();
            }
        }

        /* Added for template builder */
        public static Object GetPropValue(this Object obj, String name)
        {
            foreach (String part in name.Split('.'))
            {
                if (obj == null) { return null; }

                Type type = obj.GetType();
                PropertyInfo info = type.GetProperty(part);
                if (info == null) { return null; }

                obj = info.GetValue(obj, null);
            }
            return obj;
        }

        public static T GetPropValue<T>(this Object obj, String name)
        {
            Object retval = GetPropValue(obj, name);
            if (retval == null) { return default(T); }

            // throws InvalidCastException if types are incompatible
            return (T)retval;
        }
        /* END Added for template builder */
    }
}
