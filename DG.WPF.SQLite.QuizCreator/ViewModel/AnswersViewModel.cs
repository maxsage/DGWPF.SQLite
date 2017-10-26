
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
using System.Windows;
using System.Windows.Data;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    public class AnswersViewModel : ViewModelBase
    {
        private readonly AnswerDataService _answerDataService = new AnswerDataService();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AnswersViewModel class.
        /// </summary>
        public AnswersViewModel(QuestionViewModel selectedQuestion)
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath ;

            SelectedQuestion = selectedQuestion;

            SaveAnswersCommand = new RelayCommand(this.SaveAnswers);
            NewAnswerCommand = new RelayCommand(this.NewAnswer);
            DeleteAnswerCommand = new RelayCommand(this.DeleteAnswer);
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.CheckAnswers);
            NextAnswerCommand = new RelayCommand(this.NavigateToNextAnswer);
            PreviousAnswerCommand = new RelayCommand(this.NavigateToPreviousAnswer);

            PreviewHtmlCommand = new RelayCommand(this.PreviewHtml);
            EditHtmlCommand = new RelayCommand(this.EditHtml);

            List<AnswerViewModel> answers =
                Mapper.Map<List<Answer>, List<AnswerViewModel>>(
                _answerDataService.GetAnswers(SqLiteDatabasePath ));

            foreach(AnswerViewModel answer in answers)
            {
                answer.IsDirty = false;
            }

            AnswersInternal = new ObservableCollection<AnswerViewModel>(answers);
            Answers = new CollectionViewSource();
        }
        #endregion Constructors

        #region Filtering Logic
        void Answers_Filter(object sender, FilterEventArgs e)
        {
            var answer = e.Item as AnswerViewModel;
            if (answer != null)
            {
                if (answer.QuestionId == SelectedQuestion.Id)
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


        public RelayCommand PreviewHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand EditHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveAnswersCommand
        {
            get;
            private set;
        }

        public RelayCommand NewAnswerCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteAnswerCommand        
        {
            get;
            private set;
        }

        public RelayCommand NextAnswerCommand
        {
            get;
            private set;
        }

        public RelayCommand PreviousAnswerCommand
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
        private void CheckAnswers(CancelEventArgs e)
        {
            if (AnswersInternal.Count(q => q.IsDirty == true) <= 0) return;
            if (System.Windows.MessageBox.Show("There are unsaved changes. Proceed?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void SaveAnswers()
        {
            var dirtyAnswers = AnswersInternal.Where(q => q.IsDirty == true).ToList();

            var answers = Mapper.Map<List<AnswerViewModel>, List<Answer>>(dirtyAnswers);

            _answerDataService.SaveAnswers(SqLiteDatabasePath , answers);

            foreach (var a in dirtyAnswers)
            {
                a.IsDirty = false;
            }
        }

        private void NewAnswer()
        {
            var answer = new Answer
            {
                Description = "New Answer",
                CorrectAnswer = false,
                QuestionId = SelectedQuestion.Id,
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Environment.UserName
            };
            
            answer.Id = _answerDataService.AddAnswer(SqLiteDatabasePath , answer);

            AnswerViewModel answerViewModel = Mapper.Map<Answer, AnswerViewModel>(answer);
            answerViewModel.IsDirty = false;

            AnswersInternal.Add(answerViewModel);
            SelectedAnswer = answerViewModel;
        }

        private void DeleteAnswer()
        {
            if (SelectedAnswer != null)
            {
                if (MessageBox.Show("Are you sure you want to delete the answer: " + SelectedAnswer.Description, "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    _answerDataService.DeleteAnswer(SqLiteDatabasePath , SelectedAnswer.Id);

                    AnswersInternal.Remove(SelectedAnswer);
                }
            }
        }
        #endregion CRUD Methods

        #region ListView Navigation Methods
        private void NavigateToNextAnswer()
        {
            Answers.View.MoveCurrentToNext();
            SelectedAnswer = (AnswerViewModel)Answers.View.CurrentItem;
        }

        private void NavigateToPreviousAnswer()
        {
            Answers.View.MoveCurrentToPrevious();
            SelectedAnswer = (AnswerViewModel)Answers.View.CurrentItem;
        }
        #endregion ListView Navigation Methods

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="AnswersInternal" /> property's name.
        /// </summary>
        public const string AnswersInternalPropertyName = "AnswersInternal";
        private ObservableCollection<AnswerViewModel> _answersInternal;
        /// <summary>
        /// Gets the Questions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AnswerViewModel> AnswersInternal
        {
            get
            {
                return _answersInternal;
            }

            set
            {
                if (_answersInternal == value)
                {
                    return;
                }
                _answersInternal = value;
                RaisePropertyChanged(AnswersInternalPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Answers" /> property's name.
        /// </summary>
        public const string AnswersPropertyName = "Answers";
        private CollectionViewSource _answers;
        /// <summary>
        /// Gets the Answers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CollectionViewSource Answers
        {
            get
            {
                _answers.Source = AnswersInternal;
                _answers.Filter += Answers_Filter;
                return _answers;
            }

            set
            {
                if (_answers == value)
                {
                    return;
                }
                _answers = value;
                RaisePropertyChanged(AnswersPropertyName);
            }
        }

        private bool Filter(object arg)
        {
            
            return true;
        }

        /// <summary>
        /// Gets the AnswerTypes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<bool> AnswerTypes
        {
            get
            {
                var _answerTypes = new List<bool>();
                _answerTypes.Add(true);
                _answerTypes.Add(false);
                return _answerTypes;
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
                if(_selectedQuestion == value)
                {
                    return;
                }

                _selectedQuestion = value;
                RaisePropertyChanged(SelectedQuestionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedAnswer" /> property's name.
        /// </summary>
        public const string SelectedAnswerPropertyName = "SelectedAnswer";

        private AnswerViewModel _selectedAnswer;

        /// <summary>
        /// Gets the SelectedAnswer property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public AnswerViewModel SelectedAnswer
        {
            get
            {
                return _selectedAnswer;
            }

            set
            {
                if (_selectedAnswer == value)
                {
                    return;
                }

                _selectedAnswer = value;
                RaisePropertyChanged(SelectedAnswerPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SqLiteDatabasePath " /> property's name.
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
        /// Gets the Answer Path property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AnswerPath
        {
            get
            {
                return
                    SelectedQuestion.Description + Environment.NewLine;
            }
        }
        #endregion ViewModel Properties

        #region Event Methods
        private void PreviewHtml()
        {
            var previewHtmlWindow = new PreviewHtmlWindow(SelectedAnswer.DescriptionHtml);
            previewHtmlWindow.Show();
        }

        private void EditHtml()
        {
            var editHtmlWindow = new EditHtmlWindow(SelectedAnswer, "DescriptionHtml");
            editHtmlWindow.Show();
        }
        #endregion Event Methods

    }
}
