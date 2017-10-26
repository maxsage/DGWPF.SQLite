using DG.WPF.SQLite.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace DG.WPF.SQLite.DataService
{
    public class QuizSessionQuestionDataService
    {
        private static QuizSessionQuestionDataService _current = null;
        public static QuizSessionQuestionDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new QuizSessionQuestionDataService();

                return _current;
            }
        }

        public List<QuizSessionQuestion> GetQuizSessionQuestionsByQuizSessionId(string dbPath, int quizSessionId)
        {
            var quizSessionQuestions = new List<QuizSessionQuestion>();

            using (var db = new SQLiteConnection(dbPath))
            {
                quizSessionQuestions = db.Query<QuizSessionQuestion>(
                    "SELECT * FROM QuizSessionQuestion WHERE QuizSessionId = " + 
                    quizSessionId);
            }

            return quizSessionQuestions;
        }

        public QuizSessionQuestion GetQuizSessionQuestionByComposite(string dbPath, int quizSessionId, int questionId)
        {
            var quizSessionQuestion = new QuizSessionQuestion();

            using (var db = new SQLiteConnection(dbPath))
            {
                quizSessionQuestion = db.Query<QuizSessionQuestion>(
                    "SELECT * FROM QuizSessionQuestion WHERE QuizSessionId = " +
                    quizSessionId + " AND " + "QuestionId = " + questionId).FirstOrDefault();
            }

            return quizSessionQuestion;
        }

        public void AddQuizSessionQuestion(string dbPath, QuizSessionQuestion quizSessionQuestion)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new QuizSessionQuestion()
                {
                    QuizSessionId = quizSessionQuestion.QuizSessionId,
                    QuestionId = quizSessionQuestion.QuestionId,
                    DisplayNumber = quizSessionQuestion.DisplayNumber,
                    TimeStarted = quizSessionQuestion.TimeStarted,
                    TimeFinished =  quizSessionQuestion.TimeFinished,
                    CorrectlyAnswered = quizSessionQuestion.CorrectlyAnswered
                });
            }
        }


        public void UpdateQuizSessionQuestion(string dbPath, QuizSessionQuestion quizSessionQuestion)
        {
            //TODO Trying it this way because SQLite.NET doesn't support composite keys

            var ts = "";
            if (quizSessionQuestion.TimeStarted != null)
            {
                var timeStarted = (DateTime)quizSessionQuestion.TimeStarted;
                ts = timeStarted.ToString("yyyy-MM-dd HH:mm:ss");
            }

            var tf = "";
            if (quizSessionQuestion.TimeFinished != null)
            {
                var timeFinished = (DateTime)quizSessionQuestion.TimeFinished;
                tf = timeFinished.ToString("yyyy-MM-dd HH:mm:ss");
            }

            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<QuizSessionQuestion>
                    ("UPDATE QuizSessionQuestion SET " +
                     "QuizSessionId = " + quizSessionQuestion.QuizSessionId + ", " +
                     "QuestionId = " + quizSessionQuestion.QuestionId + ", " +
                     "DisplayNumber = " + quizSessionQuestion.DisplayNumber + ", " +
                     "CorrectlyAnswered = " + (quizSessionQuestion.CorrectlyAnswered == true ? 1 : 0) + ", " +
                     "TimeStarted = '" + ts + "', " +
                     "TimeFinished = '" + tf + "' " +
                     " WHERE QuizSessionId = " + quizSessionQuestion.QuizSessionId +
                     " AND QuestionId = " + quizSessionQuestion.QuestionId);
            }
        }
    }
}
