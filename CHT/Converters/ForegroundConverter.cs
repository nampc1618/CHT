using CHT.Commons;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace CHT.Converters
{
    public class ComStateToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((SysStates.EComState)value)
                {
                    case SysStates.EComState.OPENED:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#33b910");
                    case SysStates.EComState.CLOSED:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#adb0ad");
                    case SysStates.EComState.RECEIVING_DATA:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#0941e6");
                    default:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#000000");
                }
            }
            else
            {
                return (SolidColorBrush)new BrushConverter().ConvertFromString("#000000");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class PrinterStateToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((SysStates.EPrinterState)value)
                {
                    case SysStates.EPrinterState.CONNECTED:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#33b910");
                    case SysStates.EPrinterState.DISCONECTED:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#adb0ad");
                    default:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#000000");
                }
            }
            else
            {
                return (SolidColorBrush)new BrushConverter().ConvertFromString("#000000");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class MessageStateToForegroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                switch ((SysStates.EMessageState)value)
                {
                    case SysStates.EMessageState.UPDATE_FIELD_SUCCESS:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#33b910");
                    default:
                        return (SolidColorBrush)new BrushConverter().ConvertFromString("#adb0ad");
                }
            }
            else
            {
                return (SolidColorBrush)new BrushConverter().ConvertFromString("#adb0ad");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
