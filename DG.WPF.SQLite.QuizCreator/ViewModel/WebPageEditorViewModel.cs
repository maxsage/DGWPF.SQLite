using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.IO;
using System.Windows;
using DG.WPF.SQLite.Model;
using DG.WPF.SQLite.Utilities;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class WebPageEditorViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the WebPageHtmlViewModel class.
        /// </summary>
        public WebPageEditorViewModel(AdditionalInfoViewModel additionalInfoViewModel)
        {
            SelectedAdditionalInfo = additionalInfoViewModel;
          
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.OnClosing);
            HtmlChangedCommand = new RelayCommand(this.HtmlChanged);
            UploadCommand = new RelayCommand(this.Upload);

            Download();
        }

        /// <summary>
        /// The <see cref="Html" /> property's name.
        /// </summary>
        public const string HtmlPropertyName = "Html";

        private string _hTML = "";

        /// <summary>
        /// Sets and gets the WebPageHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Html
        {
            get
            {
                return _hTML;
            }

            set
            {
                if (_hTML == value)
                {
                    return;
                }

                RaisePropertyChanging(HtmlPropertyName);
                _hTML = value;
                RaisePropertyChanged(HtmlPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedAdditionalInfo" /> property's name.
        /// </summary>
        public const string SelectedAdditionalInfoPropertyName = "SelectedAdditionalInfo";

        private AdditionalInfoViewModel _selectedAdditionalInfo;

        /// <summary>
        /// Sets and gets the SelectedAdditionalInfo property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public AdditionalInfoViewModel SelectedAdditionalInfo
        {
            get
            {
                return _selectedAdditionalInfo;
            }

            set
            {
                if (_selectedAdditionalInfo == value)
                {
                    return;
                }

                RaisePropertyChanging(SelectedAdditionalInfoPropertyName);
                _selectedAdditionalInfo = value;
                RaisePropertyChanged(SelectedAdditionalInfoPropertyName);

            }
        }



        /// <summary>
        /// The <see cref="TemporaryFileName" /> property's name.
        /// </summary>
        public const string TemporaryFileNamePropertyName = "TemporaryFileName";

        private string _temporaryFileName = "";

        /// <summary>
        /// Sets and gets the WebPageHtml property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string TemporaryFileName
        {
            get
            {
                return _temporaryFileName;
            }

            set
            {
                if (_temporaryFileName == value)
                {
                    return;
                }

                RaisePropertyChanging(TemporaryFileNamePropertyName);
                _temporaryFileName = value;
                RaisePropertyChanged(TemporaryFileName);
            }
        }

        private void HtmlChanged()
        {
            IsDirty = true;
        }

        private void Download()
        {
            var parameterViewModel = new ViewModelLocator().ParameterViewModel;

            var ftpClient = new Ftp(parameterViewModel.FtpHost, parameterViewModel.FtpUserName, parameterViewModel.FtpPassword);

            TemporaryFileName = Path.GetTempFileName();

            var categoryViewModel = new ViewModelLocator().CategoriesViewModel.SelectedCategory;

            ftpClient.Download(
                categoryViewModel.AdditionalInfoFtpAddress + 
                SelectedAdditionalInfo.AdditionalInfoUri, 
                TemporaryFileName);
            Html = File.ReadAllText(TemporaryFileName);
        }

        private void Upload()
        {
            var parameterViewModel = new ViewModelLocator().ParameterViewModel;

            var ftpClient = new Ftp(parameterViewModel.FtpHost, parameterViewModel.FtpUserName, parameterViewModel.FtpPassword);

            TemporaryFileName = Path.GetTempFileName();

            var categoryViewModel = new ViewModelLocator().CategoriesViewModel.SelectedCategory;

            File.AppendAllText(TemporaryFileName, Html);

            ftpClient.Upload(
                categoryViewModel.AdditionalInfoFtpAddress +
                SelectedAdditionalInfo.AdditionalInfoUri,
                TemporaryFileName);

            //Once uploaded set to not dirty as uploading is like saving
            IsDirty = false;
        }

        private void OnClosing(CancelEventArgs e)
        {
            if(this.IsDirty)
            {
                if(MessageBox.Show("Html has changed but hasn't been uploaded. Continue?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        #region RelayCommand Properties
        public RelayCommand<CancelEventArgs> OnClosingCommand
        {
            get;
            private set;
        }

        public RelayCommand HtmlChangedCommand
        {
            get;
            private set;
        }

        public RelayCommand UploadCommand
        {
            get;
            private set;
        }
        #endregion RelayCommand Properties

        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        public const string IsDirtyPropertyName = "IsDirty";

        private bool _isDirty;

        /// <summary>
        /// Gets the IsDirty property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsDirty
        {
            get
            {
                return _isDirty;
            }

            set
            {
                if (_isDirty == value)
                {
                    return;
                }

                _isDirty = value;
                RaisePropertyChanged(IsDirtyPropertyName);
            }
        }
    }
}