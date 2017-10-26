using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;
using System.Net;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class QuestionViewModel : ViewModelBase
    {
        public static readonly string QuestionPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        public static readonly string QuestionPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;

        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        public const string IsDirtyPropertyName = "IsDirty";

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
        /// Sets and gets the DescriptionFullHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
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
        /// The <see cref="Id" /> property's name.
        /// </summary>
        private const string IdPropertyName = "Id";

        private int _id;

        /// <summary>
        /// Sets and gets the QuestionId property.
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
        /// The <see cref="SequenceNumber" /> property's name.
        /// </summary>
        public const string SequenceNumberPropertyName = "SequenceNumber";

        private int _sequenceNumber = 0;

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
        public const string QuestionTypeIdPropertyName = "QuestionTypeId";

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
                // If the HTML Description is empty we will duplicate what is in the description
                // but with <p> tags around it and HTMLEncoded.
                if(DescriptionHtml == string.Empty)
                {
                    DescriptionHtml = "<p>" + WebUtility.HtmlEncode(Description) + "</p>";
                }

                IsDirty = true;

            }
        }

        /// <summary>
        /// The <see cref="DescriptionHtml" /> property's name.
        /// </summary>
        public const string DescriptionHtmlPropertyName = "DescriptionHtml";

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