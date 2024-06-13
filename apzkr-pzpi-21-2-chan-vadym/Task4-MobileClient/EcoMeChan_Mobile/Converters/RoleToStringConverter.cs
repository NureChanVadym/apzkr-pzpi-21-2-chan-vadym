using System;
using System.Globalization;
using EcoMeChan_Mobile.Enums;

namespace EcoMeChan_Mobile.Converters
{
    public class RoleToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Role role)
            {
                return role.ToString();
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string roleString)
            {
                return Enum.Parse(typeof(Role), roleString);
            }

            return null;
        }
    }
}