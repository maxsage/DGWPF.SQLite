using System.Windows;

namespace DG.WPF.SQLite.Quiz.Views
{
    public partial class ConfigureQuiz : Window
    {
        public ConfigureQuiz()
        {
            InitializeComponent();

            Loaded += ConfigureQuiz_Loaded;
        }

        void ConfigureQuiz_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Left;
            this.Top = desktopWorkingArea.Top;
            this.Width = desktopWorkingArea.Width / 2;
            this.Height = desktopWorkingArea.Height;
        }
    }
}
