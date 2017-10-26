using System;
using System.Collections.Generic;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Quiz.HelperClasses;
using GalaSoft.MvvmLight;
using DG.WPF.SQLite.Model;
using System.Linq;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    ///     This class contains properties that a View can data bind to.
    ///     <para>
    ///         See http://www.galasoft.ch/mvvm
    ///     </para>
    /// </summary>
    public class SubCategoryViewModel : ViewModelBase
    {
        private readonly QuizSessionDataService _quizSessionDataService = new QuizSessionDataService();

        /// <summary>
        ///     The <see cref="IsDirty" /> property's name.
        /// </summary>
        private const string IsDirtyPropertyName = "IsDirty";

        /// <summary>
        ///     The <see cref="SqLiteDatabasePath" /> property's name.
        /// </summary>
        private const string SqLiteDatabasePathPropertyName = "SqliteDatabasePath";

        /// <summary>
        ///     The <see cref="Id" /> property's name.
        /// </summary>
        private const string IdPropertyName = "Id";

        /// <summary>
        ///     The <see cref="CategoryId" /> property's name.
        /// </summary>
        private const string CategoryIdPropertyName = "CategoryId";

        /// <summary>
        ///     The <see cref="SubCategoryDescription" /> property's name.
        /// </summary>
        private const string SubCategoryDescriptionPropertyName = "SubCategoryDescription";

        /// <summary>
        ///     The <see cref="SubCategoryNotesHtml" /> property's name.
        /// </summary>
        private const string SubCategoryNotesHtmlPropertyName = "SubCategoryNotesHtml";

        /// <summary>
        ///     The <see cref="CreatedDate" /> property's name.
        /// </summary>
        private const string CreatedDatePropertyName = "CreatedDate";

        /// <summary>
        ///     The <see cref="CreatedBy" /> property's name.
        /// </summary>
        private const string CreatedByPropertyName = "CreatedBy";

        private static readonly string SubCategoryPreHtml = new ViewModelLocator().ParameterViewModel.PreHtml;
        private static readonly string SubCategoryPostHtml = new ViewModelLocator().ParameterViewModel.PostHtml;
        
        private int _id;
        private string _createdBy = "";
        private DateTime? _createdDate = DateTime.Now;

        private bool _isDirty;


        private string _sqLiteDatabasePath = "";
        private string _subCategoryDescription = "";
        private int _categoryId;
        private string _subCategoryNotesHtml = "";

        public SubCategoryViewModel()
        {
            SqLiteDatabasePath = new ViewModelLocator().ConfigureQuizViewModel.SqLiteDatabasePath;
            
        }

        /// <summary>
        ///     Gets the IsDirty property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public bool IsDirty
        {
            get { return _isDirty; }

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
        ///     Sets and gets the SqLiteDatabasePath property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        private string SqLiteDatabasePath
        {
            get { return _sqLiteDatabasePath; }
            set
            {
                Set(SqLiteDatabasePathPropertyName, ref _sqLiteDatabasePath, value);
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets and gets the SubCategoryNotesFullHtml property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SubCategoryNotesFullHtml
        {
            get
            {
                return
                    SubCategoryPreHtml +
                    SubCategoryNotesHtml +
                    SubCategoryPostHtml;
            }
        }

        /// <summary>
        ///     Sets and gets the TotalQuestions property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int TotalQuestions
        {
            get
            {
                var subCategoryIds = new List<int> {Id};

                var questionDataService = new QuestionDataService();
                var count = questionDataService.GetQuestionsBySubCategoryIds(SqLiteDatabasePath, subCategoryIds).Count;

                return count;
            }
        }

        /// <summary>
        ///     Sets and gets the Id property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int Id
        {
            get { return _id; }
            set { Set(IdPropertyName, ref _id, value); }
        }

        /// <summary>
        ///     Sets and gets the CategoryId property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public int CategoryId
        {
            get { return _categoryId; }
            set
            {
                Set(CategoryIdPropertyName, ref _categoryId, value);
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets and gets the SubCategoryDescription property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SubCategoryDescription
        {
            get { return _subCategoryDescription; }
            set
            {
                Set(SubCategoryDescriptionPropertyName, ref _subCategoryDescription, value);
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets and gets the SubCategoryNotesHtml property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string SubCategoryNotesHtml
        {
            get { return _subCategoryNotesHtml; }
            set
            {
                Set(SubCategoryNotesHtmlPropertyName, ref _subCategoryNotesHtml, value);
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets and gets the CreatedDate property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public DateTime? CreatedDate
        {
            get { return _createdDate; }
            set
            {
                Set(CreatedDatePropertyName, ref _createdDate, value);
                IsDirty = true;
            }
        }

        /// <summary>
        ///     Sets and gets the CreatedBy property.
        ///     Changes to that property's value raise the PropertyChanged event.
        /// </summary>
        public string CreatedBy
        {
            get { return _createdBy; }
            set
            {
                Set(CreatedByPropertyName, ref _createdBy, value);
                IsDirty = true;
            }
        }

        /// <summary>
        /// Sets and gets the QuizSessions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<QuizSession> QuizSessions
        {
            get
            {
                return _quizSessionDataService.GetQuizSessions(SqLiteDatabasePath)
                    .Where(qs => qs.SubCategoryId == Id).ToList();
            }
        }

        /// <summary>
        /// Sets and gets the QuickFireQuizSessions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<QuizSession> QuickFireQuizSessions
        {
            get
            {
                if (QuizSessions != null)
                {
                    return QuizSessions
                        .Where(qs => qs.QuizTypeId == 1 && qs.QuizEndDateTime != null).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the RevisionQuizSessions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<QuizSession> RevisionQuizSessions
        {
            get
            {
                if (QuizSessions != null)
                {
                    return QuizSessions
                        .Where(qs => qs.QuizTypeId == 2 && qs.QuizEndDateTime != null).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSession property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public QuizSession LastQuickFireQuizSession
        {
            get
            {
                if (QuizSessions != null)
                {
                    return QuizSessions
                        .Where(qs => qs.QuizTypeId == 1 && qs.QuizEndDateTime != null)
                        .OrderBy(qs => qs.QuizEndDateTime).LastOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSessionPercentage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastQuickFireQuizSessionPercentage
        {
            get
            {
                if (LastQuickFireQuizSession != null)
                {
                    var p = Convert.ToDecimal(LastQuickFireQuizSession.CorrectQuestions) / Convert.ToDecimal(LastQuickFireQuizSession.TotalQuestions);
                    return (p * 100).ToString("0.##") + "%";
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSessionScoreMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastQuickFireQuizSessionScoreMessage
        {
            get
            {
                if (LastQuickFireQuizSession != null && LastQuickFireQuizSessionPercentage != null)
                {
                    return LastQuickFireQuizSession.CorrectQuestions +
                           " out of " +
                           LastQuickFireQuizSession.TotalQuestions +
                           " - " +
                           LastQuickFireQuizSessionPercentage;
                }
                else
                {
                    return "n/a";
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSessionCompletedMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastQuickFireQuizSessionCompletedMessage
        {
            get
            {
                if (LastQuickFireQuizSession != null && LastQuickFireQuizSession.QuizEndDateTime != null)
                {
                    return LastQuickFireQuizSession.QuizEndDateTime.Value.ToString("dd/MM/yy HH:mm:ss") +
                        " (" + LastQuickFireQuizSessionAge + " days ago)";
                }
                else
                {
                    return "n/a";
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSessionAge property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int? LastQuickFireQuizSessionAge
        {
            get
            {
                if (LastQuickFireQuizSession != null)
                {
                    var tsLastCompleted = DateTime.Now - Convert.ToDateTime(LastQuickFireQuizSession.QuizEndDateTime);

                    return tsLastCompleted.Days;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastQuickFireQuizSessionTimeMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastQuickFireQuizSessionTimeMessage
        {
            get
            {
                if (LastQuickFireQuizSession != null)
                {
                    var time = Convert.ToDateTime(LastQuickFireQuizSession.QuizEndDateTime) - Convert.ToDateTime(LastQuickFireQuizSession.QuizStartDateTime);
                    return time.ToShortForm();
                }
                else
                {
                    return "n/a";
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSession property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public QuizSession LastRevisionQuizSession
        {
            get
            {
                if (QuizSessions != null)
                {
                    return QuizSessions
                        .Where(qs => qs.QuizTypeId == 2 && qs.QuizEndDateTime != null)
                        .OrderBy(qs => qs.QuizEndDateTime).LastOrDefault();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSessionPercentage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastRevisionQuizSessionPercentage
        {
            get
            {
                if (LastRevisionQuizSession != null)
                {
                    var p = Convert.ToDecimal(LastRevisionQuizSession.CorrectQuestions) / Convert.ToDecimal(LastRevisionQuizSession.TotalQuestions);
                    return (p * 100).ToString("0.##") + "%";
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSessionScoreMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastRevisionQuizSessionScoreMessage
        {
            get
            {
                if (LastRevisionQuizSession != null && LastRevisionQuizSessionPercentage != null)
                {
                    return LastRevisionQuizSession.CorrectQuestions +
                           " out of " +
                           LastRevisionQuizSession.TotalQuestions +
                           " - " +
                           LastRevisionQuizSessionPercentage;
                }
                else
                {
                    return "n/a";
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSessionCompletedMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastRevisionQuizSessionCompletedMessage
        {
            get
            {
                if (LastRevisionQuizSession != null && LastRevisionQuizSession.QuizEndDateTime != null)
                {
                    return LastRevisionQuizSession.QuizEndDateTime.Value.ToString("dd/MM/yy HH:mm:ss") +
                        " (" + LastRevisionQuizSessionAge + " days ago)";

                }
                else
                {
                    return "n/a";
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSessionAge property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int? LastRevisionQuizSessionAge
        {
            get
            {
                if (LastRevisionQuizSession != null)
                {
                    var tsLastCompleted = DateTime.Now - Convert.ToDateTime(LastRevisionQuizSession.QuizEndDateTime);

                    return tsLastCompleted.Days;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Sets and gets the LastRevisionQuizSessionTimeMessage property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string LastRevisionQuizSessionTimeMessage
        {
            get
            {
                if (LastRevisionQuizSession != null)
                {
                    var time = Convert.ToDateTime(LastRevisionQuizSession.QuizEndDateTime) - Convert.ToDateTime(LastRevisionQuizSession.QuizStartDateTime);
                    return time.ToShortForm();
                }
                else
                {
                    return "n/a";
                }
            }
        }
    }
}