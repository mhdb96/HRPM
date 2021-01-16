using HRPMCore.Enums;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace HRPMonitor.Converters
{
    [ValueConversion(typeof(LoggingStatus), typeof(string))]
    public class LoggingStatusToActionIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(PackIconKind))
                throw new InvalidOperationException("The target must be a PackIconKind");
            if ((LoggingStatus)value == LoggingStatus.Paused)
            {
                return PackIconKind.Play;
            }
            else if ((LoggingStatus)value == LoggingStatus.Running)
            {
                return PackIconKind.Pause;
            }
            else
            {
                return PackIconKind.Play;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

