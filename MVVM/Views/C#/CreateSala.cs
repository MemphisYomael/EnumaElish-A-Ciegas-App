using CommunityToolkit.Maui.Markup;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.Models.BotonesUI;
using EnumaElishApp.MVVM.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.PlatformConfiguration.TizenSpecific;
using Microsoft.Maui.Controls.Shapes;
using Image = Microsoft.Maui.Controls.Image;

namespace EnumaElishApp.MVVM.Views.C_;

public class CreateSala : ContentPage
{
    private readonly Botones botones = new Botones();
    private readonly MetodosGeneracionColores metodosGeneracionColores = new MetodosGeneracionColores();
    public CreateSala()
	{
        Shell.SetNavBarIsVisible(this, false);
		this.On<iOS>().SetUseSafeArea(false);

        BindingContext = new CreateSalaViewModel();

        Content = new Grid
		{
			ColumnDefinitions = new ColumnDefinitionCollection
			{
				new ColumnDefinition(),
            },
			RowDefinitions = new RowDefinitionCollection
			{
				new RowDefinition(),
            },

            Children = {

                   new Microsoft.Maui.Controls.Image
                        {
                            Source = "fondo_enuma_elish.jpeg",
                            Aspect = Aspect.AspectFill,

                        }
                        .Fill()
                        .Row(0)
                        .Column(0)
                        .ZIndex(-2),

                         new BoxView
                        {
                            BackgroundColor = Color.FromRgba(0, 0, 0, 0.5), // Black with 30% opacity (adjust as needed)
                            InputTransparent = true // Allows touch events to pass through to the image
                        }
                        .Fill()
                        .Row(0)
                        .Column(0)
                        .ZIndex(-1),

                  new Grid
        {
            Padding = 20,
            ColumnSpacing = 20,
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
                new ColumnDefinition(),
            },
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
                new RowDefinition(),
            },

            Children = {
#if WINDOWS
                          botones.CrearBoton("Cancelar", "popModal", 30)
                        .Column(2)
                        .ColumnSpan(2)
                        .Row(3),
#endif
                        
                        botones.CrearBoton("Crear Sala", "", 30)
                        .Row(3)
                        .Column(0)
                        .ColumnSpan(2),

                        new Microsoft.Maui.Controls.Entry
                        {
                            Placeholder = "Nombre de la sala",
                            TextColor = Color.FromRgba("ffffff"),
                            BackgroundColor = Colors.Transparent,
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Center
                        }
                        .Font("text")
                        .Column(0)
                        .Row(0)
                        .ColumnSpan(2),

                        new Microsoft.Maui.Controls.Entry
                        {
                            TextColor = Color.FromRgba("ffffff"),
                            BackgroundColor = Colors.Transparent,
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Center,
                            IsPassword = true
                            
                        }.Placeholder("Contraseña (opcional)")
                        .Font("text")
                        .Column(0)
                        .Row(1)
                        .ColumnSpan(2),

                        new Microsoft.Maui.Controls.Entry
                        {
                            TextColor = Color.FromRgba("ffffff"),
                            BackgroundColor = Colors.Transparent,
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Center,
                            Keyboard = Keyboard.Numeric,
                            MaxLength = 2 
                        }.Placeholder("Numero de Jugadores (max 10, min 5)")
                        .Font("text")
                        .Column(0)
                        .Row(2)
                        .ColumnSpan(2),


                        new Border
                        {
                            Shadow = new Shadow
                            {
                                Offset = new Point(0, 3),
                                Radius = 8,
                                Brush = Colors.Black.AsPaint()
                            },
                            StrokeShape = new RoundRectangle { CornerRadius = 15 },
                            Stroke = Color.FromArgb("f5e1ce"),
                            Content = new Grid
                            {
                                Children =
                                {
                                    new Microsoft.Maui.Controls.Image
                                    {
                                        Aspect = Aspect.AspectFill,
                                        HorizontalOptions = LayoutOptions.Fill,
                                        VerticalOptions = LayoutOptions.Fill,

                                    }.Bind(Image.SourceProperty, "foto")
                                }
                            }
                            
                        }.Column(2)
                        .ColumnSpan(2)
#if !WINDOWS
                        .RowSpan(4)
#else
                        .RowSpan(3)
#endif


                      }
              }
            
            }
        };
    }
}