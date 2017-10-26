using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.WPF.SQLite.Model
{
    public class SubCategory : ObservableObject
    {
        private int _Id;
        /// <summary>
        /// Sets and gets the Id property.
        /// </summary>
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

        private int _categoryId;

        /// <summary>
        /// Sets and gets the CategoryId property.
        /// </summary>
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                _categoryId = value;
            }
        }

        private string _subCategoryDescription = "";

        /// <summary>
        /// Sets and gets the SubCategoryDescription property.
        /// </summary>
        public string SubCategoryDescription
        {
            get
            {
                return _subCategoryDescription;
            }
            set
            {
                _subCategoryDescription = value;
            }
        }

        private string _subCategoryNotesHtml = "";

        /// <summary>
        /// Sets and gets the SubCategoryNotesHtml property.
        /// </summary>
        public string SubCategoryNotesHtml
        {
            get
            {
                return _subCategoryNotesHtml;
            }
            set
            {
                _subCategoryNotesHtml = value;
            }
        }

        private DateTime? _createdDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the CreatedDate property.
        /// </summary>
        public DateTime? CreatedDate
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
                _createdBy =  value;
            }
        }


        private DateTime? _updatedDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the UpdatedDate property.
        /// </summary>
        public DateTime? UpdatedDate
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
