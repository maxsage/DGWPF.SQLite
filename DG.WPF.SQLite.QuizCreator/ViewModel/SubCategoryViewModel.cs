using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SubCategoryViewModel : ViewModelBase
    {
        public static readonly string SubCategoryPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        public static readonly string SubCategoryPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;
        
        public SubCategoryViewModel()
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath ;
        }

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
        /// The <see cref="SqLiteDatabasePath " /> property's name.
        /// </summary>
        public const string SqLiteDatabasePathPropertyName = "SqLiteDatabasePath ";


        private string _sqLiteDatabasePath = "";

        /// <summary>
        /// Sets and gets the SqLiteDatabasePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SqLiteDatabasePath 
        {
            get
            {
                return _sqLiteDatabasePath;
            }
            set
            {
                Set(SqLiteDatabasePathPropertyName, ref _sqLiteDatabasePath, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// Sets and gets the SubCategoryNotesFullHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SubCategoryNotesFullHtml
        {
            get
            {
                return
                    SubCategoryPreHtml +
                    SubCategoryNotesHtml +
                    SubCategoryPostHtml;
            }
        }


        /// <summary>
        /// Sets and gets the TotalQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TotalQuestions
        {
            get
            {
                var count = 0;

                var subCategoryIds = new List<int>();
                subCategoryIds.Add(Id);

                var questionDataService = new QuestionDataService();
                count = questionDataService.GetQuestionsBySubCategoryIds(SqLiteDatabasePath , subCategoryIds).Count;

                return count;
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        public const string IdPropertyName = "Id";

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
            }
        }

        /// <summary>
        /// The <see cref="CategoryId" /> property's name.
        /// </summary>
        public const string CategoryIdPropertyName = "CategoryId";

        private int _categoryId = 0;

        /// <summary>
        /// Sets and gets the CategoryId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                Set(CategoryIdPropertyName, ref _categoryId, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SubCategoryDescription" /> property's name.
        /// </summary>
        public const string SubCategoryDescriptionPropertyName = "SubCategoryDescription";

        private string _subCategoryDescription = "";

        /// <summary>
        /// Sets and gets the SubCategoryDescription property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SubCategoryDescription
        {
            get
            {
                return _subCategoryDescription;
            }
            set
            {
                Set(SubCategoryDescriptionPropertyName, ref _subCategoryDescription, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SubCategoryNotesHtml" /> property's name.
        /// </summary>
        public const string SubCategoryNotesHtmlPropertyName = "SubCategoryNotesHtml";

        private string _subCategoryNotesHtml = "";

        /// <summary>
        /// Sets and gets the SubCategoryNotesHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SubCategoryNotesHtml
        {
            get
            {
                return _subCategoryNotesHtml;
            }
            set
            {
                Set(SubCategoryNotesHtmlPropertyName, ref _subCategoryNotesHtml, value);
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