using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Description for QuestionWindow.
    /// </summary>
    public partial class AnswersWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the QuestionWindow class.
        /// </summary>
        public AnswersWindow()
        {
            InitializeComponent();

            Loaded += AnswersWindow_Loaded;
        }

        void AnswersWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Left;
            this.Top = desktopWorkingArea.Top;
            this.Width = desktopWorkingArea.Width / 2;
            this.Height = desktopWorkingArea.Height;
        }
        // UPDATE 
        // Intellisense autocomplete was adding the wrong assembly name!!!!
        // xmlns:cmd="clr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.WPF45"
        // should be
        // xmlns:cmd="clr-namespace:GalaSoft.MvvmLight.Command;assembly=GalaSoft.MvvmLight.Extras.WPF45"
        // I added the Extras word and the EventToCommand was found ok
        // http://mvvmlight.codeplex.com/discussions/210716
        // The windowclosing stuff I found here (specifically PassEventsToCommand syntax to cancel window close)
    }
}