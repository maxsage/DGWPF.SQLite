using System.Windows.Forms;
using DG.WPF.SQLite.Quiz.Properties;
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
    public class OptionsViewModel : ViewModelBase
    {
        ParameterDataService _parameterDataService = new ParameterDataService();

        /// <summary>
        /// Initializes a new instance of the OptionsViewModel class.
        /// </summary>
        public OptionsViewModel()
        {
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);
            FileBrowseCommand = new RelayCommand(FileBrowse);

            DefaultQuizPackDirectory = Settings.Default.DefaultQuizPackDirectory;
        }

        public RelayCommand<Window> CloseWindowCommand
        {
            get; 
            private set;
        }

        public RelayCommand FileBrowseCommand
        {
            get; 
            private set;
        }


        private void CloseWindow(Window window)
        {
            // Save settings
            Settings.Default.DefaultQuizPackDirectory = DefaultQuizPackDirectory;
            Settings.Default.Save();


            // Close window
            if (window != null)
            {
                window.Close();
            }
        }

        private void FileBrowse()
        {
            var dlg = new System.Windows.Forms.FolderBrowserDialog()
            {
                SelectedPath = Properties.Settings.Default.DefaultQuizPackDirectory
            };

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == DialogResult.OK)
            {
                DefaultQuizPackDirectory = dlg.SelectedPath;
            }
        }

        /// <summary>
        /// The <see cref="DefaultQuizPackDirectory" /> property's name.
        /// </summary>
        private const string DefaultQuizPackDirectoryPropertyName = "DefaultQuizPackDirectory";

        private string _defaultQuizPackDirectory = "";

        /// <summary>
        /// Sets and gets the DefaultQuizPackDirectory property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string DefaultQuizPackDirectory
        {
            get
            {
                return _defaultQuizPackDirectory;
            }

            set
            {
                if (_defaultQuizPackDirectory == value)
                {
                    return;
                }

                RaisePropertyChanging(DefaultQuizPackDirectoryPropertyName);
                _defaultQuizPackDirectory = value;
                RaisePropertyChanged(DefaultQuizPackDirectoryPropertyName);
            }
        }
    }
}