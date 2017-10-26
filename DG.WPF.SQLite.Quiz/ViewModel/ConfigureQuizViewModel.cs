using System.Linq;
using System.Windows;
using System.Windows.Data;
using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using DG.WPF.SQLite.Quiz.Views;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    /// Interaction logic for ConfigureQuiz.xaml
    /// </summary>
    public class ConfigureQuizViewModel : ViewModelBase
    {
        private readonly SubCategoryDataService _subCategoryDataService = new SubCategoryDataService();
        private readonly CategoryDataService _categoryDataService = new CategoryDataService();
        private readonly QuestionDataService _questionDataService = new QuestionDataService();
        private readonly QuizSessionDataService _quizSessionDataService = new QuizSessionDataService();
        private readonly QuizSessionQuestionDataService _quizSessionQuestionDataService = new QuizSessionQuestionDataService();

        #region Constructors
        public ConfigureQuizViewModel()
        {
            OpenQuizPackCommand = new RelayCommand(OpenQuizPack);
            StartQuizCommand = new RelayCommand(StartQuiz);
            ShowAboutWindowCommand = new RelayCommand(ShowAboutWindow);
            ShowOptionsWindowCommand = new RelayCommand(ShowOptionsWindow);
            CloseDatabaseCommand = new RelayCommand(CloseDatabase);
            ExitCommand = new RelayCommand<Window>(Exit);

            QuickFireMode = true;
            RevisionMode = false;
            OrderQuestions = true;

            // TODO - provide login functionality
            PlayerId = "MrNew";

            var quizSessionViewModel = new QuizSessionViewModel();
            
        }
        #endregion Constructors

        #region RelayCommandProperties
        public RelayCommand OpenQuizPackCommand
        {
            get;
            private set;
        }

        public RelayCommand StartQuizCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowAboutWindowCommand
        {
            get;
            private set; 
        }

        public RelayCommand ShowOptionsWindowCommand
        {
            get;
            private set;
        }

        public RelayCommand CloseDatabaseCommand
        {
            get;
            private set;
        }
        public RelayCommand<Window> ExitCommand
        {
            get;
            private set;
        }
        #endregion RelayCommand Properties

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        private const string CategoriesPropertyName = "Categories";

        private ObservableCollection<CategoryViewModel> _categories;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                if (_categories == value)
                {
                    return;
                }

                _categories = value;
                RaisePropertyChanged(CategoriesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentQuestions" /> property's name.
        /// </summary>
        private const string CurrentQuestionsPropertyName = "CurrentQuestions";

        private ObservableCollection<QuizSessionQuestionViewModel> _CurrentQuestions;

        /// <summary>
        /// Gets the CurrentQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<QuizSessionQuestionViewModel> CurrentQuestions
        {
            get
            {
                return _CurrentQuestions;
            }

            set
            {
                if (_CurrentQuestions == value)
                {
                    return;
                }

                _CurrentQuestions = value;
                RaisePropertyChanged(CategoriesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        private const string SelectedCategoryPropertyName = "SelectedCategory";

        private CategoryViewModel _selectedCategory;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (_selectedCategory == value)
                {
                    return;
                }

                Set(SelectedCategoryPropertyName, ref _selectedCategory, value);
            }
        }

        /// <summary>
        /// The <see cref="SubCategories" /> property's name.
        /// </summary>
        private const string SubCategoriesPropertyName = "SubCategories";

        private ObservableCollection<SubCategoryViewModel> _subCategories;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SubCategoryViewModel> SubCategories
        {
            get
            {
                return _subCategories;
            }

            set
            {
                if (_subCategories == value)
                {
                    return;
                }

                _subCategories = value;
                RaisePropertyChanged(SubCategoriesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentSubCategory" /> property's name.
        /// </summary>
        private const string SelectedSubCategoryPropertyName = "CurrentSubCategory";

        private SubCategoryViewModel _currentSubCategory;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SubCategoryViewModel CurrentSubCategory
        {
            get
            {
                return _currentSubCategory;
            }

            set
            {
                if (_currentSubCategory == value)
                {
                    return;
                }

                Set(SelectedSubCategoryPropertyName, ref _currentSubCategory, value);
            }
        }

        /// <summary>
        /// The <see cref="PlayerId" /> property's name.
        /// </summary>
        private const string PlayerIdPropertyName = "PlayerId";

        private string _playerId;

        /// <summary>
        /// Gets the PlayerId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PlayerId
        {
            get
            {
                return _playerId;
            }

            set
            {
                if (_playerId == value)
                {
                    return;
                }

                _playerId = value;
                RaisePropertyChanged(PlayerIdPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SqLiteDatabasePath" /> property's name.
        /// </summary>
        private const string SqLiteDatabasePathPropertyName = "SqLiteDatabasePath";

        private string _sqLiteDatabasePath = "";

        /// <summary>
        /// Sets and gets the SqLiteDatabasePath property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SqLiteDatabasePath
        {
            get
            {
                return _sqLiteDatabasePath;
            }
            private set
            {
                Set(SqLiteDatabasePathPropertyName, ref _sqLiteDatabasePath, value);
            }
        }


        /// <summary>
        /// The <see cref="RevisionMode" /> property's name.
        /// </summary>
        private const string RevisionModePropertyName = "RevisionMode";

        private bool _revisionMode;

        /// <summary>
        /// Sets and gets the RevisionMode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool RevisionMode
        {
            get
            {
                return _revisionMode;
            }
            set
            {
                Set(RevisionModePropertyName, ref _revisionMode , value);
            }
        }

        /// <summary>
        /// The <see cref="QuickFireMode" /> property's name.
        /// </summary>
        private const string QuickFireModePropertyName = "QuickFireMode";

        private bool _quickFireMode;

        /// <summary>
        /// Sets and gets the QuickFireMode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool QuickFireMode
        {
            get
            {
                return _quickFireMode;
            }
            set
            {
                Set(QuickFireModePropertyName, ref _quickFireMode, value);
            }
        }

        /// <summary>
        /// Gets the QuizTypeId property.
        /// </summary>
        public int QuizTypeId
        {
            get { return QuickFireMode ? 1 : 2; }
        }

        /// <summary>
        /// The <see cref="OrderQuestions" /> property's name.
        /// </summary>
        private const string OrderQuestionsPropertyName = "OrderQuestions";

        private bool _orderQuestions;

        /// <summary>
        /// Sets and gets the OrderQuestionsMode property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool OrderQuestions
        {
            get
            {
                return _orderQuestions;
            }
            set
            {
                Set(OrderQuestionsPropertyName, ref _orderQuestions, value);
            }
        }

        #endregion ViewModel Properties

        #region Event Methods
        private void StartQuiz()
        {
            // Locate the existing quizSessionViewModel
            var quizSessionViewModel = new ViewModelLocator().QuizSessionViewModel;

            // Check the database for existing QuizSessions that have not been completed.
            var existingQuizSession =
            _quizSessionDataService.GetQuizSessionsByPlayerSubCategoryAndQuizType(
                SqLiteDatabasePath, PlayerId, CurrentSubCategory.Id, QuickFireMode ? 1 : 2).FirstOrDefault(qs => qs.QuizEndDateTime == null);

            // Create a new QuizSession or load an existing one if available 
            if (existingQuizSession == null)
            {
                // Create a new QuizSession 
                var newQuizSession = new QuizSession()
                {
                    QuizStartDateTime = DateTime.Now,
                    QuizTypeId = QuizTypeId,
                    OrderQuestions = OrderQuestions,
                    CorrectQuestions = 0,
                    PlayerId = PlayerId,
                    SubCategoryId = CurrentSubCategory.Id,
                    TotalQuestions =
                        _questionDataService.GetQuestionsBySubCategoryId(SqLiteDatabasePath, CurrentSubCategory.Id)
                            .Count
                };

                // Add the new QuizSession to the database
                var quizSessionId = _quizSessionDataService.AddQuizSession(SqLiteDatabasePath, newQuizSession);

                // Map the new QuizSession to a QuizSessionViewModel
                Mapper.Map(newQuizSession, quizSessionViewModel);
            
                // Get the Questions for this SubCategory from the Database
                // And then Map them to QuizSessionQuestionViewModel objects
                var quizSessionQuestions =
                Mapper.Map<List<Question>, List<QuizSessionQuestionViewModel>>(
                    _questionDataService.GetQuestionsBySubCategoryId(SqLiteDatabasePath, CurrentSubCategory.Id));

                // Order the QuizSessionQuestions by SequenceNumber or randomly
                var orderedQuizSessionQuestions = OrderQuestions
                    ? quizSessionQuestions.OrderBy(q => q.SequenceNumber)
                    : quizSessionQuestions.OrderBy(x => Guid.NewGuid());
                
                // Allocate a DisplayNumber to remember the ordering (This is important even
                // if the ordering is random)
                var i = 0;
                foreach (var quizSessionQuestionViewModel in orderedQuizSessionQuestions)
                {
                    quizSessionQuestionViewModel.DisplayNumber = ++i;
                    quizSessionQuestionViewModel.QuizSessionId = quizSessionId;
                    quizSessionQuestionViewModel.CorrectlyAnswered = null;

                    var quizSessionQuestion = new QuizSessionQuestion();
                    Mapper.Map(quizSessionQuestionViewModel, quizSessionQuestion);

                    // Write the QuizSessionQuestionViewModel to the database using
                    // a Map from QuizSessionQuestionViewModel to QuizSessionQuestion
                    _quizSessionQuestionDataService.AddQuizSessionQuestion(SqLiteDatabasePath, 
                        quizSessionQuestion);                                
                }

                quizSessionViewModel.Id = quizSessionId;
                quizSessionViewModel.QuestionsInternal = 
                    new ObservableCollection<QuizSessionQuestionViewModel>(orderedQuizSessionQuestions.OrderBy(q => q.DisplayNumber));
                quizSessionViewModel.Questions = new CollectionViewSource();
                quizSessionViewModel.CurrentQuizSessionQuestion = quizSessionViewModel.QuestionsInternal.FirstOrDefault();
            }
            else
            {
                // Tell the user we are loading an existing QuizSession
                MessageBox.Show(PlayerId + " started a quiz: " + CurrentSubCategory.SubCategoryDescription + " on " +
                                existingQuizSession.QuizStartDateTime.ToString("d") + ". I will load that quiz");

                // Map the existing QuizSession to a QuizSessionViewModel
                Mapper.Map(existingQuizSession, quizSessionViewModel);
                
                // Get the existing QuizSessionQuestions
                var existingQuizSessionQuestions =
                    _quizSessionQuestionDataService.GetQuizSessionQuestionsByQuizSessionId(SqLiteDatabasePath, existingQuizSession.Id);

                // Get the Questions for this SubCategory from the Database
                var questions =
                    _questionDataService.GetQuestionsBySubCategoryId(SqLiteDatabasePath, CurrentSubCategory.Id);

                // Map the QuizSessionQuestion objects to QuizSessionQuestionViewModel objects
                var quizSessionQuestions =
                        Mapper.Map<List<QuizSessionQuestion>, List<QuizSessionQuestionViewModel>>(
                        existingQuizSessionQuestions);

                // Iterate through the quizSessionQuestions assigning the 
                // additional Question attributes
                //TODO Should you be using Automapper here
                foreach (var quizSessionQuestionViewModel in quizSessionQuestions)
                {
                    var model = quizSessionQuestionViewModel;
                    var question = questions.First(q => q.Id == model.Id);
                    quizSessionQuestionViewModel.AdditionalInfoId = question.AdditionalInfoId;
                    quizSessionQuestionViewModel.Description = question.Description;
                    quizSessionQuestionViewModel.DescriptionHtml = question.DescriptionHtml;
                    quizSessionQuestionViewModel.Difficulty = question.Difficulty;
                    quizSessionQuestionViewModel.QuestionTypeId = question.QuestionTypeId;
                    quizSessionQuestionViewModel.Score = question.Score;
                    quizSessionQuestionViewModel.SequenceNumber = question.SequenceNumber;
                    quizSessionQuestionViewModel.SubCategoryId = question.SubCategoryId;
                }

                quizSessionViewModel.QuestionsInternal = new ObservableCollection<QuizSessionQuestionViewModel>(quizSessionQuestions.OrderBy(o => o.DisplayNumber)); 

                quizSessionViewModel.Id = existingQuizSession.Id;
                quizSessionViewModel.Questions = new CollectionViewSource();
                quizSessionViewModel.CorrectQuestions = existingQuizSessionQuestions.Count(qsq => qsq.CorrectlyAnswered == true);
                quizSessionViewModel.CurrentQuizSessionQuestion = quizSessionViewModel.QuestionsInternal
                    .First(q => q.CorrectlyAnswered == null);

            }

            quizSessionViewModel.StartQuiz();

            var doQuizWindow = new DoQuizWindow();
            doQuizWindow.Show();
        }

        private void OpenQuizPack()
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".db",
                Filter = "DB Files (*.db)|*.db",
                InitialDirectory = Properties.Settings.Default.DefaultQuizPackDirectory
            };

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                SqLiteDatabasePath = dlg.FileName;

                var categories =
                    Mapper.Map<List<Category>, List<CategoryViewModel>>(
                    _categoryDataService.GetCategories(SqLiteDatabasePath));

                var subCategories =
                    Mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(
                    _subCategoryDataService.GetSubCategories(SqLiteDatabasePath));

                Categories = new ObservableCollection<CategoryViewModel>(categories);
                SelectedCategory = Categories.FirstOrDefault();
                SubCategories = new ObservableCollection<SubCategoryViewModel>(subCategories);
            }
        }

        private void ShowAboutWindow()
        {
            var aboutWindow = new AboutWindow();
            aboutWindow.ShowDialog();
        }

        private void ShowOptionsWindow()
        {
            var optionsWindow = new OptionsWindow();
            optionsWindow.ShowDialog();
        }

        private void CloseDatabase()
        {
            SqLiteDatabasePath = "";

            Categories = null;
            SubCategories = null;

        }

        private void Exit(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        #endregion Event Methods 

    }
    
}
