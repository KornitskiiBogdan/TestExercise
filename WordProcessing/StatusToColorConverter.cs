using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace WordProcessing
{
    public class StatusToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EStatus eStatus = (EStatus)value;
            if(eStatus == EStatus.Failed)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if(eStatus == EStatus.InProgress)
            {
                return new SolidColorBrush(Colors.LightBlue);
            }
            else
            {
                return new SolidColorBrush(Colors.Green);
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
