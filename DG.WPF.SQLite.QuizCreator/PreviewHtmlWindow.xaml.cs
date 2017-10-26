using DG.WPF.SQLite.QuizCreator.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Description for QuestionsWindow.
    /// </summary>
    public partial class PreviewHtmlWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the QuestionWindow class.
        /// </summary>
        public PreviewHtmlWindow()
        {
            InitializeComponent();
        }

        public PreviewHtmlWindow(string Html)
        {
            InitializeComponent();

            try
            {
                string finalHtml =
                    new ViewModelLocator().ParameterViewModel.PreHtml +
                    Html +
                    new ViewModelLocator().ParameterViewModel.PostHtml;

                wbPreviewHtml.NavigateToString(finalHtml);
            }
            catch (Exception ex)
            {
                MessageBox.Show("A problem occurred: " + ex.Message);
            }
        }
    }
}