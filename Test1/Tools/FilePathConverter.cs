using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;

namespace Test1.Tools
{
    public class FilePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? path = value as string;
            if (path != null)
            {
                return Path.GetFileName(path);
            }
            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
