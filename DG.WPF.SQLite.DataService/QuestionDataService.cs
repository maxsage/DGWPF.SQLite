using DG.WPF.SQLite.Model;
using System.Collections.Generic;
using System.Linq;
using DG.WPF.SQLite.Utilities;
using SQLite;


namespace DG.WPF.SQLite.DataService
{
    public class QuestionDataService
    {
        private static QuestionDataService _current = null;
        public static QuestionDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new QuestionDataService();

                return _current;
            }
        }


        public List<Question> GetQuestionsBySubCategoryId(string dbPath, int subCategoryId)
        {
            var questions = new List<Question>();

            using (var db = new SQLiteConnection(dbPath))
            {
                questions = db.Query<Question>("SELECT * FROM Question WHERE SubCategoryId = " + 
                    subCategoryId.ToString());
            }

            return questions;
        }

        public List<Question> GetQuestionsBySubCategoryIds(string dbPath, List<int> subCategoryIds)
        {
            var questions = new List<Question>();

            var subCats = string.Join(",", subCategoryIds.Select(n => n.ToString()).ToArray());

            using (var db = new SQLiteConnection(dbPath))
            {
                questions = db.Query<Question>("SELECT * FROM Question WHERE SubCategoryId IN (" +
                    subCats + ")");
            }

            return questions;
        }

        public Question GetQuestionById(string dbPath, int questionId)
        {
            var question = new Question();

            using (var db = new SQLiteConnection(dbPath))
            {
                question = db.Query<Question>("SELECT * FROM Question WHERE Id = " +
                    questionId.ToString()).FirstOrDefault();
            }

            return question;
        }

        public List<Question> GetQuestions(string dbPath)
        {
            var questions = new List<Question>();

            using (var db = new SQLiteConnection(dbPath))
            {
                questions = db.Query<Question>("SELECT * FROM Question");
            }

           return questions;
        }

        public int AddQuestion(string dbPath, Question question)
        {
            var key = 0;
            
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new Question()
                    {
                        SequenceNumber = question.SequenceNumber,
                        QuestionTypeId = question.QuestionTypeId,
                        // Escape any quotation marks the user has entered
                        Description = question.Description.Replace("'", "''"),
                        DescriptionHtml = question.DescriptionHtml,
                        SubCategoryId = question.SubCategoryId,
                        Difficulty = question.Difficulty,
                        Score = question.Score,
                        AdditionalInfoId = question.AdditionalInfoId,
                        CreatedDate = question.CreatedDate,
                        CreatedBy = question.CreatedBy,
                        UpdatedDate = question.UpdatedDate,
                        UpdatedBy = question.UpdatedBy
                    });

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return key;
        }

        public void DeleteQuestion(string dbPath, int Id)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<Question>
                    ("DELETE FROM Question WHERE Id = " + Id);
            }

        }

        public void SaveQuestions(string dbPath, List<Question> questions)
        {
            foreach (Question question in questions)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Query<Question>
                        ("UPDATE Question SET " +
                         "SequenceNumber = '" + question.SequenceNumber + "', " +
                         "QuestionTypeId = '" + question.QuestionTypeId + "', " +
                         "Description = '" + question.Description.SQLiteStringFormat() + "', " +
                         "DescriptionHtml = '" + question.DescriptionHtml.SQLiteStringFormat() + "', " +
                         "SubCategoryId = '" + question.SubCategoryId + "', " +
                         "Difficulty = '" + question.Difficulty + "', " +
                         "Score = '" + question.Score + "', " +
                         "AdditionalInfoId = '" + question.AdditionalInfoId + "', " +
                         "CreatedDate = '" + question.CreatedDate + "', " +
                         "CreatedBy = '" + question.CreatedBy + "', " +
                         "UpdatedDate = '" + question.UpdatedDate + "', " +
                         "UpdatedBy = '" + question.UpdatedBy + "' WHERE Id = " + question.Id);
                }
            }
        }

    }
}
