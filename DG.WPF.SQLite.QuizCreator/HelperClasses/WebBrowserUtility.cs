using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace DG.WPF.SQLite.QuizCreator.HelperClasses
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
                           DependencyProperty.RegisterAttached("BindableSource", typeof(string),
                           typeof(WebBrowserUtility), new UIPropertyMetadata(null,
                           BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o,
                                                         DependencyPropertyChangedEventArgs e)
        {    
            string html;

            if ((string)e.NewValue != String.Empty)
            {
                html = (string)e.NewValue;
            }
            else
            {
                html = "<div style=\"background-color: #00ff00;\"><p>No Html to display.</p></div>";
            }
            
            var webBrowser = (WebBrowser)o;
             
            try
            {
                webBrowser.NavigateToString((string)e.NewValue);
            }
            catch (Exception) { }
        }
    }
}