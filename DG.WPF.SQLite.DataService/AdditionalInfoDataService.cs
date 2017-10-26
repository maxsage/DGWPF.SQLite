using DG.WPF.SQLite.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DG.WPF.SQLite.Utilities;
using SQLite;

namespace DG.WPF.SQLite.DataService
{
    public class AdditionalInfoDataService
    {
        private static AdditionalInfoDataService _current = null;
        public static AdditionalInfoDataService Current
        {
            get
            {
                if (_current == null)
                    _current = new AdditionalInfoDataService();

                return _current;
            }
        }

        public List<AdditionalInfo> GetAdditionalInfosBySubCategoryId(string dbPath, int subCategoryId)
        {
            var additionalInfos = new List<AdditionalInfo>();

            using (var db = new SQLiteConnection(dbPath))
            {
                additionalInfos = db.Query<AdditionalInfo>("SELECT * FROM AdditionalInfo WHERE SubCategoryId = " +
                    subCategoryId.ToString());
            }

            return additionalInfos;
        }

        public AdditionalInfo GetAdditionalInfoById(string dbPath, int Id)
        {
            var additionalInfo = new AdditionalInfo();

            using (var db = new SQLiteConnection(dbPath))
            {
                additionalInfo = db.Query<AdditionalInfo>("SELECT * FROM AdditionalInfo WHERE Id = " +
                    Id.ToString()).FirstOrDefault();
            }

            return additionalInfo;
        }

        public int AddAdditionalInfo(string dbPath, AdditionalInfo additionalInfo)
        {
            var key = 0;
            
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Insert(new AdditionalInfo()
                {
                    SubCategoryId = additionalInfo.SubCategoryId,
                    SequenceNumber = additionalInfo.SequenceNumber,
                    SectionDescription = additionalInfo.SectionDescription.SQLiteStringFormat(),
                    AdditionalInfoHtml = additionalInfo.AdditionalInfoHtml.SQLiteStringFormat(),
                    AdditionalInfoUri = additionalInfo.AdditionalInfoUri.SQLiteStringFormat(),
                    DisplayInBrowser = additionalInfo.DisplayInBrowser,
                    CreatedDate = additionalInfo.CreatedDate,
                    CreatedBy = additionalInfo.CreatedBy,
                    UpdatedDate = additionalInfo.UpdatedDate,
                    UpdatedBy = additionalInfo.UpdatedBy
                });

                key = db.ExecuteScalar<int>("SELECT last_insert_rowid()");
            }

            return key;
        }

        public void DeleteAdditionalInfo(string dbPath, int Id)
        {
            using (var db = new SQLiteConnection(dbPath))
            {
                db.Query<AdditionalInfo>
                    ("DELETE FROM AdditionalInfo WHERE Id = " + Id);
            }
        }

        public List<Question> GetQuestions(string dbPath)
        {
            var questions = new List<Question>();

            using (var db = new SQLiteConnection(dbPath))
            {
                questions = db.Query<Question>("SELECT * FROM Question");
            }

           return questions;
        }

        public void SaveAdditionalInfos(string dbPath, List<AdditionalInfo> additionalInfos)
        {
            {
                foreach (AdditionalInfo additionalInfo in additionalInfos)
                {
                    using (var db = new SQLiteConnection(dbPath))
                    {
                        db.Query<AdditionalInfo>
                            ("UPDATE AdditionalInfo SET " +
                             "SubCategoryId = " + additionalInfo.SubCategoryId + ", " +
                             "SequenceNumber = " + additionalInfo.SequenceNumber + ", " +
                             "SectionDescription = '" + additionalInfo.SectionDescription.SQLiteStringFormat() + "', " +
                             "AdditionalInfoHtml = '" + additionalInfo.AdditionalInfoHtml.SQLiteStringFormat() + "', " +
                             "AdditionalInfoUri = '" + additionalInfo.AdditionalInfoUri + "', " +
                             "DisplayInBrowser = " + (additionalInfo.DisplayInBrowser == true ? 1 : 0) + ", " +
                             "CreatedDate = '" + additionalInfo.CreatedDate + "', " +
                             "CreatedBy = '" + additionalInfo.CreatedBy + "', " +
                             "UpdatedDate = '" + additionalInfo.UpdatedDate + "', " +
                             "UpdatedBy = '" + additionalInfo.UpdatedBy  + "' WHERE Id = " + 
                                additionalInfo.Id);
                    }
                }
            }
        }
    }
}
