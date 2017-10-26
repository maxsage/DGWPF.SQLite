using GalaSoft.MvvmLight;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Model
{
    public class AdditionalInfo : ObservableObject
    {
        private int _id;

        /// <summary>
        /// Sets and gets the Id property.
        /// </summary>
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        private int _subCategoryId;

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

        private int _sequenceNumber;

        /// <summary>
        /// Sets and gets the SequenceNumber property.
        /// </summary>
        public int SequenceNumber
        {
            get
            {
                return _sequenceNumber;
            }
            set
            {
                _sequenceNumber = value;
            }
        }

        private string _sectionDescription;

        /// <summary>
        /// Sets and gets the SequenceNumber property.
        /// </summary>
        public string SectionDescription
        {
            get
            {
                return _sectionDescription;
            }
            set
            {
                _sectionDescription = value;
            }
        }

        private string _additionalInfoHtml;

        /// <summary>
        /// Sets and gets the AdditionalInfoHtml property.
        /// </summary>
        public string AdditionalInfoHtml
        {
            get
            {
                return _additionalInfoHtml;
            }
            set
            {
                _additionalInfoHtml = value;
            }
        }

        private string _additionalInfoUri;

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

        private bool _displayInBrowser;

        /// <summary>
        /// Sets and gets the DisplayInBrowser property.
        /// </summary>
        public bool DisplayInBrowser
        {
            get
            {
                return _displayInBrowser;
            }
            set
            {
                _displayInBrowser =  value;
            }
        }

        private string _createdBy;

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
