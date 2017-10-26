using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Quiz.HelperClasses
{
    static class Extensions
    {
        public static string ToShortForm(this TimeSpan t)
        {
            string shortForm = "";
            if (t.Days > 0)
            {
                shortForm += string.Format("{0} days ", t.Days.ToString());
            }
            if (t.Hours > 0)
            {
                shortForm += string.Format("{0} hours ", t.Hours.ToString());
            }
            if (t.Minutes > 0)
            {
                shortForm += string.Format("{0} minutes ", t.Minutes.ToString());
            }
            if (t.Seconds > 0)
            {
                shortForm += string.Format("{0} seconds ", t.Seconds.ToString());
            }
            return shortForm;
        }
    }
}
