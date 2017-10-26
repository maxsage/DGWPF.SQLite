using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQLite;

namespace DG.WPF.SQLite.Model
{
    public class QuizSession : ObservableObject
    {
        private int _Id;

        /// <summary>
        /// Sets and gets the QuizSessionId property.
        /// </summary>
        [PrimaryKey, AutoIncrement] // SQLite attributes
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private string _playerId;

        /// <summary>
        /// Sets and gets the PlayerId property.
        /// </summary>
        public string PlayerId
        {
            get
            {
                return _playerId;
            }
            set
            {
                _playerId = value;
            }
        }

        private int _subCategoryId = 0;

        /// <summary>
        /// Sets and gets the SubCategoryId property.
        /// </summary>
        public int SubCategoryId
        {
            get
            {
                return _subCategoryId;
            }
            set
            {
                _subCategoryId = value;
            }
        }

        private int _quizTypeId = 0;

        /// <summary>
        /// Sets and gets the QuizTypeId property.
        /// </summary>
        public int QuizTypeId
        {
            get
            {
                return _quizTypeId;
            }
            set
            {
                _quizTypeId = value;
            }
        }

        private bool _orderQuestions = false;

        /// <summary>
        /// Sets and gets the OrderQuestions property.
        /// </summary>
        public bool OrderQuestions
        {
            get
            {
                return _orderQuestions;
            }
            set
            {
                _orderQuestions =value;
            }
        }

        private DateTime _quizStartDateTime;

        /// <summary>
        /// Sets and gets the QuizStartDateTime property.
        /// </summary>
        public DateTime QuizStartDateTime
        {
            get
            {
                return _quizStartDateTime;
            }
            set
            {
                _quizStartDateTime = value;
            }
        }

        private DateTime? _quizEndDateTime;

        /// <summary>
        /// Sets and gets the QuizEndDateTime property.
        /// </summary>
        public DateTime? QuizEndDateTime
        {
            get
            {
                return _quizEndDateTime;
            }
            set
            {
                _quizEndDateTime = value;
            }
        }

        private int _timeTaken;

        /// <summary>
        /// Sets and gets the TimeTaken property.
        /// </summary>
        public int TimeTaken
        {
            get
            {
                return _timeTaken;
            }
            set
            {
                _timeTaken = value;
            }
        }

        private int _totalQuestions;

        /// <summary>
        /// Sets and gets the TotalQuestions property.
        /// </summary>
        public int TotalQuestions
        {
            get
            {
                return _totalQuestions;
            }
            set
            {
                _totalQuestions = value;
            }
        }

        private int _correctQuestions;

        /// <summary>
        /// Sets and gets the CorrectQuestions property.
        /// </summary>
        public int CorrectQuestions
        {
            get
            {
                return _correctQuestions;
            }
            set
            {
                _correctQuestions = value;
            }
        }
    }
}

