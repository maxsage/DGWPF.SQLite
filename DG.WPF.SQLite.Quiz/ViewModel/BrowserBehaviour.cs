using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DG.WPF.SQLite.Quiz.ViewModel
{
    public class BrowserBehavior
    {
        public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
            "Html",
            typeof (string),
            typeof (BrowserBehavior),
            new FrameworkPropertyMetadata(OnHtmlChanged));

        [AttachedPropertyBrowsableForType(typeof (WebBrowser))]
        public static string GetHtml(WebBrowser d)
        {
            return (string) d.GetValue(HtmlProperty);
        }

        public static void SetHtml(WebBrowser d, string value)
        {
            d.SetValue(HtmlProperty, value);
        }

        private static void OnHtmlChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var webBrowser = dependencyObject as WebBrowser;
            if (webBrowser != null)
                webBrowser.NavigateToString(e.NewValue as string ?? "&nbsp;");
        }
    }




    public static class WebBrowserUtility
     {
            public static readonly DependencyProperty BindableSourceProperty =
                DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

            public static string GetBindableSource(DependencyObject obj)
            {
                return (string)obj.GetValue(BindableSourceProperty);
            }

            public static void SetBindableSource(DependencyObject obj, string value)
            {
                obj.SetValue(BindableSourceProperty, value);
            }

            public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
            {
                var browser = o as WebBrowser;
                if (browser == null) return;
                var uri = e.NewValue as string;


                var dropBoxFolder = Environment.GetEnvironmentVariable("userprofile")
                                    + "\\DropBox";

                dropBoxFolder = dropBoxFolder.Replace("\\", "/");

                uri = uri
                    .Contains("[DropBox]")
                    ? uri.
                        Replace("[DropBox]", dropBoxFolder)
                    : uri;



                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;
            }

    }
}
