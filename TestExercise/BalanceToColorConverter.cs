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
            int intValue = (int)value;
            if(parameter is ObservableCollection<Banknote> baknotes)
            {
                var maxBalance = baknotes.Sum(x => x.Value * x.maxCount);
                if(intValue < maxBalance / 2.0 && intValue > maxBalance / 3.0)
                {
                    return Colors.Orange;
                }
                else if(intValue < maxBalance / 3.0)
                {
                    return Colors.Red;
                }
            }
            return Colors.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
