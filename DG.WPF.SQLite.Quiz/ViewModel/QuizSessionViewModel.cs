using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
using GalaSoft.MvvmLight.Ioc;
using System.Media;
using System.Diagnostics;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    public interface ICloseable
    {
        event EventHandler<EventArgs> RequestClose;
    }

    public class QuizSessionViewModel : ViewModelBase, ICloseable
    {


        /// <summary>
        /// The current view.
        /// </summary>
        private ViewModelBase _userAnswerViewModel;

        /// <summary>
        /// The CurrentView property.  The setter is private since only this 
        /// class can change the view via a command.  If the View is changed,
        /// we need to raise a property changed event (via INPC).
        /// </summary>
        public ViewModelBase UserAnswerViewModel
        {
            get
            {
                return _userAnswerViewModel;
            }
            set
            {
                if (_userAnswerViewModel == value)
                    return;
                _userAnswerViewModel = value;
                RaisePropertyChanged("UserAnswerViewModel");
            }
        }

        private readonly QuestionDataService _questionDataService = new QuestionDataService();
        private readonly AnswerDataService _answerDataService = new AnswerDataService();
        private readonly QuizSessionDataService _quizSessionDataService = new QuizSessionDataService();
        private readonly QuizSessionQuestionDataService _quizSessionQuestionDataService = new QuizSessionQuestionDataService();

        #region Constructors
        
        public QuizSessionViewModel()
        {
            SubmitAnswerCommand = new RelayCommand(SubmitAnswer);
            PauseCommand = new RelayCommand(Pause);
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
        }

        public void StartQuiz()
        {
            QuestionStartDateTime = DateTime.Now;

            // Get answers for first question
            var answers =
                Mapper.Map<List<Answer>, List<AnswerViewModel>>(
                    _answerDataService.GetAnswersByQuestionId(SqLiteDatabasePath, CurrentQuizSessionQuestion.Id)).OrderBy(x => Guid.NewGuid());

            Answers = new ObservableCollection<AnswerViewModel>(answers);
        }

        #endregion Constructors

        #region Filtering Logic
        void Questions_Filter(object sender, FilterEventArgs e)
        {
            var question = e.Item as QuizSessionQuestionViewModel;
            if (question != null)
            {
                e.Accepted = question.SubCategoryId == CurrentSubCategory.Id;
            }
        }
        #endregion Filtering Logic

        #region RelayCommandProperties

        public RelayCommand SubmitAnswerCommand { get; private set;}
        public RelayCommand PauseCommand { get; private set; }
        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        #endregion RelayCommandProperties

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="QuestionsInternal" /> property's name.
        /// </summary>
        private const string QuestionsInternalPropertyName = "QuestionsInternal";

        private ObservableCollection<QuizSessionQuestionViewModel> _questionsInternal;

        /// <summary>
        /// Gets the QuestionsInternal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<QuizSessionQuestionViewModel> QuestionsInternal
        {
            get
            {
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
                RaisePropertyChanged(QuestionsInternalPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="Answers" /> property's name.
        /// </summary>
        private const string AnswersPropertyName = "Answers";

        private ObservableCollection<AnswerViewModel> _answers;
        /// <summary>
        /// Gets the Answers property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AnswerViewModel> Answers
        {
            get
            {
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
        

        /// <summary>
        /// The <see cref="CurrentQuizSessionQuestion" /> property's name.
        /// </summary>
        private const string CurrentQuestionPropertyName = "CurrentQuizSessionQuestion";

        private QuizSessionQuestionViewModel _currentQuizSessionQuestion;

        /// <summary>
        /// Gets the CurrentQuizSessionQuestion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public QuizSessionQuestionViewModel CurrentQuizSessionQuestion
        {
            get
            {
                return _currentQuizSessionQuestion;
            }

            set
            {
                if (_currentQuizSessionQuestion == value)
                {
                    return;
                }

                _currentQuizSessionQuestion = value;
                RaisePropertyChanged(CurrentQuestionPropertyName);

                if (CurrentQuizSessionQuestion == null && Questions.View.IsCurrentAfterLast)
                {
                    ProcessEndOfQuiz();
                    return;
                }

                var answers =
                    Mapper.Map<List<Answer>, List<AnswerViewModel>>(
                        _answerDataService.GetAnswersByQuestionId(SqLiteDatabasePath, CurrentQuizSessionQuestion.Id)).OrderBy(x => Guid.NewGuid());

                Answers = new ObservableCollection<AnswerViewModel>(answers);


                if (CurrentQuizSessionQuestion.QuestionTypeId == 1)
                {
                    UserAnswerViewModel = new SingleChoiceSingleAnswerViewModel();
                }
                if (CurrentQuizSessionQuestion.QuestionTypeId == 2)
                {
                    UserAnswerViewModel = new MultipleChoiceSingleAnswerViewModel();
                }
                if (CurrentQuizSessionQuestion.QuestionTypeId == 4)
                {
                    UserAnswerViewModel = new MultipleChoiceMultipleAnswerViewModel();
                }

                CurrentAdditionalInfo = 
                    Mapper.Map(AdditionalInfoDataService.Current.GetAdditionalInfoById(
                        SqLiteDatabasePath, CurrentQuizSessionQuestion.AdditionalInfoId),
                        new AdditionalInfoViewModel());

            }
        }

        /// <summary>
        /// The <see cref="AnswerText" /> property's name.
        /// </summary>
        private const string AnswerTextPropertyName = "AnswerText";

        private string _answerText;

        /// <summary>
        /// Gets the AnswerText property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AnswerText
        {
            get
            {
                return _answerText;
            }

            set
            {
                if (_answerText == value)
                {
                    return;
                }

                _answerText = value;
                RaisePropertyChanged(AnswerTextPropertyName);
            }
        }


        /// <summary>
        /// Gets the PlayerId property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PlayerId
        {
            get { return new ViewModelLocator().ConfigureQuizViewModel.PlayerId; }
        }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CategoryViewModel CurrentCategory
        {
            get { return new ViewModelLocator().ConfigureQuizViewModel.SelectedCategory; }
        }

        /// <summary>
        /// Gets the CurrentSubCategory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SubCategoryViewModel CurrentSubCategory
        {
            get
            {
                return new ViewModelLocator().ConfigureQuizViewModel.CurrentSubCategory;
            }
        }

        /// <summary>
        /// The <see cref="Id" /> property's name.
        /// </summary>
        private const string IdPropertyName = "Id";

        private int _id;

        /// <summary>
        /// Gets the Id property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int Id
        {
            get { return _id; }

            set
            {
                if (_id == value)
                {
                    return;
                }

                _id = value;
                RaisePropertyChanged(IdPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="QuizStartDateTime" /> property's name.
        /// </summary>
        private const string QuizStartDateTimePropertyName = "QuizStartDateTime";

        private DateTime _quizStartDateTime;

        /// <summary>
        /// Gets the QuizStartDateTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime QuizStartDateTime
        {
            get
            {
                return _quizStartDateTime;
            }

            set
            {
                if (_quizStartDateTime == value)
                {
                    return;
                }

                _quizStartDateTime = value;
                RaisePropertyChanged(QuizStartDateTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="QuizEndDateTime" /> property's name.
        /// </summary>
        private const string QuizEndDateTimePropertyName = "QuizEndDateTime";

        private DateTime _quizEndDateTime;

        /// <summary>
        /// Gets the QuizEndDateTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime QuizEndDateTime
        {
            get
            {
                return _quizEndDateTime;
            }

            set
            {
                if (_quizEndDateTime == value)
                {
                    return;
                }

                _quizEndDateTime = value;
                RaisePropertyChanged(QuizEndDateTimePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="QuestionStartDateTime" /> property's name.
        /// </summary>
        private const string QuestionStartDateTimePropertyName = "QuizStartDateTime";

        private DateTime _questionStartDateTime;

        /// <summary>
        /// Gets the QuestionStartDateTime property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime QuestionStartDateTime
        {
            get
            {
                return _questionStartDateTime;
            }

            set
            {
                if (_questionStartDateTime == value)
                {
                    return;
                }

                _questionStartDateTime = value;
                RaisePropertyChanged(QuestionStartDateTimePropertyName);
            }
        }

        /// <summary>
        /// Gets the QuizTypeId property.
        /// </summary>
        public int QuizTypeId
        {
            get
            {
                return new ViewModelLocator().ConfigureQuizViewModel.QuizTypeId;
            }
        }

        /// <summary>
        /// Gets the SqLiteDatabasePath property.
        /// </summary>
        private string SqLiteDatabasePath
        {
            get
            {
                return new ViewModelLocator().ConfigureQuizViewModel.SqLiteDatabasePath;
            }
        }

        /// <summary>
        /// The <see cref="CorrectQuestions" /> property's name.
        /// </summary>
        private const string CorrectQuestionsPropertyName = "CorrectQuestions";

        private int _correctQuestions;

        /// <summary>
        /// Sets and gets the CorrectQuestions property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int CorrectQuestions
        {
            get
            {
                return _correctQuestions;
            }
            set
            {
                Set(CorrectQuestionsPropertyName, ref _correctQuestions, value);
            }
        }


        /// <summary>
        /// The <see cref="CurrentAdditionalInfo" /> property's name.
        /// </summary>
        private const string CurrentAdditionalInfoPropertyName = "CurrentAdditionalInfo";

        private AdditionalInfoViewModel _currentAdditionalInfo;

        /// <summary>
        /// Sets and gets the CurrentAdditionalInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public AdditionalInfoViewModel CurrentAdditionalInfo
        {
            get { return _currentAdditionalInfo; }
            set { Set(CurrentAdditionalInfoPropertyName, ref _currentAdditionalInfo, value); }
        }
                    



        #endregion ViewModel Properties

        #region Event Methods

        private void SubmitAnswer()
        {

            var correct = false;

            if (Answers.Count > 0)

                switch (CurrentQuizSessionQuestion.QuestionTypeId)
                {
                    case 1:
                    {
                        // Single Choice/Single Answer
                        if (ProcessSingleChoiceSingleAnswer())
                        {
                            CorrectQuestions++;
                            correct = true;
                        }
                        break;
                    }
                    case 2:
                    {
                        // Multiple Choice/Single Answer
                        if (ProcessMultipleChoiceSingleAnswer())
                        {
                            CorrectQuestions++;
                            correct = true;
                        }
                        break;
                    }
                    case 4:
                    {
                        // Multiple Choice/Multiple Answer
                        if (ProcessMultipleChoiceMultipleAnswer())
                        {
                            CorrectQuestions++;
                            correct = true; 
                        }
                        break;
                    }
                }

            // Get the QuizSessionQuestion for this Question
            var quizSessionQuestion = _quizSessionQuestionDataService.GetQuizSessionQuestionByComposite(SqLiteDatabasePath, Id, CurrentQuizSessionQuestion.Id);
            
            // Update the quizSessionQuestion with the relevant details
            quizSessionQuestion.CorrectlyAnswered = correct;
            quizSessionQuestion.TimeStarted = QuestionStartDateTime;
            quizSessionQuestion.TimeFinished = DateTime.Now;

            _quizSessionQuestionDataService.UpdateQuizSessionQuestion(SqLiteDatabasePath, quizSessionQuestion);

            // Play correct or incorrect sound
            if (correct)
            {
                PlayCorrect();
            }
            else
            {
                PlayIncorrect();
            }

            // If we are in Revision Mode then show Additional Info
            if (QuizTypeId == 2)
            {
        
                //    else
                //    {
                
                //            var myProcess = new Process
                //            {
                //                StartInfo =
                //                {
                //                    FileName = "AcroRd32.exe",
                //                    Arguments = "/A \"" + ai.AdditionalInfoUri + "\" " + filePath
                //                }
                //            };
                //            myProcess.Start();
                //        }
                //        catch (Exception ex)
                //        {
                //            MessageBox.Show("There were problems opening the file: " + ex.Message);
                //        }
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("No additional info with an Id of " + CurrentQuizSessionQuestion.AdditionalInfoId + " was found for this question");
                //}
            }

            // Get ready for the next question
            AnswerText = "";
            Questions.View.MoveCurrentToNext();
            QuestionStartDateTime = DateTime.Now;

        }

        private bool ProcessSingleChoiceSingleAnswer()
        {
            //var userAnswers = Answers.Where(a => a.Selected).ToArray();

            var correctAnswers = Answers.Where(a => a.CorrectAnswer).ToArray();

            var isUserCorrect = false;

            foreach(AnswerViewModel correctAnswer in correctAnswers)
            {
                if(correctAnswer.Description.ToLower() == AnswerText.ToLower())
                {
                    isUserCorrect = true;
                } 
            }

            return isUserCorrect;
        }

        private bool ProcessMultipleChoiceMultipleAnswer()
        {
            var userAnswers = Answers.Where(a => a.Selected).ToArray();
            var correctAnswers = Answers.Where(a => a.CorrectAnswer).ToArray();

            var isUserCorrect = (!userAnswers.Except(correctAnswers).Any())
                                && (userAnswers.Count() == correctAnswers.Count());

            return isUserCorrect;

        }     
        
        private bool ProcessMultipleChoiceSingleAnswer()
        {
            return Answers.Count(a => a.Selected && a.CorrectAnswer) ==
                   Answers.Count(a => a.CorrectAnswer);
        }


        private void ProcessEndOfQuiz()
        {
            QuizEndDateTime = DateTime.Now;

            var timeTaken = (int)(QuizEndDateTime - QuizStartDateTime).TotalSeconds;

            // Get the existing QuizSession
            var quizSession = _quizSessionDataService.GetQuizSessionById(SqLiteDatabasePath, Id);

            // Update the relevant quizSession details
            quizSession.QuizEndDateTime = QuizEndDateTime;
            quizSession.TimeTaken = timeTaken;
            quizSession.CorrectQuestions = CorrectQuestions;
            
            _quizSessionDataService.UpdateQuizSession(SqLiteDatabasePath, quizSession);

            MessageBox.Show("End of Quiz \n" +
                "Started: " + QuizStartDateTime + "\n" +
                "Ended: " + QuizEndDateTime + "\n" +
                CorrectQuestions + " Correct Answers out of " + QuestionsInternal.Count);

            RequestClose(this, null);
        }

        private void PlayCorrect()
        {
            var player = new SoundPlayer {SoundLocation = "c:\\Windows\\Media\\chimes.wav"};
            try
            {
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PlayIncorrect()
        {
            var player = new SoundPlayer {SoundLocation = "c:\\Windows\\Media\\Windows Hardware Fail.wav"};
            try
            {
                player.Load();
                player.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        private void Pause()
        {
            throw new NotImplementedException();
        }

        #endregion Event Methods

        public event EventHandler<EventArgs> RequestClose;
    }

}
