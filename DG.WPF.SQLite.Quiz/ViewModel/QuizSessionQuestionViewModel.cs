using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class QuizSessionQuestionViewModel : ViewModelBase
    {
        private static readonly string QuestionPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        private static readonly string QuestionPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        private const string IdPropertyName = "Id";
        private int _id;

        /// <summary>
        /// Sets and gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set(IdPropertyName, ref _id, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="QuizSessionId" /> property's name.
        /// </summary>
        public const string QuizSessionIdPropertyName = "QuizSessionId";
        private int _quizSessionId;

        /// <summary>
        /// Sets and gets the QuizSessionId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int QuizSessionId
        {
            get
            {
                return _quizSessionId;
            }
            set
            {
                Set(IdPropertyName, ref _quizSessionId, value);
                IsDirty = true;
            }
        }

        ///// <summary>
        ///// The <see cref="QuestionId" /> property's name.
        ///// </summary>
        //public const string QuestionIdPropertyName = "QuestionId";
        //private int _questionId;

        ///// <summary>
        ///// Sets and gets the QuestionId property.
        ///// Changes to that property's value raise the PropertyChanged event. 
        ///// </summary>
        //public int QuestionId
        //{
        //    get
        //    {
        //        return _questionId;
        //    }
        //    set
        //    {
        //        Set(IdPropertyName, ref _questionId, value);
        //        IsDirty = true;
        //    }
        //}

        /// <summary>
        /// The <see cref="DisplayNumber" /> property's name.
        /// </summary>
        private const string DisplayNumberPropertyName = "DisplayNumber";

        private int _displayNumber;

        /// <summary>
        /// Sets and gets the DisplayNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int DisplayNumber
        {
            get
            {
                return _displayNumber;
            }
            set
            {
                Set(DisplayNumberPropertyName, ref _displayNumber, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="TimeStarted" /> property's name.
        /// </summary>
        private const string TimeStartedPropertyName = "TimeStarted";

        private DateTime? _timeStarted;

        /// <summary>
        /// Sets and gets the TimeStarted property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? TimeStarted
        {
            get
            {
                return _timeStarted;
            }
            set
            {
                Set(TimeStartedPropertyName, ref _timeStarted, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="TimeFinished" /> property's name.
        /// </summary>
        private const string TimeFinishedPropertyName = "TimeFinished";

        private DateTime? _timeFinished;

        /// <summary>
        /// Sets and gets the TimeFinished property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? TimeFinished
        {
            get
            {
                return _timeFinished;
            }
            set
            {
                Set(TimeFinishedPropertyName, ref _timeFinished, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="CorrectlyAnswered" /> property's name.
        /// </summary>
        private const string CorrectlyAnsweredPropertyName = "CorrectlyAnswered";

        private bool? _correctlyAnswered;

        /// <summary>
        /// Sets and gets the CorrectlyAnswered property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool? CorrectlyAnswered
        {
            get
            {
                return _correctlyAnswered;
            }
            set
            {
                Set(CorrectlyAnsweredPropertyName, ref _correctlyAnswered, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SequenceNumber" /> property's name.
        /// </summary>
        private const string SequenceNumberPropertyName = "SequenceNumber";

        private int _sequenceNumber;

        /// <summary>
        /// Sets and gets the SequenceNumber property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SequenceNumber
        {
            get
            {
                return _sequenceNumber;
            }
            set
            {
                Set(SequenceNumberPropertyName, ref _sequenceNumber, value);
                IsDirty = true;
            }
        }



        /// <summary>
        /// The <see cref="QuestionTypeId" /> property's name.
        /// </summary>
        private const string QuestionTypeIdPropertyName = "QuestionTypeId";

        private int _questionTypeId = 0;

        /// <summary>
        /// Sets and gets the QuestionTypeId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int QuestionTypeId
        {
            get
            {
                return _questionTypeId;
            }
            set
            {
                Set(QuestionTypeIdPropertyName, ref _questionTypeId, value);
                IsDirty = true;
            }
        }

    /// <summary>
        /// The <see cref="Description" /> property's name.
        /// </summary>
        public const string DescriptionPropertyName = "Description";

        private string _description = "";

        /// <summary>
        /// Sets and gets the Description property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                Set(DescriptionPropertyName, ref _description, value);
                IsDirty = true;

            }
        }

        /// <summary>
        /// The <see cref="DescriptionHtml" /> property's name.
        /// </summary>
        private const string DescriptionHtmlPropertyName = "DescriptionHtml";

        private string _descriptionHtml = "";

        /// <summary>
        /// Sets and gets the DescriptionHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DescriptionHtml
        {
            get
            {
                return _descriptionHtml;
            }
            set
            {
                Set(DescriptionHtmlPropertyName, ref _descriptionHtml, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SubCategoryId" /> property's name.
        /// </summary>
        public const string SubCategoryIdPropertyName = "SubCategoryId";

        private int _subCategoryId = 0;

        /// <summary>
        /// Sets and gets the SubCategoryId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SubCategoryId
        {
            get
            {
                return _subCategoryId;
            }
            set
            {
                Set(SubCategoryIdPropertyName, ref _subCategoryId, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="Difficulty" /> property's name.
        /// </summary>
        public const string DifficultyPropertyName = "Difficulty";

        private int _difficulty = 0;

        /// <summary>
        /// Sets and gets the Difficulty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                Set(DifficultyPropertyName, ref _difficulty, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="Score" /> property's name.
        /// </summary>
        public const string ScorePropertyName = "Score";

        private int _score = 0;

        /// <summary>
        /// Sets and gets the Score property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                Set(ScorePropertyName, ref _score, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfoId" /> property's name.
        /// </summary>
        public const string AdditionalInfoIdPropertyName = "AdditionalInfoId";

        private int _additionalInfoId = 0;

        /// <summary>
        /// Sets and gets the AdditionalInfoId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AdditionalInfoId
        {
            get
            {
                return _additionalInfoId;
            }
            set
            {
                Set(AdditionalInfoIdPropertyName, ref _additionalInfoId, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        private const string IsDirtyPropertyName = "IsDirty";
        private bool _isDirty;

        /// <summary>
        /// Gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                if (_isDirty == value)
                {
                    return;
                }

                _isDirty = value;
                RaisePropertyChanged(IsDirtyPropertyName);
            }
        }

        /// <summary>
        /// Gets the DescriptionFullHtml property.
        /// </summary>
        public string DescriptionFullHtml
        {
            get
            {
                return
                    QuestionPreHtml +
                    DescriptionHtml +
                    QuestionPostHtml;
            }
        }


        /// <summary>
        /// The <see cref="Answers" /> property's name.
        /// </summary>
        public const string AnswersPropertyName = "Answers";

        private IList<Answer> _answers;

        /// <summary>
        /// Gets and sets the Answers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IList<Answer> Answers
        {
            get
            {
                return _answers;
            }
            set
            {
                Set(AnswersPropertyName, ref _answers, value);
            }
        }
    }
}