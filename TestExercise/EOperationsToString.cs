using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TestExercise
{
    public class EOperationsToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Operations eOperations = (Operations)value;
            if (eOperations == Operations.Add)
            {
                return "Добавлено";
            }
            else
            {
                return "Удалено";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
