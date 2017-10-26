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
    public class QuizPackViewModel : ViewModelBase
    {
    
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
        /// The <see cref="SqLiteDatabasePath" /> property's name.
        /// </summary>
        public const string SqLiteDatabasePathPropertyName = "SqLiteDatabasePath";


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
                Set(DescriptionPropertyName, ref _sqLiteDatabasePath, value);
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
