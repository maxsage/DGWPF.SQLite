/*
  In App:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DG.WPF.SQLite.QuizCreator"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace DG.WPF.SQLite.QuizCreator.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<AdditionalInfosViewModel>();
            SimpleIoc.Default.Register<AdditionalInfoViewModel>();
            SimpleIoc.Default.Register<AnswersViewModel>();
            SimpleIoc.Default.Register<AnswerViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
            SimpleIoc.Default.Register<CategoryViewModel>();
            SimpleIoc.Default.Register<ParameterViewModel>();
            SimpleIoc.Default.Register<SubCategoriesViewModel>();
            SimpleIoc.Default.Register<SubCategoryViewModel>();
            SimpleIoc.Default.Register<WebPageEditorViewModel>();
            SimpleIoc.Default.Register<QuestionTypesViewModel>();
            SimpleIoc.Default.Register<QuestionsViewModel>();

            
        }



        public AdditionalInfosViewModel AdditionalInfosViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdditionalInfosViewModel>();
            }
        }

        public AdditionalInfoViewModel AdditionalInfoViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AdditionalInfoViewModel>();
            }
        }

        public AnswersViewModel AnswersViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnswersViewModel>();
            }
        }

        public AnswerViewModel AnswerViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AnswerViewModel>();
            }
        }
        
        public CategoriesViewModel CategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesViewModel>();
            }
        }

        public CategoryViewModel CategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoryViewModel>();
            }
        }
        

        public ParameterViewModel ParameterViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParameterViewModel>();
            }
        }

        public SubCategoriesViewModel SubCategoriesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubCategoriesViewModel>();
            }
        }

        public SubCategoryViewModel SubCategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubCategoryViewModel>();
            }
        }

        public QuestionTypesViewModel QuestionTypesViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionTypesViewModel>();
            }
        }

        public QuestionsViewModel QuestionsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuestionsViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

        //public static void ClearSubCategoriesViewModel()
        //{
        //    if (SubCategoriesViewModel == null)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        SubCategoriesViewModel.Cleanup();
        //        SubCategoriesViewModel = null;
        //    }
        //}
    }
}