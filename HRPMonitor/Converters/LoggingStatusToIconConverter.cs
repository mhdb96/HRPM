using HRPMCore.Enums;
using MaterialDesignThemes.Wpf;
using System;
using System.Windows;
using System.Windows.Data;

namespace HRPMonitor.Converters
{
    [ValueConversion(typeof(LoggingStatus), typeof(string))]
    public class LoggingStatusToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(PackIconKind))
                throw new InvalidOperationException("The target must be a PackIconKind");
            if ((LoggingStatus)value == LoggingStatus.Paused)
            {
                return PackIconKind.Pause;
            }
            else if((LoggingStatus)value == LoggingStatus.Running)
            {
                return PackIconKind.Tick;
            }
            else
            {
                return PackIconKind.Stop;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}

