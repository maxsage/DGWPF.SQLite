//using Manoli.Utils.CSharpFormat;
using Manoli.Utils.CSharpFormat;
using DG.WPF.SQLite.QuizCreator.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Description for QuestionsWindow.
    /// </summary>
    public partial class EditHtmlWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the QuestionWindow class.
        /// </summary>
        public EditHtmlWindow()
        {
            InitializeComponent();
        }

        public EditHtmlWindow(object viewModel, string htmlToEdit)
        {
            InitializeComponent();

            this.DataContext = viewModel;
            Binding binding = new Binding(htmlToEdit);
            binding.Source = viewModel;
            Html.SetBinding(TextBox.TextProperty, binding);

            PreHtml.Text = new ViewModelLocator().ParameterViewModel.PreHtml;
            PostHtml.Text = new ViewModelLocator().ParameterViewModel.PostHtml;

        }

        private void btnCSToHtml_Click(object sender, RoutedEventArgs e)
        {
            if (Html.SelectedText.Length > 0)
            {
                int selectionStart = Html.SelectionStart;
                int selectionLength = Html.SelectionLength;

                CSharpFormat csf = new CSharpFormat();
                string formattedCode = csf.FormatCode(Html.SelectedText);

                // Clear the selected text from the TextBox
                Html.Text = Html.Text.Remove(selectionStart, selectionLength);

                Html.Text = Html.Text.Insert(
                    selectionStart,
                    formattedCode);

                Html.CaretIndex = (selectionStart + formattedCode.Length);
            }
            else
            {
                System.Windows.MessageBox.Show("No text has been selected.");
            }
        }

        private void btnJSToHtml_Click(object sender, RoutedEventArgs e)
        {
            if (Html.SelectedText.Length > 0)
            {
                int selectionStart = Html.SelectionStart;
                int selectionLength = Html.SelectionLength;

                JavaScriptFormat jsf = new JavaScriptFormat();
                string formattedCode = jsf.FormatCode(Html.SelectedText);

                // Clear the selected text from the TextBox
                Html.Text = Html.Text.Remove(selectionStart, selectionLength);

                Html.Text = Html.Text.Insert(
                    selectionStart,
                    formattedCode);

                Html.CaretIndex = (selectionStart + formattedCode.Length);
            }
            else
            {
                System.Windows.MessageBox.Show("No text has been selected.");
            }
        }

        private void btnHtmlToHtml_Click(object sender, RoutedEventArgs e)
        {
            if (Html.SelectedText.Length > 0)
            {
                int selectionStart = Html.SelectionStart;
                int selectionLength = Html.SelectionLength;

                HtmlFormat htmlFormat = new HtmlFormat();
                string formattedCode = htmlFormat.FormatCode(Html.SelectedText);

                // Clear the selected text from the TextBox
                Html.Text = Html.Text.Remove(selectionStart, selectionLength);

                Html.Text = Html.Text.Insert(
                    selectionStart,
                    formattedCode);

                Html.CaretIndex = (selectionStart + formattedCode.Length);
            }
            else
            {
                System.Windows.MessageBox.Show("No text has been selected.");
            }
        }

        private void btnKeywordToHtml_Click(object sender, RoutedEventArgs e)
        {
            if (Html.SelectedText.Length > 0)
            {
                var selectionStart = Html.SelectionStart;
                var selectionLength = Html.SelectionLength;

                var kwf = new KeywordFormat();
                var formattedCode = kwf.FormatKeywordToHtml(Html.SelectedText);

                // Clear the selected text from the TextBox
                Html.Text = Html.Text.Remove(selectionStart, selectionLength);

                Html.Text = Html.Text.Insert(
                    selectionStart,
                    formattedCode);

                Html.CaretIndex = (selectionStart + formattedCode.Length);
            }
            else
            {
                System.Windows.MessageBox.Show("No text has been selected.");
            }
        }
    }
}