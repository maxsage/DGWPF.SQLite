using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
namespace DG.WPF.SQLite.QuizCreator.ViewModel
{

    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ParameterViewModel : ViewModelBase
    {
        private readonly ParameterDataService _parameterDataService = new ParameterDataService();

        /// <summary>
        /// Initializes a new instance of the EnvironmentViewModel class.
        /// </summary>
        public ParameterViewModel()
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath;

            Parameters = _parameterDataService.GetParameters(SqLiteDatabasePath).AsQueryable();
        }

        /// <summary>
        /// The <see cref="Parameters" /> property's name.
        /// </summary>
        public const string ParametersPropertyName = "Parameters";

        private IQueryable<Parameter> _parameters;


        /// <summary>
        /// Gets the Parameters property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public IQueryable<Parameter> Parameters
        {
            get
            {
                return _parameters;
            }

            set
            {
                if (_parameters == value)
                {
                    return;
                }

                _parameters = value;
                RaisePropertyChanged(ParametersPropertyName);
            }
        }


        /// <summary>
        /// Gets the Environment property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Environment
        {
            get
            {
                var environment = Parameters.FirstOrDefault(p => p.ParameterNum == 1);
                return environment.CharValue;
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
        /// Gets the PreHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PreHtml
        {
            get
            {
                var preHtml = Parameters.FirstOrDefault(p => p.ParameterNum == 2);
                return preHtml.CharValue;
            }
        }

        /// <summary>
        /// Gets the PostHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string PostHtml
        {
            get
            {
                var postHtml = Parameters.FirstOrDefault(p => p.ParameterNum == 3);
                return postHtml.CharValue;
            }
        }

        /// <summary>
        /// Gets the FtpHost property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FtpHost
        {
            get
            {
                var ftpHost = Parameters.FirstOrDefault(p => p.ParameterNum == 4);
                return ftpHost.CharValue;
            }
        }

        /// <summary>
        /// Gets the FtpUserName property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FtpUserName
        {
            get
            {
                var ftpUserName = Parameters.FirstOrDefault(p => p.ParameterNum == 5);
                return ftpUserName.CharValue;
            }
        }

        /// <summary>
        /// Gets the FtpPassword property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FtpPassword
        {
            get
            {
                var ftpPassword = Parameters.FirstOrDefault(p => p.ParameterNum == 6);
                return ftpPassword.CharValue;
            }
        }

        /// <summary>
        /// Gets the FtpFolder property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string FtpFolder
        {
            get
            {
                var ftpFolder = Parameters.FirstOrDefault(p => p.ParameterNum == 7);
                return ftpFolder.CharValue;
            }
        }

        /// <summary>
        /// Gets the HtmlStub property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string HtmlStub
        {
            get
            {
                var htmlStub = Parameters.FirstOrDefault(p => p.ParameterNum == 14);
                return htmlStub.CharValue;
            }
        }

        /// <summary>
        /// Gets the AdditionalInfoUri property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoUri
        {
            get
            {
                var additionalInfoUri = Parameters.FirstOrDefault(p => p.ParameterNum == 9);
                return additionalInfoUri.CharValue;
            }
        }


        /// <summary>
        /// Gets the DatabaseCreationDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime? DatabaseCreationDate
        {
            get
            {
                var databaseCreationDate = Parameters.FirstOrDefault(p => p.ParameterNum == 15);
                return databaseCreationDate.DateValue;
            }
        }
    }
}