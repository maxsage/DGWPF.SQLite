using GalaSoft.MvvmLight;
using System;
using System.Configuration;
using System.Deployment.Application;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using DG.WPF.SQLite.DataService;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        ParameterDataService _parameterDataService = new ParameterDataService();

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public AboutViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);

            //Get version info               
            var assemblyInfo = System.Reflection.Assembly.GetExecutingAssembly();

            var ourVersion = "";

            ourVersion = ApplicationDeployment.IsNetworkDeployed ? 
                ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString() : assemblyInfo.GetName().Version.ToString();

            ApplicationVersion = ourVersion;
        }

        public RelayCommand<Window> CloseWindowCommand
        {
            get; 
            private set;
        }

        private static void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }

        /// <summary>
        /// The <see cref="ApplicationVersion" /> property's name.
        /// </summary>
        private const string ApplicationVersionPropertyName = "ApplicationVersion";

        private string _applicationVersion = "";

        /// <summary>
        /// Sets and gets the ApplicationVersion property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                return _applicationVersion;
            }

            set
            {
                if (_applicationVersion == value)
                {
                    return;
                }

                RaisePropertyChanging(ApplicationVersionPropertyName);
                _applicationVersion = value;
                RaisePropertyChanged(ApplicationVersionPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="UserConfigLocation" /> property's name.
        /// </summary>      
        private const string UserConfigLocationPropertyName = "UserConfigLocation";

        /// <summary>
        /// Gets the UserConfigLocation property.
        /// </summary>
        public string UserConfigLocation
        {
            get
            {
                return ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;
            }
        }

        /// <summary>
        /// Gets the AppConfigLocation property.
        /// </summary>
        public string AppConfigLocation
        {
            get
            {
                return AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
            }
        }
    }
}