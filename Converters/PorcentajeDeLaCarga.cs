using System;
using System.Globalization;

namespace EnumaElishApp.Converters
{
    public class PorcentajeDeLaCarga : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is double carga)
            {
                // Asegurar que el valor esté entre 0 y 1
                var porcentaje = Math.Max(0, Math.Min(1, carga / 100.0));
                System.Diagnostics.Debug.WriteLine($"Convertiendo carga: {carga} -> {porcentaje}");
                return (float)porcentaje;
            }

            if (value is int cargaInt)
            {
                var porcentaje = Math.Max(0, Math.Min(1, cargaInt / 100.0));
                System.Diagnostics.Debug.WriteLine($"Convertiendo carga int: {cargaInt} -> {porcentaje}");
                return (float)porcentaje;
            }

            System.Diagnostics.Debug.WriteLine($"Valor no válido para conversión: {value?.GetType().Name ?? "null"}");
            return 0f;
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is float porcentaje)
            {
                return porcentaje * 100.0;
            }
            return 0.0;
        }
    }
}