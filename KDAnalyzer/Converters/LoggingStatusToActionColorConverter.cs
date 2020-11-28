using KDACore.Enums;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace KDAnalyzer.Converters
{
    [ValueConversion(typeof(LoggingStatus), typeof(string))]
    public class LoggingStatusToActionColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be a Brush");
            if ((LoggingStatus)value == LoggingStatus.Paused)
            {
                
                return "#43a047";
            }
            else if ((LoggingStatus)value == LoggingStatus.Running)
            {
                return "#f57f17";
            }
            else
            {
                return "#43a047";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

