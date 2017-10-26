using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using SQLite;

namespace DG.WPF.SQLite.Model
{
    public class Answer : ObservableObject
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

        private int _questionId = 0;

        /// <summary>
        /// Sets and gets the QuestionId property.
        /// </summary>
        public int QuestionId
        {
            get
            {
                return _questionId;
            }
            set
            {
                _questionId = value;
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

        private bool _correctAnswer;

        /// <summary>
        /// Sets and gets the CorrectAnswer property.
        /// </summary>
        public bool CorrectAnswer
        {
            get
            {
                return _correctAnswer;
            }
            set
            {
                _correctAnswer = value;
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
