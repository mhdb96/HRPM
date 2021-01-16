
using HRPMCore.Enums;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace HRPMonitor.Converters
{
    [ValueConversion(typeof(LoggingStatus), typeof(string))]
    public class LoggingStatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException("The target must be a Brush");
            if ((LoggingStatus)value == LoggingStatus.Paused)
            {
                return "#f57f17";
            }
            else if ((LoggingStatus)value == LoggingStatus.Running)
            {
                return "#43a047";
            }
            else
            {
                return "#f4511e";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

