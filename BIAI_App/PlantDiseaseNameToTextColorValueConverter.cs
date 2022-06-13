using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;
using Color = System.Drawing.Color;

namespace BIAI_App
{
    [ValueConversion(typeof(string), typeof(SolidColorBrush))]
    internal class PlantDiseaseNameToTextColorValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null) return Brushes.Black;
            string plantDisease = (string) value;

            if (plantDisease.Contains("Healthy"))
            {
                return Brushes.Green;
            }

            if (plantDisease.Contains("Invalid") || plantDisease == "")
            {
                return Brushes.DimGray;
            }
            
            return Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
