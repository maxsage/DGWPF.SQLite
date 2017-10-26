using System;
using System.Windows;
using System.Windows.Controls;
using DG.WPF.SQLite.Model;

namespace DG.WPF.SQLite.Quiz.Views
{
    /// <summary>
    /// Interaction logic for AdditionalInfo
    /// </summary>
    public partial class AdditionalInfoWindow : Window
    {
        UserControl uc;

        public AdditionalInfoWindow(AdditionalInfo additionalInfo, Category currentCategory)
        {
            
            InitializeComponent();
            try
            {
                //Uri uri = new Uri(additionalInfo.SubCategory.SubCategoryAdditionalInfoBaseUri + additionalInfo.AdditionalInfoUri);
                Uri uri = new Uri(currentCategory.AdditionalInfoUri + additionalInfo.AdditionalInfoUri);
                wbAdditionalInfo.Navigate(uri);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
        //    "Html",
        //    typeof(string),
        //    typeof(AdditionalInfoWindow),
        //    new FrameworkPropertyMetadata(OnHtmlChanged));

        //[AttachedPropertyBrowsableForType(typeof(WebBrowser))]
        //public static string GetHtml(WebBrowser d)
        //{
        //    return (string)d.GetValue(HtmlProperty);
        //}

        //public static void SetHtml(WebBrowser d, string value)
        //{
        //    d.SetValue(HtmlProperty, value);
        //}

        //static void OnHtmlChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        //{
        //    WebBrowser wb = d as WebBrowser;
        //    if (wb != null)
        //        wb.NavigateToString(e.NewValue as string);
        //}
    }
}
