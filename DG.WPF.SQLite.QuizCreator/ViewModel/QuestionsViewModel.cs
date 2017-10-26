using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Data;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    public class QuestionsViewModel : ViewModelBase
    {
        private readonly QuestionDataService _questionDataService = new QuestionDataService();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the QuestionViewModel class.
        /// </summary>
        public QuestionsViewModel(SubCategoryViewModel selectedSubCategory)
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath;

            SelectedSubCategory = selectedSubCategory;
            
            //TODO is there a more efficient way of doing this?
            SelectedCategory = new ViewModelLocator().CategoriesViewModel.Categories.FirstOrDefault(c => c.Id == selectedSubCategory.CategoryId);

            SaveQuestionsCommand = new RelayCommand(this.SaveQuestions);
            NewQuestionCommand = new RelayCommand(this.NewQuestion);
            DeleteQuestionCommand = new RelayCommand(this.DeleteQuestion);
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.CheckQuestions);

            NextQuestionCommand = new RelayCommand(this.NavigateToNextQuestion);
            PreviousQuestionCommand = new RelayCommand(this.NavigateToPreviousQuestion);

            ShowAnswersCommand = new RelayCommand(this.ShowAnswersWindow);

            PreviewHtmlCommand = new RelayCommand(this.PreviewHtml);
            EditHtmlCommand = new RelayCommand(this.EditHtml);
            UpdateHtmlCommand = new RelayCommand(this.UpdateHtml);

            List<QuestionViewModel> questions =
                Mapper.Map<List<Question>, List<QuestionViewModel>>(
                    _questionDataService.GetQuestions(SqLiteDatabasePath).OrderBy(q => q.SequenceNumber).ToList());
            
            foreach(QuestionViewModel question in questions)
            {
                question.IsDirty = false;
            }
            
            QuestionsInternal = new ObservableCollection<QuestionViewModel>(questions);
            Questions = new CollectionViewSource();
        }
        #endregion Constructors

        #region Filtering Logic
        void Questions_Filter(object sender, FilterEventArgs e)
        {
            QuestionViewModel question = e.Item as QuestionViewModel;
            if (question != null)
            {
                if (question.SubCategoryId == SelectedSubCategory.Id)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }

            }

        }
        #endregion Filtering Logic

        #region RelayCommand Properties
        public RelayCommand ShowAnswersCommand
        {
            get;
            private set;
        }


        public RelayCommand PreviewHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand UpdateHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand EditHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveQuestionsCommand
        {
            get;
            private set;
        }

        public RelayCommand NewQuestionCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteQuestionCommand        
        {
            get;
            private set;
        }

        public RelayCommand NextQuestionCommand
        {
            get;
            private set;
        }

        public RelayCommand PreviousQuestionCommand
        {
            get;
            private set;
        }

        public RelayCommand<CancelEventArgs> OnClosingCommand
        {
            get;
            private set;
        }
        #endregion RelayCommand Properties

        #region CRUD Methods
        private void CheckQuestions(CancelEventArgs e)
        {
            if (QuestionsInternal.All(q => q.IsDirty != true)) return;
            if (System.Windows.MessageBox.Show("There are unsaved changes. Proceed?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void SaveQuestions()
        {
            var dirtyQuestions = QuestionsInternal.Where(q => q.IsDirty == true).ToList();

            var questions = Mapper.Map<List<QuestionViewModel>, List<Question>>(dirtyQuestions);
        
            _questionDataService.SaveQuestions(SqLiteDatabasePath, questions);

            // Set dirty questions as not dirty
            foreach (var q in dirtyQuestions)
            {
                q.IsDirty = false;
            }
        }

        private void NewQuestion()
        {
            // Get the last sequence number
            int lastSequenceNumber = 0;
            var additionalInfoId = 0;
            try
            {
                var lastOrDefault = QuestionsInternal.Where(ai => ai.SubCategoryId == SelectedSubCategory.Id).OrderBy(ai => ai.SequenceNumber).LastOrDefault();
                if (lastOrDefault != null)
                {
                    lastSequenceNumber = lastOrDefault.SequenceNumber;
                    // Also set the additionalInfoId so that we continue 
                    // with the AdditionalInfo used on the last question
                    additionalInfoId = lastOrDefault.AdditionalInfoId;
                }
                else
                {
                    // See if we have any AdditionalInfos 
                    try
                    {
                        var additionalInfoViewModel = AdditionalInfos.FirstOrDefault();
                        if (additionalInfoViewModel != null)
                        {
                            additionalInfoId = additionalInfoViewModel.Id;
                        }
                    }
                    catch { }
                }
            }
            catch
            { }

            var firstOrDefault = QuestionTypes.FirstOrDefault();
            if (firstOrDefault == null) return;
            var question = new Question
            {
                SequenceNumber = (lastSequenceNumber + 10),
                QuestionTypeId = firstOrDefault.Id,
                Description = "New Question",
                DescriptionHtml = "",
                SubCategoryId = SelectedSubCategory.Id,
                Difficulty = 1,
                Score = 10,
                AdditionalInfoId = additionalInfoId, 
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Environment.UserName

            };
            
                

            question.Id = _questionDataService.AddQuestion(SqLiteDatabasePath, question);
            
            var questionViewModel = new QuestionViewModel();
            questionViewModel = Mapper.Map<Question, QuestionViewModel>(question);
            questionViewModel.IsDirty = false;

            QuestionsInternal.Add(questionViewModel);
            SelectedQuestion = questionViewModel;
        }

        private void DeleteQuestion()
        {
            if (SelectedQuestion == null) return;
            if (
                MessageBox.Show("Are you sure you want to delete the question: " + SelectedQuestion.Description,
                    "Confirm", MessageBoxButton.OKCancel) != MessageBoxResult.OK) return;
            _questionDataService.DeleteQuestion(SqLiteDatabasePath, SelectedQuestion.Id);

            QuestionsInternal.Remove(SelectedQuestion);
        }

        #endregion CRUD Methods

        #region ListView Navigation Methods
        private void NavigateToNextQuestion()
        {
            Questions.View.MoveCurrentToNext();
            SelectedQuestion = (QuestionViewModel)Questions.View.CurrentItem;
        }

        private void NavigateToPreviousQuestion()
        {
            Questions.View.MoveCurrentToPrevious();
            SelectedQuestion = (QuestionViewModel)Questions.View.CurrentItem;
        }
        #endregion ListView Navigation Methods

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="QuestionsInternal" /> property's name.
        /// </summary>
        public const string QuestionsInternalPropertyName = "QuestionsInternal";
        private ObservableCollection<QuestionViewModel> _questionsInternal;
        /// <summary>
        /// Gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<QuestionViewModel> QuestionsInternal
        {
            get
            {
                //return new ObservableCollection<QuestionViewModel>(_questions.Where(q => q.Model.SubCategoryId == SelectedSubCategory.Model.SubCategoryId));
                //return _questions.Where(q => q.Model.SubCategoryId == SelectedSubCategory.Model.SubCategoryId);
                return _questionsInternal;
            }

            set
            {
                if (_questionsInternal == value)
                {
                    return;
                }
                _questionsInternal = value;
                RaisePropertyChanged(QuestionsInternalPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Questions" /> property's name.
        /// </summary>
        public const string QuestionsPropertyName = "Questions";
        private CollectionViewSource _questions;
        /// <summary>
        /// Gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CollectionViewSource Questions
        {
            get
            {
                _questions.Source = QuestionsInternal;
                _questions.Filter += Questions_Filter;
                return _questions;
            }

            set
            {
                if (_questions == value)
                {
                    return;
                }
                _questions = value;
                RaisePropertyChanged(QuestionsPropertyName);
            }
        }

        private bool Filter(object arg)
        {
            
            return true;
        }


        /// <summary>
        /// Gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<SubCategoryViewModel> SubCategories
        {
            get
            {
                //TODO: SubCategoriesInternal or just SubCategories from SubCategoriesViewModel?
                var _subCategories = new ViewModelLocator().SubCategoriesViewModel.SubCategoriesInternal.ToList();
                return _subCategories.Where(sc => sc.CategoryId == SelectedCategory.Id).ToList();
            }
        }

        /// <summary>
        /// Gets the QuestionTypes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<QuestionType> QuestionTypes
        {
            get
            {
                var _questionTypes = new ViewModelLocator().QuestionTypesViewModel.QuestionTypes.ToList();
                return _questionTypes;
            }
        }

        /// <summary>
        /// Gets the AdditionalInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<AdditionalInfoViewModel> AdditionalInfos
        {
            get
            {
                var additionalInfoDataService = new AdditionalInfoDataService();

                List<AdditionalInfoViewModel> additionalInfos = 
                    Mapper.Map<List<AdditionalInfo>, List<AdditionalInfoViewModel>>(
                        additionalInfoDataService.GetAdditionalInfosBySubCategoryId(SqLiteDatabasePath, 
                        SelectedSubCategory.Id).OrderBy(ai => ai.SequenceNumber).ToList());

                return additionalInfos;
            }
        }

        /// <summary>
        /// The <see cref="SelectedSubCategory" /> property's name.
        /// </summary>
        public const string SelectedSubCategoryPropertyName = "SelectedSubCategory";

        private SubCategoryViewModel _selectedSubCategory;
        
        /// <summary>
        /// Gets the SelectedSubCategory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SubCategoryViewModel SelectedSubCategory
        {
            get
            {
                return _selectedSubCategory;
            }
            set
            {
                _selectedSubCategory = value;
            }

        }

        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        private CategoryViewModel _selectedCategory;

        /// <summary>
        /// Gets the SelectedCategory property.
        /// </summary>
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
            }
        }

        /// <summary>
        /// The <see cref="SelectedQuestion" /> property's name.
        /// </summary>
        public const string SelectedQuestionPropertyName = "SelectedQuestion";

        private QuestionViewModel _selectedQuestion;

        /// <summary>
        /// Gets the SelectedQuestion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public QuestionViewModel SelectedQuestion
        {
            get
            {
                return _selectedQuestion;
            }

            set
            {
                if (_selectedQuestion == value)
                {
                    return;
                }

                _selectedQuestion = value;
                RaisePropertyChanged(SelectedQuestionPropertyName);
            }
        }
        
        /// <summary>
        /// The <see cref="SqLiteDatabasePath" /> property's name.
        /// </summary>
        public const string SqLiteDatabasePathPropertyName = "SqLiteDatabasePath";


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
            set
            {
                Set(SqLiteDatabasePathPropertyName, ref _sqLiteDatabasePath, value);
            }
        }

        /// <summary>
        /// Gets the Question Path property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string QuestionPath
        {
            get
            {
                return
                    SelectedCategory.CategoryDescription + Environment.NewLine +
                    "        " + 
                    SelectedSubCategory.SubCategoryDescription;
            }
        }
        #endregion ViewModel Properties

        #region Event Methods

        private void PreviewHtml()
        {
            var previewHtmlWindow = new PreviewHtmlWindow(SelectedQuestion.DescriptionHtml);
            previewHtmlWindow.Show();
        }

        private void EditHtml()
        {
            var editHtmlWindow = new EditHtmlWindow(SelectedQuestion, "DescriptionHtml");
            editHtmlWindow.Show();
        }

        private void UpdateHtml()
        {
            SelectedQuestion.DescriptionHtml = "<p>" + WebUtility.HtmlEncode(SelectedQuestion.Description) + "</p>";
        }

        private void ShowAnswersWindow()
        {
            var answersWindow = new AnswersWindow();
            answersWindow.DataContext = new AnswersViewModel(SelectedQuestion);
            answersWindow.Show();
        }
        #endregion Event Methods
    }
}
