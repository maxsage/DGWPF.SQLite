using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using SQLite;
using DG.WPF.SQLite.Model;
using DG.WPF.SQLite.Utilities;

namespace DG.WPF.SQLite.DataService
{
    public class SubCategoryDataService
    {
        private static SubCategoryDataService _current = null;
        public static SubCategoryDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new SubCategoryDataService();

                return _current;
            }
        }

        public List<SubCategory> GetSubCategoriesByCategory(string dbPath, int categoryId)
        {
            var subCategories = new List<SubCategory>();

            using (var db = new SQLiteConnection(dbPath))
            {
                subCategories = db.Query<SubCategory>("SELECT * FROM SubCategory WHERE Id = " + categoryId);
            }

            return subCategories;
        }
        
        public List<SubCategory> GetSubCategories(string dbPath)
        {
            List<SubCategory> subCategories;

            using (var db = new SQLiteConnection(dbPath))
            {
                subCategories = db.Query<SubCategory>("SELECT * FROM SubCategory");
            }

            return subCategories;
        }

        public SubCategory GetSubCategoryById(string dbPath, int subCategoryId)
        {
            SubCategory subCategory;

            using (var db = new SQLiteConnection(dbPath))
            {
                subCategory = db.Query<SubCategory>
                    ("SELECT * FROM SubCategory WHERE Id = " + subCategoryId).FirstOrDefault();
            }

            return subCategory;
        }

        public int AddSubCategory(string dbPath, SubCategory subCategory)
        {
            var key = 0;

            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<SubCategory>
                    ("INSERT INTO SubCategory (CategoryId, SubCategoryDescription, SubCategoryNotesHtml, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy) " +
                    "VALUES('" +
                        subCategory.CategoryId + "', '" +
                        subCategory.SubCategoryDescription.SQLiteStringFormat() + "', '" +
                        subCategory.SubCategoryNotesHtml.SQLiteStringFormat() + "', '" +
                        subCategory.CreatedDate + "', '" +
                        subCategory.CreatedBy + "', '" +
                        subCategory.UpdatedDate + "', '" +
                        subCategory.UpdatedBy + "');");

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return key;
        }

        public void DeleteSubCategory(string dbPath, int subCategoryId)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<SubCategory>
                    ("DELETE FROM SubCategory WHERE Id = " + subCategoryId);
            }
        }

        public void SaveSubCategories(string dbPath, List<SubCategory> subCategories)
        {
            foreach (SubCategory subCategory in subCategories)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Query<SubCategory>
                        ("UPDATE SubCategory SET " +
                         "CategoryId = '" + subCategory.CategoryId + "', " +
                         "SubCategoryDescription = '" + subCategory.SubCategoryDescription.SQLiteStringFormat() + "', " +
                         "SubCategoryNotesHtml = '" + subCategory.SubCategoryNotesHtml.SQLiteStringFormat() + "', " +
                         "CreatedDate = '" + subCategory.CreatedDate + "', " +
                         "CreatedBy = '" + subCategory.CreatedBy + "', " +
                         "UpdatedDate = '" + subCategory.UpdatedDate + "', " +
                         "UpdatedBy = '" + subCategory.UpdatedBy + "' WHERE Id = " + subCategory.Id);
                }
            }
        }
    }
}
