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
    public class QuestionTypeDataService
    {
        private static QuestionTypeDataService _current = null;
        public static QuestionTypeDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new QuestionTypeDataService();

                return _current;
            }
        }

        public QuestionType GetQuestionTypeById(string dbPath, int Id)
        {
            var questionType = new QuestionType();

            using (var db = new SQLiteConnection(dbPath))
            {
                questionType = db.Query<QuestionType>("SELECT * FROM QuestionType WHERE Id = " +
                    Id).FirstOrDefault();
            }

            return questionType;
        }

        public List<QuestionType> GetQuestionTypes(string dbPath)
        {
            var questionTypes = new List<QuestionType>();

            using (var db = new SQLiteConnection(dbPath))
            {
                questionTypes = db.Query<QuestionType>("SELECT * FROM QuestionType");
            }

           return questionTypes;
        }
    }
}
