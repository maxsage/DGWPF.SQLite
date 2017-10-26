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
    public class QuizSessionDataService
    {
        private static QuizSessionDataService _current = null;
        public static QuizSessionDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new QuizSessionDataService();

                return _current;
            }
        }


        public List<QuizSession> GetQuizSessionsByPlayerSubCategoryAndQuizType(
            string dbPath, string playerId, int subCategoryId, int quizTypeId)
        {
            var quizSessions = new List<QuizSession>();

            using (var db = new SQLiteConnection(dbPath))
            {
                quizSessions = db.Query<QuizSession>(
                    "SELECT * FROM QuizSession WHERE PlayerId = '" + 
                    playerId + "' " +
                    " AND SubCategoryId = " +
                    subCategoryId +
                    " AND QuizTypeId = " +
                    quizTypeId
                    );
            }

            return quizSessions;
        }

        public QuizSession GetQuizSessionById(string dbPath, int Id)
        {
            QuizSession quizSession;

            using (var db = new SQLiteConnection(dbPath))
            {
                quizSession = db.Query<QuizSession>("SELECT * FROM QuizSession WHERE Id = " +
                    Id.ToString()).FirstOrDefault();
            }

            return quizSession;
        }

        public List<QuizSession> GetQuizSessions(string dbPath)        
        {
            List<QuizSession> quizSessions;

            using (var db = new SQLiteConnection(dbPath))
            {
                quizSessions = db.Query<QuizSession>("SELECT * FROM QuizSession");
            }

           return quizSessions;
        }

        public int AddQuizSession(string dbPath, QuizSession quizSession)
        {
            var key = 0;
            
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new QuizSession()
                    {
                        PlayerId = quizSession.PlayerId,
                        SubCategoryId = quizSession.SubCategoryId,
                        QuizTypeId = quizSession.QuizTypeId,
                        OrderQuestions = quizSession.OrderQuestions,
                        QuizStartDateTime = quizSession.QuizStartDateTime,
                        QuizEndDateTime = quizSession.QuizEndDateTime,
                        TimeTaken = quizSession.TimeTaken,
                        TotalQuestions = quizSession.TotalQuestions,
                        CorrectQuestions = quizSession.CorrectQuestions
                    });

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()"); 
            }
            return key;
        }

        public void DeleteQuizSession(string dbPath, QuizSession quizSession)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Delete<QuizSession>(quizSession.Id);
            }
        }

        public void UpdateQuizSession(string dbPath, QuizSession quizSession)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Update(quizSession);
            }
        }

    }
}
