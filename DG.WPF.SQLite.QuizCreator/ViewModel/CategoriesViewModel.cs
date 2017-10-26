using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DG.WPF.SQLite.DataService;
using System.Windows;
using AutoMapper;
using DG.WPF.SQLite.Model;


namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class CategoriesViewModel : ViewModelBase
    {
        private readonly CategoryDataService _categoryDataService = new CategoryDataService();

        /// <summary>
        /// Initializes a new instance of the AnswerViewModel class.
        /// </summary>
        public CategoriesViewModel()
        {
            OpenQuizPackCommand = new RelayCommand(this.OpenQuizPack);
            ShowSubCategoriesCommand = new RelayCommand(this.ShowSubCategoriesWindow);
            PreviewHtmlCommand = new RelayCommand(this.PreviewHtml);
            EditHtmlCommand = new RelayCommand(this.EditHtml);
            SaveCategoriesCommand = new RelayCommand(this.SaveCategories);
            NewCategoryCommand = new RelayCommand(this.NewCategory);
            DeleteCategoryCommand = new RelayCommand(this.DeleteCategory);
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.CheckCategories);
        }

        public RelayCommand OpenQuizPackCommand
        {
            get; 
            private set;
        }

        public RelayCommand ShowSubCategoriesCommand
        {
            get;
            private set;
        }

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

        public RelayCommand SaveCategoriesCommand
        {
            get;
            private set;
        }

        public RelayCommand NewCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand<CancelEventArgs> OnClosingCommand
        {
            get;
            private set;
        }

        private void CheckCategories(CancelEventArgs e)
        {
            if (Categories == null || Categories.All(c => c.IsDirty != true)) return;
            if (System.Windows.MessageBox.Show("There are unsaved changes. Proceed?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
            {
                e.Cancel = true;
            }
        }

        private void SaveCategories()
        {
            var dirtyCategories = 
                Categories.Where(q => q.IsDirty == true).ToList();
            
            var categories =
                Mapper.Map<List<CategoryViewModel>, List<Category>>(dirtyCategories);

            _categoryDataService.SaveCategories(SqLiteDatabasePath, categories);

            // Set dirty questions as not dirty
            foreach (CategoryViewModel q in dirtyCategories)
            {
                q.IsDirty = false;
            }
        }

        private void NewCategory()
        {
            var category = new Category
            {
                CategoryNotesHtml = new ViewModelLocator().ParameterViewModel.HtmlStub,
                CategoryDescription = "New Category Description",
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Environment.UserName
            };

            category.Id = _categoryDataService.AddCategory(SqLiteDatabasePath, category);

            var categoryViewModel = new CategoryViewModel();
            categoryViewModel = Mapper.Map<Category, CategoryViewModel>(category);
            categoryViewModel.IsDirty = false;

            Categories.Add(categoryViewModel);
            SelectedCategory = categoryViewModel;
        }

        private void DeleteCategory()
        {
            if (SelectedCategory == null) return;
            if (
                MessageBox.Show(
                    "Are you sure you want to delete the question: " + SelectedCategory.CategoryDescription, "Confirm",
                    MessageBoxButton.OKCancel) != MessageBoxResult.OK) return;
            _categoryDataService.DeleteCategory(SqLiteDatabasePath, SelectedCategory.Id);
            Categories.Remove(SelectedCategory);
        }

        /// <summary>
        /// The <see cref="IsDirty" /> property's name.
        /// </summary>
        private const string IsDirtyPropertyName = "IsDirty";

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

         /// <summary>
        /// The <see cref="Categories" /> property's name.
        /// </summary>
        public const string CategoriesPropertyName = "Categories";

        private ObservableCollection<CategoryViewModel> _categories;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<CategoryViewModel> Categories
        {
            get
            {
                return _categories;
            }

            set
            {
                if (_categories == value)
                {
                    return;
                }

                _categories = value;
                RaisePropertyChanged(CategoriesPropertyName);
                IsDirty = true;
            }
        }

        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        private CategoryViewModel _selectedCategory;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CategoryViewModel SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }

            set
            {
                if (_selectedCategory == value)
                {
                    return;
                }

                Set(SelectedCategoryPropertyName, ref _selectedCategory, value);
                IsDirty = true;
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
                IsDirty = true;
            }
        }

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentEnvironment
        {
            get { return "Sort it out"; }
        }

        /// <summary>
        /// Gets the ServerConnection property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string ServerConnection
        {
            get
            {
                //DGContext context = new DGContext();
                //return context.Database.Connection.DataSource.ToString();
                return "Invalid";
            }
        }

        ///// <summary>
        ///// The <see cref="FtpHost" /> property's name.
        ///// </summary>
        //public const string FtpHostPropertyName = "FtpHost";


        //private string _ftpHost = "";

        //public string FtpHost
        //{
        //    get
        //    {
        //        return _ftpHost;
        //    }
        //    set
        //    {
        //        Set(SqLiteDatabasePathPropertyName, ref _ftpHost, value);
        //        IsDirty = true;
        //    }
        //}

        public string AdditionalInfoFtpAddressToolTipHeading
        {
            get
            {
                return "Additional Info Ftp Address";
            }
        }

        public string AdditionalInfoFtpAddressToolTipText
        {
            get
            {
                return "This field allows you to specify the Ftp folder for uploading additional info topics (e.g. httpdocs/001.ProCSharp2010/). The FtpHost can be specified in the parameters screen (e.g. ftp://ftp.diggen.co.uk/)";
            }
        }

        public string AdditionalInfoUriToolTipHeading
        {
            get
            {
                return "Additional Info Uri Address";
            }
        }

        public string AdditionalInfoUriToolTipText
        {
            get
            {
                return "This field allows you to specify the first portion of the Uri for additional info Html files (e.g. http://www.diggen.co.uk/001.ProCSharp2010/). The Html document name itself is stored against each individual additional info record (e.g. 001 Understanding the Previous State of Affairs.html)";
            }
        }
        
        private void PreviewHtml()
        {
            if (SelectedCategory != null)
            {
                //var previewHtmlWindow = new PreviewHtmlWindow(SelectedCategory, "CategoryNotesFullHtml");
                var previewHtmlWindow = new PreviewHtmlWindow(SelectedCategory.CategoryNotesHtml);
                previewHtmlWindow.Show();
            }
            else
            {
                MessageBox.Show("No Category Selected");
            }
        }

        private void EditHtml()
        {
            if (SelectedCategory != null)
            {
                var editHtmlWindow = new EditHtmlWindow(SelectedCategory, "CategoryNotesHtml");
                editHtmlWindow.Show();
            }
            else
            {
                MessageBox.Show("No Category Selected");
            }
        }

        private void ShowSubCategoriesWindow()
        {
            var categoryViewModel = this.SelectedCategory;
            if (categoryViewModel == null) return;
            var subCategoriesWindow = new SubCategoriesWindow
            {
                DataContext = new SubCategoriesViewModel(categoryViewModel)
            };

            subCategoriesWindow.Show();
        }

        private void OpenQuizPack()
        {
            // Create OpenFileDialog 
            var dlg = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".db",
                Filter = "DB Files (*.db)|*.db",
                InitialDirectory = Properties.Settings.Default.DefaultDirectory
            };

            // Display OpenFileDialog by calling ShowDialog method 
            var result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                // Open document 
                SqLiteDatabasePath = dlg.FileName;

                var categories =
                    Mapper.Map<List<Category>, List<CategoryViewModel>>(
                    _categoryDataService.GetCategories(SqLiteDatabasePath));

                foreach (var category in categories)
                {
                    category.IsDirty = false;
                }

                Categories = new ObservableCollection<CategoryViewModel>(categories);
            }
        }
    }
}