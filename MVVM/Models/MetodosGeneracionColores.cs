using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumaElishApp.MVVM.Models
{
    public class MetodosGeneracionColores
    {
        public LinearGradientBrush crearDegradado(string color1, string color2)
        {
            LinearGradientBrush linearGradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
            {
                new GradientStop(Color.FromArgb(color1), 0.0f),
                new GradientStop(Color.FromArgb(color2), 1.0f),
    }
            };
            return linearGradient;
        }

        public LinearGradientBrush crearDegradado3Colores(string color1, string color2, string color3)
        {
            LinearGradientBrush linearGradient = new LinearGradientBrush
            {
                StartPoint = new Point(1, 0),
                EndPoint = new Point(0, 1),
                GradientStops = new GradientStopCollection
            {
                new GradientStop(Color.FromArgb(color1), 0.0f),
                new GradientStop(Color.FromArgb(color2), 0.5f),
                new GradientStop(Color.FromArgb(color3), 1.0f),
    }
            };
            return linearGradient;
        }

    }
}
