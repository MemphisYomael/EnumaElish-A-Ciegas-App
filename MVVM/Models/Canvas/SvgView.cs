using SkiaSharp;
using SkiaSharp.Extended.Svg;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using SkiaSharp.Views.Maui.Controls.Hosting;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace EnumaElishApp.MVVM.Models.Canvas
{
    public class SvgView : SKCanvasView
    {
        private SKSvg? _svg;
        private string? _svgFileName;

        public static readonly BindableProperty SvgNameProperty = BindableProperty.Create(
            nameof(SvgName),
            typeof(string),
            typeof(SvgView),
            default(string),
            propertyChanged: OnSvgNameChanged);

        public string SvgName
        {
            get => (string)GetValue(SvgNameProperty);
            set => SetValue(SvgNameProperty, value);
        }

        public SvgView()
        {
            this.PaintSurface += OnPaintSurface;
        }

        private static void OnSvgNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SvgView svgView && newValue is string newFileName)
            {
                svgView._svgFileName = newFileName;
                svgView.LoadSvg();
                svgView.InvalidateSurface(); // Vuelve a dibujar
            }
        }

        private void LoadSvg()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_svgFileName))
                    return;

                var path = Path.Combine(AppContext.BaseDirectory,"Resources", "Images", _svgFileName);
                using var stream = File.OpenRead(path);
                _svg = new SKSvg();
                _svg.Load(stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error cargando SVG '{_svgFileName}': {ex.Message}");
            }
        }

        private void OnPaintSurface(object? sender, SKPaintSurfaceEventArgs e)
        {
            var canvas = e.Surface.Canvas;
            canvas.Clear(SKColors.Transparent);

            if (_svg?.Picture != null)
            {
                var info = e.Info;
                var canvasRect = new SKRect(0, 0, info.Width, info.Height);
                var svgSize = _svg.Picture.CullRect;

                float scale = Math.Min(info.Width / svgSize.Width, info.Height / svgSize.Height);
                canvas.Scale(scale);

                canvas.DrawPicture(_svg.Picture);
            }
        }
    }
}
