using DG.WPF.SQLite.QuizCreator.ViewModel;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Description for AdditionalInfoWindow.
    /// </summary>
    public partial class AdditionalInfosWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the AdditionalInfoWindow class.
        /// </summary>
        public AdditionalInfosWindow()
        {
            InitializeComponent();

            Loaded += AdditionalInfosWindow_Loaded;
        }

        void AdditionalInfosWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Left;
            this.Top = desktopWorkingArea.Top;
            this.Width = desktopWorkingArea.Width / 2;
            this.Height = desktopWorkingArea.Height;
        }
    }
}