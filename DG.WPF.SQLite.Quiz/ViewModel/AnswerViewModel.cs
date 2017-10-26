using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using System;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AnswerViewModel : ViewModelBase
    {
    
        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        private const string IsDirtyPropertyName = "IsDirty";

        private bool _isDirty;

        /// <summary>
        /// Gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        private bool IsDirty
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
        /// The <see cref="AnswerId" /> property's name.
        /// </summary>
         private const string AnswerIdPropertyName = "AnswerId";


        private int _answerId = 0;

        /// <summary>
        /// Sets and gets the AnswserId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AnswerId
        {
            get
            {
                return _answerId;
            }
            set
            {
                Set(AnswerIdPropertyName, ref _answerId, value);
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


        /// <summary>
        /// The <see cref="Selected" /> property's name.
        /// </summary>
        private const string SelectedPropertyName = "Selected";


        private bool _selected;

        /// <summary>
        /// Sets and gets the Selected property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>


        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                Set(SelectedPropertyName, ref _selected, value);
                IsDirty = true;
            }
        }
    }
}
