using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Shapes;

namespace EnumaElishApp.MVVM.Models.BotonesUI
{
    public class Botones
    {
        MetodosGeneracionColores metodosGeneracionColores = new MetodosGeneracionColores();
        public Border CrearBoton(string texto, string comando, int fontSize  = 16)
        {
            var button = new Button
            {
                Text = texto,
                TextColor = Color.FromRgba("ffffff"),
                FontSize = fontSize,
                FontAttributes = FontAttributes.Bold,
                BackgroundColor = Colors.Transparent,
                Padding = new Thickness(20, 8),

            };

            if (!string.IsNullOrEmpty(comando))
            {
                button.SetBinding(Button.CommandProperty, new Binding(comando));
            }

            return new Border
            {
                StrokeShape = new RoundRectangle { CornerRadius = 15 },
                StrokeThickness = 2,
                Stroke = Color.FromArgb("f5e1ce"),
                Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                Shadow = new Shadow
                {
                    Offset = new Point(0, 3),
                    Radius = 8,
                    Brush = Colors.Black.AsPaint()
                },
                Content = button
            };
        }
    }
}

