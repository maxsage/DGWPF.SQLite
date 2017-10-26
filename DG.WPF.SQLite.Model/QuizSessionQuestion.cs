using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQLite;

namespace DG.WPF.SQLite.Model
{
    public class QuizSessionQuestion : ObservableObject
    {

        private int _quizSessionId = 0;

        /// <summary>
        /// Sets and gets the QuizSessionId property.
        /// </summary>
        //[PrimaryKey]
        //[Key][Column(Order = 0)]
        public int QuizSessionId
        {
            get
            {
                return _quizSessionId;
            }
            set
            {
                _quizSessionId = value;
            }
        }

        private int _questionId = 0;

        /// <summary>
        /// Sets and gets the QuestionId property.
        /// </summary>
        //[PrimaryKey] // SQLite attributes
        //[Key][Column(Order = 1)]
        public int QuestionId
        {
            get
            {
                return _questionId;
            }
            set
            {
                _questionId = value;
            }
        }

        private int _displayNumber = 0;

        /// <summary>
        /// Sets and gets the DisplayNumber property.
        /// </summary>
        public int DisplayNumber
        {
            get
            {
                return _displayNumber;
            }
            set
            {
                _displayNumber = value;
            }
        }

        private DateTime? _timeStarted;

        /// <summary>
        /// Sets and gets the TimeStarted property.
        /// </summary>
        public DateTime? TimeStarted
        {
            get
            {
                return _timeStarted;
            }
            set
            {
                _timeStarted = value;
            }
        }

        private DateTime? _timeFinished;

        /// <summary>
        /// Sets and gets the TimeFinished property.
        /// </summary>
        public DateTime? TimeFinished
        {
            get
            {
                return _timeFinished;
            }
            set
            {
                _timeFinished = value;
            }
        }

        private bool? _correctlyAnswered;

        /// <summary>
        /// Sets and gets the CorrectlyAnswered property.
        /// </summary>
        public bool? CorrectlyAnswered
        {
            get
            {
                return _correctlyAnswered;
            }
            set
            {
                _correctlyAnswered = value;
            }
        }
    }
}

