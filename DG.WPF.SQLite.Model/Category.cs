using GalaSoft.MvvmLight;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Model
{
    public class Category : ObservableObject
    {
        private int _Id = 0;

        /// <summary>
        /// Sets and gets the Id property.
        /// </summary>
        [PrimaryKey, AutoIncrement]
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

        private string _categoryDescription = "";

        /// <summary>
        /// Sets and gets the CategoryDescription property.
        /// </summary>
        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                _categoryDescription = value;
            }
        }

        private string _categoryNotesHtml = "";

        /// <summary>
        /// Sets and gets the CategoryNotesHtml property.
        /// </summary>
        public string CategoryNotesHtml
        {
            get
            {
                return _categoryNotesHtml;
            }
            set
            {
                _categoryNotesHtml = value;
            }
        }

        private string _additionalInfoUri = "";

        /// <summary>
        /// Sets and gets the AdditionalInfoUri property.
        /// </summary>
        public string AdditionalInfoUri
        {
            get
            {
                return _additionalInfoUri;
            }
            set
            {
                _additionalInfoUri = value;
            }
        }

        private string _additionalInfoFtpAddress = "";

        /// <summary>
        /// Sets and gets the AdditionalInfoFtpAddress property.
        /// </summary>
        public string AdditionalInfoFtpAddress
        {
            get
            {
                return _additionalInfoFtpAddress;
            }
            set
            {
                _additionalInfoFtpAddress = value;
            }
        }

        private string _createdBy = "";

        /// <summary>
        /// Sets and gets the CreatedBy property.
        /// </summary>
        public string CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        private DateTime _createdDate;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// </summary>
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        private DateTime _updatedDate;

        /// <summary>
        /// Sets and gets the AdditionalInfoFtpAddress property.
        /// </summary>
        public DateTime UpdatedDate
        {
            get
            {
                return _updatedDate;
            }
            set
            {
                _updatedDate = value;
            }
        }

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
                _updatedBy = value;
            }
        }
    }
}
