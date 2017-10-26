using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Model
{
    public class QuizQuestion : ObservableObject 
    {
        private int _id;

        /// <summary>
        /// Sets and gets the Id property.
        /// </summary>
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
    }
}
