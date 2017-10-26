using AutoMapper;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SubCategoriesViewModel : ViewModelBase
    {
        private readonly SubCategoryDataService _subCategoryDataService = new SubCategoryDataService();

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the SubCategoryViewModel class.
        /// </summary>
        public SubCategoriesViewModel(CategoryViewModel selectedCategory)
        {
            SqLiteDatabasePath = new ViewModelLocator().CategoriesViewModel.SqLiteDatabasePath;

            SelectedCategory = selectedCategory;

            SaveSubCategoriesCommand = new RelayCommand(this.SaveSubCategories);
            NewSubCategoryCommand = new RelayCommand(this.NewSubCategory);
            DeleteSubCategoryCommand = new RelayCommand(this.DeleteSubCategory);
            OnClosingCommand = new RelayCommand<CancelEventArgs>(this.CheckSubCategories);
            ShowQuestionsCommand = new RelayCommand(this.ShowQuestionsWindow);
            ShowAdditionalInfosCommand = new RelayCommand(this.ShowAdditionalInfosWindow);

            PreviewHtmlCommand = new RelayCommand(this.PreviewHtml);
            EditHtmlCommand = new RelayCommand(this.EditHtml);

            var subCategories = 
                Mapper.Map<List<SubCategory>, List<SubCategoryViewModel>>(_subCategoryDataService.GetSubCategories(SqLiteDatabasePath));
                 
            foreach(var subCategory in subCategories)
            {
                subCategory.IsDirty = false;
            }

            SubCategoriesInternal = new ObservableCollection<SubCategoryViewModel>(subCategories);
            SubCategories = new CollectionViewSource();
                   
        }
        #endregion Constructors

        #region Filtering Logic
        void SubCategories_Filter(object sender, FilterEventArgs e)
        {
            var subCategory = e.Item as SubCategoryViewModel;
            if(subCategory != null)
            {
                e.Accepted = subCategory.CategoryId == SelectedCategory.Id;
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


        public RelayCommand ShowQuestionsCommand
        {
            get;
            private set;
        }

        public RelayCommand ShowAdditionalInfosCommand
        {
            get;
            private set;
        }

        public RelayCommand SaveSubCategoriesCommand
        {
            get;
            private set;
        }

        public RelayCommand NewSubCategoryCommand
        {
            get;
            private set;
        }

        public RelayCommand DeleteSubCategoryCommand
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
        private void CheckSubCategories(CancelEventArgs e)
        {
            if (SubCategoriesInternal.Where(c => c.IsDirty == true).Count() > 0)
            {
                if (System.Windows.MessageBox.Show("There are unsaved changes. Proceed?", "Warning", MessageBoxButton.OKCancel) == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                { 
                }

            }
        }

        private void SaveSubCategories()
        {
            List<SubCategory> dirtySubCategories =
                AutoMapper.Mapper.Map<IEnumerable<SubCategoryViewModel>, List<SubCategory>>(SubCategoriesInternal.Where(q => q.IsDirty == true));
            
            // Save Questions
            _subCategoryDataService.SaveSubCategories(SqLiteDatabasePath, dirtySubCategories);

            // Set dirty sub categories as not dirty
            foreach (SubCategoryViewModel q in SubCategoriesInternal.Where(q => q.IsDirty == true))
            {
                q.IsDirty = false;
            }
        }

        private void NewSubCategory()
        {
            SubCategory subCategory = new SubCategory
            {
                CategoryId = SelectedCategory.Id,
                SubCategoryDescription = "New SubCategory Description",
                SubCategoryNotesHtml = new ViewModelLocator().ParameterViewModel.HtmlStub,
                CreatedDate = DateTime.Now,
                CreatedBy = Environment.UserName,
                UpdatedDate = DateTime.Now,
                UpdatedBy = Environment.UserName
            };

            subCategory.Id = _subCategoryDataService.AddSubCategory(SqLiteDatabasePath, subCategory);

            SubCategoryViewModel subCategoryViewModel = new SubCategoryViewModel(); 
            subCategoryViewModel =  Mapper.Map<SubCategory, SubCategoryViewModel>(subCategory);
            subCategoryViewModel.IsDirty = false;

            SubCategoriesInternal.Add(subCategoryViewModel);
            SelectedSubCategory = subCategoryViewModel;
        }

        private void DeleteSubCategory()
        {
            if (SelectedSubCategory != null)
            {
                if (MessageBox.Show("Are you sure you want to delete the question: " + SelectedSubCategory.SubCategoryDescription, "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                    _subCategoryDataService.DeleteSubCategory(SqLiteDatabasePath, SelectedSubCategory.Id);
                    SubCategoriesInternal.Remove(SelectedSubCategory);
                }
            }
        }

        #endregion CRUD Methods

        #region ViewModel Properties
        /// <summary>
        /// The <see cref="SubCategoriesInternal" /> property's name.
        /// </summary>
        public const string SubCategoriesInternalPropertyName = "SubCategoriesInternal";

        private ObservableCollection<SubCategoryViewModel> _subCategoriesInternal;

        /// <summary>
        /// Gets the WelcomeTitle property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public ObservableCollection<SubCategoryViewModel> SubCategoriesInternal
        {
            get
            {
                //return new ObservableCollection<SubCategoryViewModel>(_subCategories.Where(sc => sc.Model.CategoryId == SelectedCategory.Model.CategoryId)); 
                return _subCategoriesInternal;
            }

            set
            {
                if (_subCategoriesInternal == value)
                {
                    return;
                }

                _subCategoriesInternal = value;
                RaisePropertyChanged(SubCategoriesInternalPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SubCategories" /> property's name.
        /// </summary>
        public const string SubCategoriesPropertyName = "SubCategories";
        private CollectionViewSource _subCategories;
        /// <summary>
        /// Gets the SubCategories property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public CollectionViewSource SubCategories
        {
            get
            {
                _subCategories.Source = SubCategoriesInternal;
                _subCategories.Filter += SubCategories_Filter;
                return _subCategories;
            }

            set
            {
                if (_subCategories == value)
                {
                    return;
                }

                _subCategories = value;
                RaisePropertyChanged(SubCategoriesInternalPropertyName);
            }
        }

        private CategoryViewModel _selectedCategory;

        /// <summary>
        /// The <see cref="SelectedCategory" /> property's name.
        /// </summary>
        public const string SelectedCategoryPropertyName = "SelectedCategory";

        /// <summary>
        /// Gets the SelectedCategory property.
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
                _selectedCategory = value;
            }
        }


        /// <summary>
        /// The <see cref="SelectedSubCategory" /> property's name.
        /// </summary>
        public const string SelectedSubCategoryPropertyName = "SelectedSubCategory";

        private SubCategoryViewModel _selectedSubCategory;

        /// <summary>
        /// Gets the SelectedSubCategory property.
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
        /// Gets the SubCategory Path property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SubCategoryPath
        {
            get
            {
                return
                    SelectedCategory.CategoryDescription;
            }
        }
        #endregion ViewModel Properties

        #region Event Methods
        private void ShowQuestionsWindow()
        {
            var subCategoryViewModel = this.SelectedSubCategory;
            if (subCategoryViewModel == null) return;
            var questionsWindow = new QuestionsWindow
            {
                DataContext = new QuestionsViewModel(subCategoryViewModel)
            };
            questionsWindow.Show();
        }

        private void ShowAdditionalInfosWindow()
        {
            var subCategoryViewModel = this.SelectedSubCategory;
            if (subCategoryViewModel == null) return;
            var additionalInfosWindow = new AdditionalInfosWindow
            {
                DataContext = new AdditionalInfosViewModel(subCategoryViewModel)
            };
            additionalInfosWindow.Show();
        }

        private void PreviewHtml()
        {
            var previewHtmlWindow = new PreviewHtmlWindow(SelectedSubCategory.SubCategoryNotesHtml);
            previewHtmlWindow.Show();
        }

        private void EditHtml()
        {
            var editHtmlWindow = new EditHtmlWindow(SelectedSubCategory, "SubCategoryNotesHtml");
            editHtmlWindow.Show();
        }

        #endregion Event Methods

    }
}