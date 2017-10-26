using DG.WPF.SQLite.Model;
using System.Collections.Generic;
using System.Linq;
using DG.WPF.SQLite.Utilities;
using SQLite;

namespace DG.WPF.SQLite.DataService
{
    public class CategoryDataService
    {
        private static CategoryDataService _current = null;
        public static CategoryDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new CategoryDataService();

                return _current;
            }
        }

        public List<Category> GetCategories(string dbPath)
        {
            var categories = new List<Category>();

            using (var db = new SQLiteConnection(dbPath))
            {
                categories = db.Query<Category>("SELECT * FROM Category");
            }

            return categories;
        }

        public Category GetCategoryById(string dbPath, int Id)
        {
            var category = new Category();

            using (var db = new SQLiteConnection(dbPath))
            {
                category = db.Query<Category>
                    ("SELECT * FROM Category WHERE Id = " + Id).FirstOrDefault();
            }

            return category;
        }

        public int AddCategory(string dbPath, Category category)
        {
            var key = 0;

            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new Category()
                    {
                        CategoryDescription = category.CategoryDescription,
                        CreatedDate = category.CreatedDate,
                        CreatedBy = category.CreatedBy,
                        UpdatedDate = category.UpdatedDate,
                        UpdatedBy = category.UpdatedBy,
                        CategoryNotesHtml = category.CategoryNotesHtml.SQLiteStringFormat(),
                        AdditionalInfoUri = category.AdditionalInfoUri,
                        AdditionalInfoFtpAddress = category.AdditionalInfoFtpAddress
                    });

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            
            }

            return key;
        }

        public void DeleteCategory(string dbPath, int Id)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<Category>
                    ("DELETE FROM Category WHERE Id = " + Id);
            }

        }

        public void SaveCategories(string dbPath, List<Category> categories)
        {
            foreach(var category in categories)
            {
                using (var db = new SQLiteConnection(dbPath))
                {
                    db.Query<Category>
                        ("UPDATE Category SET " +
                         "CategoryDescription = '" + category.CategoryDescription.SQLiteStringFormat()  + "', " +
                         "CreatedDate = '" + category.CreatedDate  + "', " +
                         "CreatedBy = '" + category.CreatedBy + "', " +
                         "UpdatedDate = '" + category.UpdatedDate + "', " +
                         "UpdatedBy = '" + category.UpdatedBy + "', " +
                         "CategoryNotesHtml = '" + category.CategoryNotesHtml.SQLiteStringFormat() + "', " +
                         "AdditionalInfoUri = '" + category.AdditionalInfoUri + "', " +
                         "AdditionalInfoFtpAddress = '" + category.AdditionalInfoFtpAddress + "' WHERE Id = " + category.Id);
                }
            }
        }
    }    
}
