using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DG.WPF.SQLite.Model
{
    public class PlayerAnswer : ObservableObject
    {
        private int _Id = 0;

        /// <summary>
        /// Sets and gets the AnswserId property.
        /// </summary>
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        private int _questionId = 0;

        /// <summary>
        /// Sets and gets the QuestionId property.
        /// </summary>
        public int QuestionId
        {
            get
            {
                return _questionId;
            }
            set
            {
                _questionId = value;
            }
        }

        private string _description = "";

        /// <summary>
        /// Sets and gets the Description property.
        /// </summary>
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        private string _playerAnswerText = "";

        /// <summary>
        /// Sets and gets the PlayerAnswerText property.
        /// </summary>
        public string PlayerAnswerText
        {
            get
            {
                return _playerAnswerText;
            }
            set
            {
                _playerAnswerText = value;
            }
        }

        private bool _correctAnswer;

        /// <summary>
        /// Sets and gets the CorrectAnswer property.
        /// </summary>
        public bool CorrectAnswer
        {
            get
            {
                return _correctAnswer;
            }
            set
            {
                _correctAnswer = value;
            }
        }

        private bool _selectedByPlayer;

        /// <summary>
        /// Sets and gets the CorrectAnswer property.
        /// </summary>
        public bool SelectedByPlayer
        {
            get
            {
                return _selectedByPlayer;
            }
            set
            {
                _selectedByPlayer = value;
            }
        }

    }
}
