using AutoMapper;
using DG.WPF.SQLite;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Navigation;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    public class AdditionalInfosViewModel : ViewModelBase
    {
        private readonly AdditionalInfoDataService _additionalInfoDataService = new AdditionalInfoDataService();
        

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the AdditionalInfosViewModel class.
        /// </summary>
        public AdditionalInfosViewModel(SubCategoryViewModel selectedSubCategory)
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath;
            
            SelectedSubCategory = selectedSubCategory;

            SelectedCategory = new ViewModelLocator().CategoriesViewModel.SelectedCategory;

            SaveAdditionalInfosCommand = new RelayCommand(this.SaveAdditionalInfos);
            NewAdditionalInfoCommand = new RelayCommand(this.NewAdditionalInfo);
            DeleteAdditionalInfoCommand = new RelayCommand(this.DeleteAdditionalInfo);
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.CheckAdditionalInfos);

            NextAdditionalInfoCommand = new RelayCommand(this.NavigateToNextAdditionalInfo);
            PreviousAdditionalInfoCommand = new RelayCommand(this.NavigateToPreviousAdditionalInfo);
            
            PreviewHtmlCommand = new RelayCommand(this.PreviewHtml);
            EditHtmlCommand = new RelayCommand(this.EditHtml);

            PreviewInBrowserCommand = new RelayCommand(this.PreviewInBrowser);
            EditWebPageCommand = new RelayCommand(this.EditWebPage);

            PreviewInReaderCommand = new RelayCommand(this.PreviewInReader);

            var additionalInfos =
                Mapper.Map<List<AdditionalInfo>, List<AdditionalInfoViewModel>>(
                _additionalInfoDataService.GetAdditionalInfosBySubCategoryId(SqLiteDatabasePath, SelectedSubCategory.Id).OrderBy(ai => ai.SequenceNumber).ToList());

            foreach(var additionalInfo in additionalInfos)
            {
                additionalInfo.IsDirty = false;
            }

            AdditionalInfosInternal = new ObservableCollection<AdditionalInfoViewModel>(additionalInfos);
            AdditionalInfos = new CollectionViewSource();
            
        }
        #endregion Constructors

        #region Filtering Logic
        void AdditionalInfos_Filter(object sender, FilterEventArgs e)
        {
            AdditionalInfoViewModel additionalInfo = e.Item as AdditionalInfoViewModel;
            if (additionalInfo != null)
            {
                if (additionalInfo.SubCategoryId == SelectedSubCategory.Id)
                {
                    e.Accepted = true;
                }
                else
                {
                    e.Accepted = false;
                }
            }
        }
        #endregion Filtering Logic

        #region RelayCommand Properties
        public RelayCommand PreviewHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand EditHtmlCommand
        {
            get;
            private set;
        }

        public RelayCommand PreviewInBrowserCommand
        {
            get;
            private set;
        }

        public RelayCommand EditWebPageCommand
        {
            get;
            private set;
        }

        public RelayCommand PreviewInReaderCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveAdditionalInfosCommand
        {
            get;
            private set;
        }

        public RelayCommand NewAdditionalInfoCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteAdditionalInfoCommand        
        {
            get;
            private set;
        }

        public RelayCommand NextAdditionalInfoCommand
        {
            get;
            private set;
        }

        public RelayCommand PreviousAdditionalInfoCommand
        {
            get;
            private set;
        }

        public RelayCommand<CancelEventArgs> OnClosingCommand
        {
            get;
            private set;
        }
        #endregion RelayCommand Properties

        #region CRUD Methods
        private void CheckAdditionalInfos(CancelEventArgs e)
        {
            if (AdditionalInfosInternal.Where(ai => ai.IsDirty == true).Count() > 0)
            {
                if (System.Windows.MessageBox.Show("There are unsaved changes. Proceed?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void SaveAdditionalInfos()
        {
            var dirtyAdditionalInfos = AdditionalInfosInternal.Where(q => q.IsDirty == true).ToList();

            var additionalInfos = Mapper.Map<List<AdditionalInfoViewModel>, List<AdditionalInfo>>(dirtyAdditionalInfos);

            _additionalInfoDataService.SaveAdditionalInfos(SqLiteDatabasePath, additionalInfos);

            // Set dirty questions as not dirty
            foreach (var ai in dirtyAdditionalInfos)
            {
                ai.IsDirty = false;
            }
        }

        private void NewAdditionalInfo()
        {
            // Get the last sequence number
            var lastSequenceNumber = 0;
            try
            {
                var lastOrDefault = AdditionalInfosInternal.Where(ai => ai.SubCategoryId == SelectedSubCategory.Id).OrderBy(ai => ai.SequenceNumber).LastOrDefault();
                if (
                    lastOrDefault != null)
                    lastSequenceNumber = lastOrDefault.SequenceNumber;
            }
            catch { }

            // Get the last Uri used
            var uri = "";
            if(AdditionalInfosInternal.Count > 0)
            {
                uri = AdditionalInfosInternal.Last<AdditionalInfoViewModel>().AdditionalInfoUri;
            }

            var additionalInfo = new AdditionalInfo
            {
                SubCategoryId = SelectedSubCategory.Id,
                SequenceNumber = (lastSequenceNumber + 10),
                SectionDescription = "<AI>",
                AdditionalInfoHtml = "",
                AdditionalInfoUri = uri,
                DisplayInBrowser = false, 
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Environment.UserName
            };
            
            additionalInfo.Id = _additionalInfoDataService.AddAdditionalInfo(SqLiteDatabasePath, additionalInfo);

            var additionalInfoViewModel = Mapper.Map<AdditionalInfo, AdditionalInfoViewModel>(additionalInfo);
            additionalInfoViewModel.IsDirty = false;

            AdditionalInfosInternal.Add(additionalInfoViewModel);
            SelectedAdditionalInfo = additionalInfoViewModel;
        }

        private void DeleteAdditionalInfo()
        {
            if (SelectedAdditionalInfo == null) return;
            if (
                MessageBox.Show(
                    "Are you sure you want to delete the additional info: " + SelectedAdditionalInfo.SectionDescription,
                    "Confirm", MessageBoxButton.OKCancel) != MessageBoxResult.OK) return;
            _additionalInfoDataService.DeleteAdditionalInfo(SqLiteDatabasePath, SelectedAdditionalInfo.Id);

            AdditionalInfosInternal.Remove(SelectedAdditionalInfo);
        }

        #endregion CRUD Methods

        #region ListView Navigation Methods
        private void NavigateToNextAdditionalInfo()
        {
            AdditionalInfos.View.MoveCurrentToNext();
            SelectedAdditionalInfo = (AdditionalInfoViewModel)AdditionalInfos.View.CurrentItem;
        }

        private void NavigateToPreviousAdditionalInfo()
        {
            AdditionalInfos.View.MoveCurrentToPrevious();
            SelectedAdditionalInfo = (AdditionalInfoViewModel)AdditionalInfos.View.CurrentItem;
        }
        #endregion ListView Navigation Methods

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="AdditionalInfosInternal" /> property's name.
        /// </summary>
        public const string AdditionalInfosInternalPropertyName = "AdditionalInfosInternal";
        private ObservableCollection<AdditionalInfoViewModel> _additionalInfosInternal;
        /// <summary>
        /// Gets the AdditionalInfosInternal property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<AdditionalInfoViewModel> AdditionalInfosInternal
        {
            get
            {
                return _additionalInfosInternal;
            }

            set
            {
                if (_additionalInfosInternal == value)
                {
                    return;
                }
                _additionalInfosInternal = value;
                RaisePropertyChanged(AdditionalInfosInternalPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AdditionalInfos" /> property's name.
        /// </summary>
        public const string AdditionalInfosPropertyName = "AdditionalInfos";
        private CollectionViewSource _additionalInfos;
        /// <summary>
        /// Gets the AdditionalInfos property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CollectionViewSource AdditionalInfos
        {
            get
            {
                _additionalInfos.Source = AdditionalInfosInternal;
                _additionalInfos.Filter += AdditionalInfos_Filter;
                return _additionalInfos;
            }

            set
            {
                if (_additionalInfos == value)
                {
                    return;
                }
                _additionalInfos = value;
                RaisePropertyChanged(AdditionalInfosPropertyName);
            }
        }

        private bool Filter(object arg)
        {
            
            return true;
        }


        /// <summary>
        /// Gets the AnswerTypes property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<bool> YesOrNo
        {
            get
            {
                var _yesOrNo = new List<bool>();
                _yesOrNo.Add(true);
                _yesOrNo.Add(false);
                return _yesOrNo;
            }
        }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        //public SubCategoryViewModel SelectedSubCategory
        //{
        //    get
        //    {
        //        //var subCategoriesViewModel = new ViewModelLocator().SubCategoriesViewModel;
        //        return subCategoriesViewModel.SelectedSubCategory;
        //    }
        //}

        /// <summary>
        /// The <see cref="SelectedSubCategory" /> property's name.
        /// </summary>
        public const string SelectedSubCategoryPropertyName = "SelectedSubCategory";

        private SubCategoryViewModel _selectedSubCategory;

        /// <summary>
        /// Gets the SelectedAdditional property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public SubCategoryViewModel SelectedSubCategory
        {
            get
            {
                return _selectedSubCategory;
            }

            set
            {
                if (_selectedSubCategory == value)
                {
                    return;
                }

                _selectedSubCategory = value;
                RaisePropertyChanged(SelectedSubCategoryPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="SelectedAdditionalInfo" /> property's name.
        /// </summary>
        public const string SelectedAdditionalInfoPropertyName = "SelectedAdditionalInfo";

        private AdditionalInfoViewModel _selectedAdditionalInfo;

        /// <summary>
        /// Gets the SelectedAdditional property.
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

                _selectedAdditionalInfo = value;
                RaisePropertyChanged(SelectedAdditionalInfoPropertyName);
            }
        }

        /// <summary>
        /// Gets the AdditionalInfo Path property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AdditionalInfoPath
        {
            get
            {
                return
                    SelectedSubCategory.SubCategoryDescription;
            }
        }

        public CategoryViewModel _selectedCategory;

        public CategoryViewModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if(_selectedCategory == value)
                {
                    return;
                }

                _selectedCategory = value;
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
        #endregion ViewModel Properties

        #region Event Methods
        private void PreviewHtml()
        {
            var previewHtmlWindow = new PreviewHtmlWindow(SelectedAdditionalInfo.AdditionalInfoHtml);
            previewHtmlWindow.Show();
        }

        private void EditHtml()
        {
            EditHtmlWindow editHtmlWindow =
                new EditHtmlWindow(SelectedAdditionalInfo, "Model.AdditionalInfoHtml");
            editHtmlWindow.Show();
            
        }

        private void EditWebPage()
        {
            var webPageEditorWindow = new WebPageEditorWindow();
            webPageEditorWindow.DataContext = new WebPageEditorViewModel(SelectedAdditionalInfo);
            webPageEditorWindow.Show();
        }

        private void PreviewInBrowser()
        {
            if (SelectedAdditionalInfo.DisplayInBrowser != true) return;
            var categoryViewModel = new ViewModelLocator().CategoriesViewModel.SelectedCategory;
            try
            {
                var uri = new Uri(categoryViewModel.AdditionalInfoUri + SelectedAdditionalInfo.AdditionalInfoUri);
                Process.Start(uri.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("There were problems opening this web page: " + ex.Message);
            }
        }

        private void PreviewInReader()
        {
            var category = new ViewModelLocator().CategoriesViewModel.SelectedCategory;
            var SkyDriveFolder = Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\SkyDrive", "UserFolder", null).ToString();
            
            //Uri uri = new Uri(categoryViewModel.Model.AdditionalInfoUri + SelectedAdditionalInfo.Model.AdditionalInfoUri);

            string filePath;
            
            if (category.AdditionalInfoUri.Contains("[SkyDrive]"))
            {
                filePath = category.AdditionalInfoUri.Replace("[SkyDrive]", SkyDriveFolder);
            }
            else
            {
                filePath = category.AdditionalInfoUri;
            }


            Process[] processes = Process.GetProcessesByName("acrord32");

            foreach(Process p in processes)
            {
                p.Kill();
            }

            
            try
            {
                Process myProcess = new Process();
                myProcess.StartInfo.FileName = "AcroRd32.exe";
                myProcess.StartInfo.Arguments = "/A \"" + SelectedAdditionalInfo.AdditionalInfoUri + "\" " +  filePath;
                myProcess.Start();
            } 
            catch(Exception ex)
            {
                MessageBox.Show("There were problems opening the file: " + ex.Message);
            }

        }



        #endregion Event Methods

    }
}
