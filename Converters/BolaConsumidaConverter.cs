// Crear este archivo en tu carpeta Converters: BolaConsumidaConverter.cs
using System.Globalization;

namespace EnumaElishApp.Converters
{
    public class BolaConsumidaConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool consumida)
            {
                // Si la bola está consumida, PorcentageCarga = 0f, sino 0.99f
                return consumida ? 0f : 0.99f;
            }
            return 0.99f; // Valor por defecto
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}