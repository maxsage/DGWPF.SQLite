using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace DG.WPF.SQLite.Model
{
    public class Player : ObservableObject
    {
        private string _Id;

        /// <summary>
        /// Sets and gets the Id property.
        /// </summary>
        public string Id
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

        private string _firstName = "";

        /// <summary>
        /// Sets and gets the FirstName property.
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                _firstName = value;
            }
        }


        private string _lastName = "";

        /// <summary>
        /// Sets and gets the LastName property.
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
            }
        }

        private string _email = "";

        /// <summary>
        /// Sets and gets the Email property.
        /// </summary>
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

    }
}
