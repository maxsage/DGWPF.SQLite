using System.Windows;
using DG.WPF.SQLite.DataService;
using DG.WPF.SQLite.Model;

namespace DG.WPF.SQLite.Quiz.Views
{
    /// <summary>
    /// Interaction logic for ViewDescriptionHtmlWindow
    /// </summary>
    public partial class ViewDescriptionHtmlWindow : Window
    {
        //private DGContext context = new DGContext();
        string SqLiteDatabasePath = "";

        public ViewDescriptionHtmlWindow(string sqliteDatabasePath, int questionId)
        {
            InitializeComponent();

            SqLiteDatabasePath = sqliteDatabasePath;

            Question question =
              QuestionDataService.Current.GetQuestionById(SqLiteDatabasePath, questionId );

            try
            {
                // Create the Html wrapper
                string basicHtmlForm = ParameterDataService.Current.GetParameterById(SqLiteDatabasePath, 4).CharValue
                    + question.DescriptionHtml +
                    ParameterDataService.Current.GetParameterById(SqLiteDatabasePath, 5).CharValue;

                wbDescriptionHtml.NavigateToString(basicHtmlForm);
            }
            catch
            {
                // Dont display errors
            }
        }
    }
}
