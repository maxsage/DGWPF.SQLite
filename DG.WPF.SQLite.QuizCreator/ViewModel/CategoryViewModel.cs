using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CategoryViewModel : ViewModelBase
    {
        public static readonly string CategoryPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        public static readonly string CategoryPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;

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
        /// Sets and gets the CategoryNotesFullHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CategoryNotesFullHtml
        {
            get
            {
                return
                    CategoryPreHtml +
                    CategoryNotesHtml +
                    CategoryPostHtml;
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
        /// The <see cref="CategoryDescription" /> property's name.
        /// </summary>
        public const string CategoryDescriptionPropertyName = "CategoryDescription";
        private string _categoryDescription = "";

        /// <summary>
        /// Sets and gets the CategoryDescription property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                Set(CategoryDescriptionPropertyName, ref _categoryDescription, value);
                IsDirty = true;
                
            }
        }

        /// <summary>
        /// The <see cref="CategoryNotesHtml" /> property's name.
        /// </summary>
        public const string CategoryNotesHtmlPropertyName = "CategoryNotesHtml";
        private string _categoryNotesHtml = "";

        /// <summary>
        /// Sets and gets the CategoryNotesHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CategoryNotesHtml
        {
            get
            {
                return _categoryNotesHtml;
            }
            set
            {
                Set(CategoryNotesHtmlPropertyName, ref _categoryNotesHtml, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfoUri" /> property's name.
        /// </summary>
        public const string AdditionalInfoUriPropertyName = "AdditionalInfoUri";
        private string _additionalInfoUri = "";

        /// <summary>
        /// Sets and gets the AdditionalInfoUri property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoUri
        {
            get
            {
                return _additionalInfoUri;
            }
            set
            {
                Set(AdditionalInfoUriPropertyName, ref _additionalInfoUri, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfoFtpAddress" /> property's name.
        /// </summary>
        public const string AdditionalInfoFtpAddressPropertyName = "AdditionalInfoFtpAddress";
        private string _additionalInfoFtpAddress = "";

        /// <summary>
        /// Sets and gets the AdditionalInfoFtpAddress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoFtpAddress
        {
            get
            {
                return _additionalInfoFtpAddress;
            }
            set
            {
                Set(AdditionalInfoFtpAddressPropertyName, ref _additionalInfoFtpAddress, value);
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
        /// The <see cref="CreatedDate" /> property's name.
        /// </summary>
        public const string CreatedDatePropertyName = "CreatedDate";
        private DateTime _createdDate;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime CreatedDate
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
        /// The <see cref="UpdatedDate" /> property's name.
        /// </summary>
        public const string UpdatedDatePropertyName = "UpdatedDate";
        private DateTime _updatedDate;

        /// <summary>
        /// Sets and gets the AdditionalInfoFtpAddress property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime UpdatedDate
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
        public const string UpdatedByPropertyName = "UpddatedBy";
        private string _updatedBy = "";

        /// <summary>
        /// Sets and gets the AdditionalInfoFtpAddress property.
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