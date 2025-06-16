using CommunityToolkit.Maui.Markup;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.ViewModels;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Microsoft.Maui.Controls.Shapes;

namespace EnumaElishApp.MVVM.Views.C_;

public class EntrarASala : ContentPage
{
    private MVVM.Models.BotonesUI.Botones botones = new MVVM.Models.BotonesUI.Botones();
    private MetodosGeneracionColores metodosGeneracionColores = new MetodosGeneracionColores();
    Microsoft.Maui.Controls.Entry entry;
    public EntrarASala()
    {
        Shell.SetNavBarIsVisible(this, false);
        this.On<iOS>().SetUseSafeArea(false);

        entry = new Microsoft.Maui.Controls.Entry
        {
            TextColor = Color.FromRgba("ffffff"),
            BackgroundColor = Colors.Transparent,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            HorizontalOptions = LayoutOptions.Fill,
            VerticalOptions = LayoutOptions.Center,
            IsPassword = true,
        };
        entry.Placeholder = "Contraseña (Si la tiene)";
        entry.FontFamily = "text";
        
        BindingContext = new EmparejarSalaViewModel();
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
            Children =
            {
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
                    BackgroundColor = Color.FromRgba(0, 0, 0, 0.5),
                    InputTransparent = true
                }
                .Fill()
                .Row(0)
                .Column(0)
                .ZIndex(-1),

                new Grid
                {
                    Padding = 20,
                    ColumnSpacing = 20,
                    RowSpacing = 20,
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
                    Children =
                    {
#if WINDOWS
                        botones.CrearBoton("Cancelar", "popModal", 30)
                        .Column(2)
                        .ColumnSpan(2)
                        .Row(3),
#endif
                        new Border
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
                            Content = new Button
                            {
                                Text = "Entrar a la sala",
                                TextColor = Color.FromRgba("ffffff"),
                                FontSize = 30,
                                FontAttributes = FontAttributes.Bold,
                                BackgroundColor = Colors.Transparent,
                                Padding = new Thickness(20, 8),
                            }
                            .Bind(Button.IsEnabledProperty, "isEnabled")
                        }
                        .Row(2)
                        .Column(0)
                        .ColumnSpan(2),

                        botones
                        .CrearBoton("Emparejar Aleatoriamente", "", 20)
                        .Row(3)
                        .Column(0)
                        .ColumnSpan(2),

                        new Microsoft.Maui.Controls.Entry
                        {
                            Placeholder = "ID de la sala",
                            TextColor = Color.FromRgba("ffffff"),
                            BackgroundColor = Colors.Transparent,
                            FontSize = 16,
                            FontAttributes = FontAttributes.Bold,
                            HorizontalOptions = LayoutOptions.Fill,
                            VerticalOptions = LayoutOptions.Center,
                        }
                        .Bind(Microsoft.Maui.Controls.Entry.TextProperty, "nombreDeSala")
                        .Font("text")
                        .Column(0)
                        .Row(0)
                        .ColumnSpan(2),

                        entry       
                        .Column(0)
                        .Row(1)
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
                        }
                        .Column(2)
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