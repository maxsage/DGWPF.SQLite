using DG.WPF.SQLite.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.DataService
{
    public class ParameterDataService 
    {
        private static ParameterDataService _current = null;
        public static ParameterDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new ParameterDataService();

                return _current;
            }
        }


        public Parameter GetParameterById(string dbPath, int Id)
        {
            var parameter = new Parameter();

            // Get answers from SQLite
            using (var db = new SQLiteConnection(dbPath))
            {
                parameter = db.Query<Parameter>("SELECT * FROM Parameter WHERE Id = " +
                    Id.ToString()).FirstOrDefault();
            }
            return parameter;
        }


        public List<Parameter> GetParameters(string dbPath)
        {
            var parameters = new List<Parameter>();

            using (var db = new SQLiteConnection(dbPath))
            {
                parameters = db.Query<Parameter>("SELECT * FROM Parameter");
            }
           return parameters;
        }
    }
}