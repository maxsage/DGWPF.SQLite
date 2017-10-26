using System.Windows;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Description for SubCategoryWindow.
    /// </summary>
    public partial class SubCategoriesWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the SubCategoryWindow class.
        /// </summary>
        public SubCategoriesWindow()
        {
            InitializeComponent();

            Loaded += SubCategoriesWindow_Loaded;
        }

        void SubCategoriesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Left;
            this.Top = desktopWorkingArea.Top;
            this.Width = desktopWorkingArea.Width / 2;
            this.Height = desktopWorkingArea.Height;
        }
    }
}