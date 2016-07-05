using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BioInfo.Client.ValueConverters
{
    class CelsiusToFahrenheitConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Celsius to Farenheit
            return ((9.0 / 5.0) * (double)value) + 32;
           
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Farenheit to Celsius
            return (5.0 / 9.0) * ((double)value - 32);
        }

        public static double Convert(double c)
        {
            // Celsius to Farenheit
            return ((9.0 / 5.0) * c) + 32;
        }

        public static double ConvertBack(double f)
        {
            // Celsius to Farenheit
            return (5.0 / 9.0) * (f - 32);
        }
    }
}
