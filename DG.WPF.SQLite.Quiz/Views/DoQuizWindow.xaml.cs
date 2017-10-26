using DG.WPF.SQLite.Quiz.ViewModel;
using GalaSoft.MvvmLight.Ioc;
using System.ComponentModel;
using System.Windows;

namespace DG.WPF.SQLite.Quiz.Views
{
    /// <summary>
    /// Interaction logic for DoQuizWindow.xaml
    /// </summary>
    public partial class DoQuizWindow : Window
    {
        public DoQuizWindow()
        {
            InitializeComponent();

            Loaded += (s, e) =>
            {
                if (DataContext is ICloseable)
                {
                    (DataContext as ICloseable).RequestClose += (_, __) => this.Close();
                }
            };
        }
    }

}
