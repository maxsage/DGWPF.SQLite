using GalaSoft.MvvmLight;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Model
{
    public class Question : ObservableObject 
    {
        
        private int _Id;

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

        private int _sequenceNumber = 0;

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

        private int _questionTypeId = 0;

        /// <summary>
        /// Sets and gets the QuestionTypeId property.
        /// </summary>
        public int QuestionTypeId
        {
            get
            {
                return _questionTypeId;
            }
            set
            {
                _questionTypeId = value;
            }
        }

        private string _description = "";

        /// <summary>
        /// Sets and gets the Description property.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _descriptionHtml = "";

        /// <summary>
        /// Sets and gets the DescriptionHtml property.
        /// </summary>
        public string DescriptionHtml
        {
            get
            {
                return _descriptionHtml;
            }
            set
            {
                _descriptionHtml = value;
            }
        }

        private int _subCategoryId = 0;

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

        private int _difficulty = 0;

        /// <summary>
        /// Sets and gets the Difficulty property.
        /// </summary>
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
            }
        }

        private int _score = 0;

        /// <summary>
        /// Sets and gets the Score property.
        /// </summary>
        public int Score
        {
            get
            {
                return _score;
            }
            set
            {
                _score = value;
            }
        }

        private int _additionalInfoId = 0;

        /// <summary>
        /// Sets and gets the AdditionalInfoId property.
        /// </summary>
        public int AdditionalInfoId
        {
            get
            {
                return _additionalInfoId;
            }
            set
            {
                _additionalInfoId = value;
            }
        }

        private DateTime _createdDate = DateTime.Now;

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

        private DateTime _updatedDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the UpdatedDate property.
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

        private string _updatedBy;

        /// <summary>
        /// Sets and gets the UpdatedBy property.
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
