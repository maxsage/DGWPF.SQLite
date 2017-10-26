using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class ParameterViewModel : ViewModelBase
    {
        private readonly ParameterDataService _parameterDataService;

        /// <summary>
        /// Initializes a new instance of the EnvironmentViewModel class.
        /// </summary>
        public ParameterViewModel()
        {
            SqLiteDatabasePath = new ViewModelLocator().ConfigureQuizViewModel.SqLiteDatabasePath;
            _parameterDataService = new ParameterDataService();

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
                Parameter _environment =
                    Parameters.Where(p => p.ParameterNum == 1).FirstOrDefault();
                return _environment.CharValue;
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
                Parameter _preHtml =
                    Parameters.Where(p => p.ParameterNum == 2).FirstOrDefault();
                return _preHtml.CharValue;
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
                Parameter _postHtml =
                    Parameters.Where(p => p.ParameterNum == 3).FirstOrDefault();
                return _postHtml.CharValue;
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
                Parameter _ftpHost =
                    Parameters.Where(p => p.ParameterNum == 4).FirstOrDefault();
                return _ftpHost.CharValue;
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
                Parameter _ftpUserName =
                    Parameters.Where(p => p.ParameterNum == 5).FirstOrDefault();
                return _ftpUserName.CharValue;
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
                Parameter _ftpPassword =
                    Parameters.Where(p => p.ParameterNum == 6).FirstOrDefault();
                return _ftpPassword.CharValue;
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
                Parameter _ftpFolder =
                    Parameters.Where(p => p.ParameterNum == 7).FirstOrDefault();
                return _ftpFolder.CharValue;
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
                Parameter _htmlStub =
                    Parameters.Where(p => p.ParameterNum == 14).FirstOrDefault();
                return _htmlStub.CharValue;
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
                Parameter _additionalInfoUri =
                    Parameters.Where(p => p.ParameterNum == 9).FirstOrDefault();
                return _additionalInfoUri.CharValue;
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