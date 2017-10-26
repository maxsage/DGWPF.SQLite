using GalaSoft.MvvmLight;
using System;
using System.Net;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AnswerViewModel : ViewModelBase
    {
        public static readonly string AnswerPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        public static readonly string AnswerPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;
    
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
                    AnswerPreHtml +
                    DescriptionHtml +
                    AnswerPostHtml;
            }
        }

         /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";


        private int _id = 0;

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
        /// The <see cref="QuestionId" /> property's name.
        /// </summary>
        public const string QuestionIdPropertyName = "QuestionId";


        private int _questionId = 0;

        /// <summary>
        /// Sets and gets the QuestionId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


        public int QuestionId
        {
            get
            {
                return _questionId;
            }
            set
            {
                Set(QuestionIdPropertyName, ref _questionId, value);
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
                if (DescriptionHtml == string.Empty)
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
        /// The <see cref="CorrectAnswer" /> property's name.
        /// </summary>
        public const string CorrectAnswerPropertyName = "CorrectAnswer";


        private bool _correctAnswer;

        /// <summary>
        /// Sets and gets the CorrectAnswer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


        public bool CorrectAnswer
        {
            get
            {
                return _correctAnswer;
            }
            set
            {
                Set(CorrectAnswerPropertyName, ref _correctAnswer, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="CreatedDate" /> property's name.
        /// </summary>
        public const string CreatedDatePropertyName = "CreatedDate";

        private DateTime? _createdDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                Set(CreatedDatePropertyName, ref _createdDate, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="CreatedBy" /> property's name.
        /// </summary>
        public const string CreatedByPropertyName = "CreatedBy";

        private string _createdBy = "";

        /// <summary>
        /// Sets and gets the CreatedBy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                Set(CreatedByPropertyName, ref _createdBy, value);
                IsDirty = true;
            }
        }


        /// <summary>
        /// The <see cref="UpdatedDate" /> property's name.
        /// </summary>
        public const string UpdatedDatePropertyName = "UpdatedDate";

        private DateTime? _updatedDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the UpdatedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? UpdatedDate
        {
            get
            {
                return _updatedDate;
            }
            set
            {
                Set(UpdatedDatePropertyName, ref _updatedDate, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="UpdatedBy" /> property's name.
        /// </summary>
        public const string UpdatedByPropertyName = "UpdatedBy";

        private string _updatedBy = "";

        /// <summary>
        /// Sets and gets the UpdatedBy property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string UpdatedBy
        {
            get
            {
                return _updatedBy;
            }
            set
            {
                Set(UpdatedByPropertyName, ref _updatedBy, value);
                IsDirty = true;
            }
        }
    }
}
