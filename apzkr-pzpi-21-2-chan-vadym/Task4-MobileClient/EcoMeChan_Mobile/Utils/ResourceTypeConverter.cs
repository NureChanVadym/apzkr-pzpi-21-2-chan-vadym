using System;
using System.Globalization;
using EcoMeChan_Mobile.Enums;
using EcoMeChan_Mobile.Resources.Languages;

namespace EcoMeChan_Mobile.Utils
{
    public class ResourceTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ResourceType resourceType)
            {
                switch (resourceType)
                {
                    case ResourceType.Water:
                        return AppResources.ResourceTypeWater;
                    case ResourceType.Gas:
                        return AppResources.ResourceTypeGas;
                    case ResourceType.Electricity:
                        return AppResources.ResourceTypeElectricity;
                    default:
                        return string.Empty;
                }
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}