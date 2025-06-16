using System.Windows.Input;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;

namespace EnumaElishApp.MVVM.Models.Canvas
{
    public class ControlPersonalizadoSkia : SKCanvasView, IDisposable
    {
        private readonly SKPaint _paint = new SKPaint();
        private SKPicture _svgPicture;

        #region colorFondo
        public static readonly BindableProperty ColorFondoProperty =
          BindableProperty.Create(nameof(ColorFondo), typeof(Color), typeof(ControlPersonalizadoSkia),
            defaultValue: Colors.Blue, propertyChanged: OnColorChanged);

        public Color ColorFondo
        {
            get => (Color)GetValue(ColorFondoProperty);
            set => SetValue(ColorFondoProperty, value);
        }

        private static void OnColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ControlPersonalizadoSkia control)
            {
                control.InvalidateSurface();
            }
        }
        #endregion

        #region size
        public static readonly BindableProperty CircleSizeProperty =
          BindableProperty.Create(nameof(CircleSize), typeof(int), typeof(ControlPersonalizadoSkia),
            defaultValue: 50, propertyChanged: OnSizeChanged);

        public int CircleSize
        {
            get => (int)GetValue(CircleSizeProperty);
            set => SetValue(CircleSizeProperty, value);
        }

        private static void OnSizeChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ControlPersonalizadoSkia control)
            {
                control.InvalidateSurface();
            }
        }
        #endregion

        #region comandoAccionPressed
        public static readonly BindableProperty comandoAccionPressedProperty =
          BindableProperty.Create(nameof(ComandoAccionPressed), typeof(ICommand), typeof(ControlPersonalizadoSkia),
            default(ICommand) /*, propertyChanged: OnComandoAccionPressedChanged*/ );

        //private static void OnComandoAccionPressedChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    if (bindable is ControlPersonalizadoSkia control && newValue is ICommand command)
        //    {
        //        control.InvalidateSurface();
        //    }
        //}

        public ICommand ComandoAccionPressed
        {
            get => (ICommand)GetValue(comandoAccionPressedProperty);
            set => SetValue(comandoAccionPressedProperty, value);
        }

        #endregion

        #region parametro del comando
        public static readonly BindableProperty CommandParameterProperty =
          BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ControlPersonalizadoSkia), null);

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        #endregion

        #region colorBorder
        public static readonly BindableProperty ColorBorderProperty = 
            BindableProperty.Create(nameof(ColorBorder), typeof(Color), typeof(ControlPersonalizadoSkia),defaultValue: Colors.White ,propertyChanged: OnColorBorderChanged);

        public Color ColorBorder 
        {
            get => (Color)GetValue(ColorBorderProperty);
            set => SetValue(ColorBorderProperty, value);
        }
        private static void OnColorBorderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ControlPersonalizadoSkia control)
            {
                control.InvalidateSurface();
            }
        }
        #endregion

        #region PorcentageCarga
        public static readonly BindableProperty PorcentageCargaProperty =
            BindableProperty.Create(nameof(PorcentageCarga), typeof(float), typeof(ControlPersonalizadoSkia), 0f, propertyChanged: OnPorcentageCargaChanged);

        private static void OnPorcentageCargaChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is ControlPersonalizadoSkia control)
            {
                control.InvalidateSurface();
            }
        }

        public float PorcentageCarga
        {
            get => (float)GetValue(PorcentageCargaProperty);
            set => SetValue(PorcentageCargaProperty, value);
        }
        #endregion

 
        public ControlPersonalizadoSkia()
        {
            EnableTouchEvents = true;
        }

        protected override void OnPaintSurface(SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            var info = e.Info;

            canvas.Clear(SKColors.Transparent);

            float centerX = info.Width / 2f;
            float centerY = info.Height / 2f;
            float radioMaximo = Math.Min(info.Width, info.Height) / 2f - 10;
            float radio = Math.Min(CircleSize, radioMaximo);

            // Dibujar círculo relleno con transparencia basada en PorcentageCarga
            _paint.Style = SKPaintStyle.Fill;

            // Obtener el color base y aplicar transparencia
            var colorBase = ColorFondo.ToSKColor();

            // Calcular alpha basado en PorcentageCarga (0-1)
            byte alpha = (byte)(PorcentageCarga * 255);

            // Crear color con transparencia
            var colorConTransparencia = new SKColor(colorBase.Red, colorBase.Green, colorBase.Blue, alpha);

            _paint.Color = colorConTransparencia;
            _paint.IsAntialias = true;
            canvas.DrawCircle(centerX, centerY, radio, _paint);

            // Dibujar borde del círculo
            _paint.Style = SKPaintStyle.Stroke;

            // Cambiar color del borde a blanco si PorcentageCarga es 1, sino usar ColorBorder
            _paint.Color = (float)PorcentageCarga >= 1.0f ? SKColors.White : ColorBorder.ToSKColor();
            _paint.StrokeWidth = 7f;
            canvas.DrawCircle(centerX, centerY, radio, _paint);
        }
        protected override void OnTouch(SKTouchEventArgs e)
        {

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (ComandoAccionPressed != null && ComandoAccionPressed.CanExecute(null))
                    {
                        ComandoAccionPressed.Execute(CommandParameter);
                        InvalidateSurface();

                    }
                    break;

                //case SKTouchAction.Moved:
                    //if (ComandoAccionPressed != null && ComandoAccionPressed.CanExecute(null))
                    //{
                    //ComandoAccionPressed.Execute(CommandParameter);
                    //}
                    //Console.WriteLine("===================================================");

                    //break;

                //case SKTouchAction.Released:
                    //if (ComandoAccionPressed != null && ComandoAccionPressed.CanExecute(null))
                    //{
                    //ComandoAccionPressed.Execute(CommandParameter);
                    //}
                    //break;
            }

            e.Handled = true;
            InvalidateSurface();
        }

        public void Dispose()
        {
            _svgPicture?.Dispose();
            _paint?.Dispose();
        }

    }
}