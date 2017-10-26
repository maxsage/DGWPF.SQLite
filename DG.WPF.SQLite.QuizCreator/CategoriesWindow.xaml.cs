using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DG.WPF.SQLite.QuizCreator
{
    /// <summary>
    /// Interaction logic for CategoriesWindow
    /// </summary>
    public partial class CategoriesWindow : Window
    {
        public CategoriesWindow()
        {
            InitializeComponent();

            Loaded += CategoriesWindow_Loaded;
        }

        void CategoriesWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = desktopWorkingArea.Left;
            this.Top = desktopWorkingArea.Top;
            this.Width = desktopWorkingArea.Width / 2;
            this.Height = desktopWorkingArea.Height;
        }
    }
}
