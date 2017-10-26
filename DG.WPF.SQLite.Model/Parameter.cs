using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DG.WPF.SQLite.Model
{
    public class Parameter : ObservableObject
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

        private int _parameterNum = 0;

        /// <summary>
        /// Sets and gets the ParameterNum property.
        /// </summary>
        public int ParameterNum
        {
            get
            {
                return _parameterNum;
            }
            set
            {
                _parameterNum = value;
            }
        }


        private string _name = "";

        /// <summary>
        /// Sets and gets the Name property.
        /// </summary>
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
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

        private int? _intValue = 0;

        /// <summary>
        /// Sets and gets the IntValue property.
        /// </summary>
        public int? IntValue
        {
            get
            {
                return _intValue;
            }
            set
            {
                _intValue = value;
            }
        }

        private string _charValue;

        /// <summary>
        /// Sets and gets the CharValue property.
        /// </summary>

        public string CharValue
        {
            get
            {
                return _charValue;
            }
            set
            {
                _charValue = value;
            }
        }

        private DateTime? _dateValue;

        /// <summary>
        /// Sets and gets the DateValue property.
        /// </summary>
        public DateTime? DateValue
        {
            get
            {
                return _dateValue;
            }
            set
            {
                _dateValue = value;
            }
        }
    }
}
