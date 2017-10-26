using System;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using DG.WPF.SQLite.Model;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;

namespace DG.WPF.SQLite.Quiz.HelperClasses
{
    [ValueConversion(typeof(int), typeof(Color))]
    public class AgeToColorConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new AgeToColorConverter();
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Color.FromArgb(255, 106, 90, 205);

            if (value == null) return color;
            int age;
            Int32.TryParse(value.ToString(), out age);

            if (age > 60)
            {
                color = Color.FromArgb(255, 255, 0, 0);
            }
            if (age <= 60)
            {
                color = Color.FromArgb(255, 255, 255, 0);
            }
            if (age <= 30)
            {
                color = Color.FromArgb(255, 131, 255, 151);
            }

            return color;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    [ValueConversion(typeof(int), typeof(Visibility))]
    public class QuestionTypeToVisibilityConverter : IValueConverter
    {
        public static readonly IValueConverter Instance = new QuestionTypeToVisibilityConverter();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int questionTypeId;
            Int32.TryParse(value.ToString(), out questionTypeId);

            int controlTypeId;
            Int32.TryParse(parameter.ToString(), out controlTypeId);

            //1 Single Choice/Single Answer
            //2 Multiple Choice/Single Answer
            //4 Multiple Choice/Multiple Answer

            return questionTypeId == controlTypeId ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }



}
