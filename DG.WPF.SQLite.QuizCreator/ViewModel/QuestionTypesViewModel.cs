using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class QuestionTypesViewModel : ViewModelBase
    {
        private readonly QuestionTypeDataService _questionTypeDataService = new QuestionTypeDataService();

        /// <summary>
        /// Initializes a new instance of the QuestionTypesViewModel class.
        /// </summary>
        public QuestionTypesViewModel()
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath;

            var questionTypes =
                _questionTypeDataService.GetQuestionTypes(SqLiteDatabasePath);

            QuestionTypes = new ObservableCollection<QuestionType>(questionTypes);
        }

        /// <summary>
        /// The <see cref="QuestionTypes" /> property's name.
        /// </summary>
        public const string QuestionTypesPropertyName = "QuestionTypes";

        private ObservableCollection<QuestionType> _questionTypes;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<QuestionType> QuestionTypes
        {
            get
            {
                return new ObservableCollection<QuestionType>(_questionTypes); 
            }

            set
            {
                if (_questionTypes == value)
                {
                    return;
                }

                _questionTypes = value;
                RaisePropertyChanged(QuestionTypesPropertyName);
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
    }
}