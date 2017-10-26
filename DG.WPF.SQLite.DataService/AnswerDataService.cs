using DG.WPF.SQLite.Model;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace DG.WPF.SQLite.DataService
{
    public class AnswerDataService
    {
        private static AnswerDataService _current = null;
        public static AnswerDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new AnswerDataService();

                return _current;
            }
        }

        public List<Answer> GetAnswers(string dbPath)
        {
            var answers = new List<Answer>();

            using (var db = new SQLiteConnection(dbPath))
            {
                answers = db.Table<Answer>().ToList();
            }

            return answers;
        }

        public List<Answer> GetAnswersByQuestionId(string dbPath, int questionId)
        {
            var answers = new List<Answer>();

            using (var db = new SQLiteConnection(dbPath))
            {
                answers = db.Query<Answer>("SELECT * FROM Answer WHERE QuestionId = " + 
                    questionId.ToString());
            }

            return answers;
        }

        //public List<Question> GetQuestions(string dbPath)
        //{
        //    var questions = new List<Question>();

        //    using (var db = new SQLiteConnection(dbPath))
        //    {
        //        questions = db.Query<Question>("SELECT * FROM Question");
        //    }

        //   return questions;
        //}

        //public int AddAnswer(string dbPath, Answer answer)
        //{
        //    var key = 0;

        //    using (var db = new SQLiteConnection(dbPath))
        //    {
        //        db.Query<Answer>
        //            ("INSERT INTO Answer " +
        //                "(QuestionId, Description, DescriptionHtml, CorrectAnswer, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy)" +
        //                "VALUES(" +
        //                    answer.QuestionId + ", '" +
        //                    answer.Description + "', " +
        //                    answer.DescriptionHtml + "', " +
        //                    (answer.CorrectAnswer ? 1 : 0) + ", '" +
        //                    answer.CreatedDate + "', '" +
        //                    answer.CreatedBy + "', '" +
        //                    answer.UpdatedDate + "', '" +
        //                    answer.UpdatedBy + "');");

        //        key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
        //    }

        //    return key;
        //}

        public int AddAnswer(string dbPath, Answer answer)
        {
            var key = 0;

            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new Answer()
                {
                    QuestionId = answer.QuestionId,
                    Description = answer.Description,
                    DescriptionHtml = answer.DescriptionHtml,
                    CorrectAnswer = (answer.CorrectAnswer ? true : false),
                    CreatedDate = answer.CreatedDate,
                    CreatedBy = answer.CreatedBy,
                    UpdatedDate = answer.UpdatedDate,
                    UpdatedBy = answer.UpdatedBy
                });
                    

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return key;
        }

        public void DeleteAnswer(string dbPath, int Id)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<Answer>
                    ("DELETE FROM Answer WHERE Id = " + Id);
            }
        }

        //public void SaveAnswers(string dbPath, IEnumerable<Answer> answers)
        //{
        //    foreach (Answer answer in answers)
        //    {
        //        using (var db = new SQLiteConnection(dbPath))
        //        {
        //            db.Query<Answer>
        //                ("UPDATE Answer SET " +
        //                 "QuestionId = '" + answer.QuestionId + "', " +
        //                 "Description = '" + answer.Description + "', " +
        //                 "DescriptionHtml = '" + answer.DescriptionHtml + "', " +
        //                 "CorrectAnswer = " + (answer.CorrectAnswer ? 1 : 0) + ", " +
        //                 "CreatedDate = '" + answer.CreatedDate + "', " +
        //                 "CreatedBy = '" + answer.CreatedBy + "', " +
        //                 "UpdatedDate = '" + answer.UpdatedDate + "', " +
        //                 "UpdatedBy = '" + answer.UpdatedBy + "' WHERE Id = " + answer.Id);
        //        }
        //    }
        //}


        public void SaveAnswers(string dbPath, IEnumerable<Answer> answers)
        {
            foreach (var answer in answers)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Update(new Answer()
                    {
                        Id = answer.Id,
                        QuestionId = answer.QuestionId,
                        Description = answer.Description,
                        DescriptionHtml = answer.DescriptionHtml,
                        CorrectAnswer = (answer.CorrectAnswer ? true : false),
                        CreatedDate = answer.CreatedDate,
                        CreatedBy = answer.CreatedBy,
                        UpdatedDate = answer.UpdatedDate,
                        UpdatedBy = answer.UpdatedBy
                    });
                }
            }
        }
    }
}
