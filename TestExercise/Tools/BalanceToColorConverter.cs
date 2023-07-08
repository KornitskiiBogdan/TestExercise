using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace TestExercise
{
    public class BalanceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            EBalanceState eBalanceState = (EBalanceState)value;
            if(eBalanceState == EBalanceState.Low)
            {
                return new SolidColorBrush(Colors.Red);
            }
            else if(eBalanceState == EBalanceState.Medium)
            {
                return new SolidColorBrush(Colors.Orange);
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
