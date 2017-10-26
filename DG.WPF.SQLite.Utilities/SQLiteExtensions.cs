using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Utilities
{
    public static class SQLiteExtensions
    {
        public static string SQLiteStringFormat(this string source)
        {
            string formattedString = "";

            try
            {
                formattedString = source.Replace("'", "''");
            }
            catch (Exception)
            { }

            return formattedString;
        }
    }
}
