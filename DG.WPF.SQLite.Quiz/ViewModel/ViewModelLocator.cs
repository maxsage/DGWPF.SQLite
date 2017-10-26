/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:DG.WPF"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using DG.WPF.SQLite.Model;

namespace DG.WPF.SQLite.Quiz.ViewModel
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

            SimpleIoc.Default.Register<AboutViewModel>();
            SimpleIoc.Default.Register<OptionsViewModel>();
            SimpleIoc.Default.Register<ParameterViewModel>();
            SimpleIoc.Default.Register<ConfigureQuizViewModel>();
            SimpleIoc.Default.Register<QuizSessionViewModel>();
            SimpleIoc.Default.Register<SubCategoryViewModel>();
        }

        /// <summary>
        /// Gets the AboutWindow property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public AboutViewModel AboutViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AboutViewModel>();
            }
        }

        /// <summary>
        /// Gets the StartWindow property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public OptionsViewModel OptionsViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<OptionsViewModel>();
            }
        }

        /// <summary>
        /// Gets the Parameter property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ParameterViewModel ParameterViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ParameterViewModel>();
            }
        }

        /// <summary>
        /// Gets the StartWindow property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public ConfigureQuizViewModel ConfigureQuizViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ConfigureQuizViewModel>();
            }
        }

        /// <summary>
        /// Gets the StartWindow property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public QuizSessionViewModel QuizSessionViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<QuizSessionViewModel>();
            }
        }

        /// <summary>
        /// Gets the StartWindow property.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance",
            "CA1822:MarkMembersAsStatic",
            Justification = "This non-static member is needed for data binding purposes.")]
        public SubCategoryViewModel SubCategoryViewModel
        {
            get
            {
                return ServiceLocator.Current.GetInstance<SubCategoryViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }

    }
}