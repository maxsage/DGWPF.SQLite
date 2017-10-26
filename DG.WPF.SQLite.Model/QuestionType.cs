using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DG.WPF.SQLite.Model
{
    public class QuestionType : ObservableObject
    {
        private int _Id = 0;

        /// <summary>
        /// Sets and gets the Id property.
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

        private string _description;

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
    }
}
