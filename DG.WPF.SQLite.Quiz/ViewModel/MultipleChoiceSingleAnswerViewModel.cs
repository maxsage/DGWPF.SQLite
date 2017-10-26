using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Ioc;
using System.Windows.Data;


namespace DG.WPF.SQLite.Quiz.ViewModel
{
    public class MultipleChoiceSingleAnswerViewModel : ViewModelBase
    {
        private readonly AnswerDataService _answerDataService = new AnswerDataService();

        #region Constructors

        public MultipleChoiceSingleAnswerViewModel()
        {
        }

        #endregion Constructors
    }
}