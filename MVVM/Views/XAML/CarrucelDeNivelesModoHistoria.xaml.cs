using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Markup;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.ViewModels;
using Microsoft.Maui.Controls.Shapes;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace EnumaElishApp.MVVM.Views.XAML;

public partial class CarrucelDeNivelesModoHistoria : ContentPage
{
    private CarrucelNivelesHistoriaViewModel _viewModel;

   
    public CarrucelDeNivelesModoHistoria()
    {
        _viewModel = new CarrucelNivelesHistoriaViewModel();
        BindingContext = _viewModel;

        NavigationPage.SetHasNavigationBar(this, false);

        Content = CrearInterfaz();
    }

    private View CrearInterfaz()
    {
        return new Grid
        {
            BackgroundColor = Color.FromArgb("#0f0f23"),
            RowDefinitions = Rows.Define(
                (RowPrincipal.Header, 100),
                (RowPrincipal.Content, Star)
            ),
            Children =
            {
                // Header con gradiente y título
                CrearHeader().Row(RowPrincipal.Header),
                
                // Contenido principal con scroll
                CrearContenido().Row(RowPrincipal.Content)
            }
        };
    }

    private View CrearHeader()
    {
        return new Border
        {
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = Color.FromArgb("#1e1b4b"), Offset = 0.0f },
                    new GradientStop { Color = Color.FromArgb("#312e81"), Offset = 0.5f },
                    new GradientStop { Color = Color.FromArgb("#1e1b4b"), Offset = 1.0f }
                }
            },
            Shadow = new Shadow
            {
                Offset = new Point(0, 4),
                Radius = 15,
                Opacity = 0.4f,
                Brush = Colors.Black.AsPaint()
            },
            Content = new Grid
            {
                Padding = new Thickness(20, 10),
                RowDefinitions = Rows.Define(Auto, Star),
                Children =
                {
                    new Label()
                        .Text("ENUMA ELISH")
                        .FontSize(32)
                        .Bold()
                        .TextColor(Colors.White)
                        .CenterHorizontal()
                        .Row(0),

                    new Label()
                        .Text("Modo Historia • Selecciona tu aventura")
                        .FontSize(16)
                        .TextColor(Color.FromArgb("#a5b4fc"))
                        .CenterHorizontal()
                        .Row(1)
                }
            }
        };
    }

    private View CrearContenido()
    {
        return new ScrollView
        {
            Orientation = ScrollOrientation.Vertical,
            Content = new StackLayout
            {
                Spacing = 30,
                Padding = new Thickness(20, 20, 20, 40),
                Children = { CrearCapitulos() }
            }
        };
    }

    private View CrearCapitulos()
    {
        var stackLayout = new StackLayout { Spacing = 40 };

        foreach (var capitulo in _viewModel.Capitulos)
        {
            stackLayout.Children.Add(CrearCapitulo(capitulo));
        }

        return stackLayout;
    }

    private View CrearCapitulo(CapituloModel capitulo)
    {
        return new StackLayout
        {
            Spacing = 15,
            Children =
            {
                // Header del capítulo
                CrearHeaderCapitulo(capitulo),
                
                // Niveles del capítulo
                CrearNivelesCapitulo(capitulo)
            }
        };
    }

    private View CrearHeaderCapitulo(CapituloModel capitulo)
    {
        return new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 15 },
            Background = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 0),
                GradientStops = new GradientStopCollection
                {
                    new GradientStop { Color = capitulo.ColorTema, Offset = 0.0f },
                    new GradientStop { Color = capitulo.ColorSecundario, Offset = 1.0f }
                }
            },
            Shadow = new Shadow
            {
                Offset = new Point(0, 6),
                Radius = 20,
                Opacity = 0.3f,
                Brush = Colors.Black.AsPaint()
            },
            Padding = new Thickness(25, 20),
            Content = new Grid
            {
                ColumnDefinitions = Columns.Define(Star, Auto),
                Children =
                {
                    new StackLayout
                    {
                        Spacing = 5,
                        Children =
                        {
                            new Label()
                                .Text($"Capítulo {capitulo.NumeroCapitulo}")
                                .FontSize(14)
                                .TextColor(Color.FromArgb("#e0e7ff"))
                                .Bold(),

                            new Label()
                                .Text(capitulo.Nombre)
                                .FontSize(24)
                                .TextColor(Colors.White)
                                .Bold(),

                            new Label
                            {
                                MaxLines = 2
                            }
                                .Text(capitulo.Descripcion)
                                .FontSize(14)
                                .TextColor(Color.FromArgb("#cbd5e1"))
                        }
                    }.Column(0),

                    new Border
                    {
                        StrokeShape = new RoundRectangle { CornerRadius = 25 },
                        BackgroundColor = Color.FromArgb("#ffffff20"),
                        WidthRequest = 50,
                        HeightRequest = 50,
                        Content = new Label()
                            .Text($"{capitulo.Niveles.Count(n => n.Completado)}/{capitulo.Niveles.Count}")
                            .FontSize(16)
                            .TextColor(Colors.White)
                            .Bold()
                            .Center()
                    }.Column(1)
                }
            }
        };
    }

    private View CrearNivelesCapitulo(CapituloModel capitulo)
    {
        return new ScrollView
        {
            Orientation = ScrollOrientation.Horizontal,
            Content = new HorizontalStackLayout
            {
                Spacing = 15,
                Padding = new Thickness(5, 0),
                Children = { CrearTarjetasNiveles(capitulo.Niveles) }
            }
        };
    }

    private View CrearTarjetasNiveles(List<NivelModel> niveles)
    {
        var stackLayout = new HorizontalStackLayout { Spacing = 15 };

        foreach (var nivel in niveles)
        {
            stackLayout.Children.Add(CrearTarjetaNivel(nivel));
        }

        return stackLayout;
    }

    private View CrearTarjetaNivel(NivelModel nivel)
    {
        var colorTarjeta = nivel.Desbloqueado ? "#1e293b" : "#0f172a";
        var colorBorde = nivel.Desbloqueado ? "#475569" : "#334155";
        var colorTexto = nivel.Desbloqueado ? Colors.White : Color.FromArgb("#64748b");

        return new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 20 },
            BackgroundColor = Color.FromArgb(colorTarjeta),
            Stroke = Color.FromArgb(colorBorde),
            StrokeThickness = nivel.Desbloqueado ? 2 : 1,
            Shadow = new Shadow
            {
                Offset = new Point(0, 8),
                Radius = 25,
                Opacity = nivel.Desbloqueado ? 0.4f : 0.2f,
                Brush = Colors.Black.AsPaint()
            },
            WidthRequest = 320,
            HeightRequest = 420,
            Content = new Grid
            {
                RowDefinitions = Rows.Define(
                    (RowTarjeta.Imagen, 180),
                    (RowTarjeta.Contenido, Star),
                    (RowTarjeta.Botones, 60)
                ),
                Children =
                {
                    // Imagen con overlay y botón info
                    CrearImagenNivel(nivel).Row(RowTarjeta.Imagen),
                    
                    // Contenido del nivel
                    CrearContenidoNivel(nivel, colorTexto).Row(RowTarjeta.Contenido),
                    
                    // Botón de jugar
                    CrearBotonJugar(nivel).Row(RowTarjeta.Botones)
                }
            }
        };
    }

    private View CrearImagenNivel(NivelModel nivel)
    {
        return new Grid
        {
            Children =
            {
                // Imagen de fondo
                new Border
                {
                    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20, 20, 0, 0) },
                    Content = new Image
                    {
                        Source = nivel.ImagenFondo,
                        Aspect = Aspect.AspectFill
                    }
                },
                
                // Overlay con gradiente
                new Border
                {
                    StrokeShape = new RoundRectangle { CornerRadius = new CornerRadius(20, 20, 0, 0) },
                    Background = new LinearGradientBrush
                    {
                        StartPoint = new Point(0, 0),
                        EndPoint = new Point(0, 1),
                        GradientStops = new GradientStopCollection
                        {
                            new GradientStop { Color = Color.FromArgb("#00000000"), Offset = 0.0f },
                            new GradientStop { Color = Color.FromArgb("#80000000"), Offset = 1.0f }
                        }
                    }
                },
                
                // Botón de información (esquina superior derecha)
                new Button
                {
                    Text = "ℹ️",
                    Margin = new Thickness(10, 10, 0, 0),
                    FontSize = 20,
                    BackgroundColor = Color.FromArgb("#ffffff30"),
                    TextColor = Colors.White,
                    CornerRadius = 20,
                    WidthRequest = 40,
                    HeightRequest = 40,
                    Padding = 0,
                    Command = _viewModel.MostrarInfoNivel,
                    CommandParameter = nivel
                }
                .Top().End(),
                
                // Información superpuesta en la imagen
                new Grid
                {
                    VerticalOptions = LayoutOptions.End,
                    Padding = new Thickness(15, 10),
                    Children =
                    {
                        new StackLayout
                        {
                            Spacing = 5,
                            Children =
                            {
                                new Label()
                                    .Text($"Nivel {nivel.NumeroNivel}")
                                    .FontSize(14)
                                    .TextColor(Color.FromArgb("#e2e8f0"))
                                    .Bold(),

                                new Label
                                {
                                    MaxLines = 2
                                }
                                    .Text(nivel.Titulo)
                                    .FontSize(18)
                                    .TextColor(Colors.White)
                                    .Bold(),

                                CrearEstrellas(nivel.Estrellas)
                            }
                        }
                    }
                }
            }
        };
    }

    private View CrearContenidoNivel(NivelModel nivel, Color colorTexto)
    {
        return new Grid
        {
            Padding = new Thickness(15, 15, 15, 0),
            RowDefinitions = Rows.Define(
                (RowContenido.Descripcion, Auto),
                (RowContenido.Separador, Auto),
                (RowContenido.Info, Star)
            ),
            Children =
            {
                // Descripción
                new Label
                {
                    MaxLines = 3,
                    LineBreakMode = LineBreakMode.TailTruncation

                }
                    .Text(nivel.Descripcion)
                    .FontSize(13)
                    .TextColor(colorTexto)
                    .Row(RowContenido.Descripcion),
                
                // Separador
                new BoxView
                {
                    HeightRequest = 1,
                    BackgroundColor = Color.FromArgb("#334155"),
                    Margin = new Thickness(0, 10)
                }.Row(RowContenido.Separador),
                
                // Información adicional
                new Grid
                {
                    ColumnDefinitions = Columns.Define(Star, Star),
                    RowDefinitions = Rows.Define(Auto, Auto),
                    Children =
                    {
                        CrearInfoChip("⚡", nivel.Dificultad, colorTexto).Row(0).Column(0),
                        CrearInfoChip("⏱️", $"{nivel.TiempoEstimado.TotalMinutes}m", colorTexto).Row(0).Column(1),
                        CrearEstadoChip(nivel).Row(1).ColumnSpan(2)
                    }
                }.Row(RowContenido.Info)
            }
        };
    }

    private View CrearInfoChip(string icono, string texto, Color colorTexto)
    {
        return new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 12 },
            BackgroundColor = Color.FromArgb("#1e293b"),
            Stroke = Color.FromArgb("#475569"),
            StrokeThickness = 1,
            Padding = new Thickness(8, 4),
            Margin = new Thickness(2),
            Content = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 4,
                Children =
                {
                    new Label()
                        .Text(icono)
                        .FontSize(12),

                    new Label()
                        .Text(texto)
                        .FontSize(11)
                        .TextColor(colorTexto)
                }
            }
        };
    }

    private View CrearEstadoChip(NivelModel nivel)
    {
        var (texto, color) = nivel.Completado ? ("✅ Completado", "#22c55e") :
                            nivel.Desbloqueado ? ("🔓 Disponible", "#3b82f6") :
                                               ("🔒 Bloqueado", "#6b7280");

        return new Border
        {
            StrokeShape = new RoundRectangle { CornerRadius = 15 },
            BackgroundColor = Color.FromArgb($"{color}20"),
            Stroke = Color.FromArgb(color),
            StrokeThickness = 1,
            Padding = new Thickness(10, 6),
            Margin = new Thickness(0, 8, 0, 0),
            Content = new Label()
                .Text(texto)
                .FontSize(12)
                .TextColor(Color.FromArgb(color))
                .Bold()
                .CenterHorizontal()
        };
    }

    private View CrearEstrellas(int estrellas)
    {
        return new HorizontalStackLayout
        {
            Spacing = 2,
            Children =
            {
                new Label().Text(estrellas >= 1 ? "⭐" : "☆").FontSize(14),
                new Label().Text(estrellas >= 2 ? "⭐" : "☆").FontSize(14),
                new Label().Text(estrellas >= 3 ? "⭐" : "☆").FontSize(14)
            }
        };
    }

    private View CrearBotonJugar(NivelModel nivel)
    {
        var esJugable = nivel.Desbloqueado;
        var colorBoton = esJugable ? "#22c55e" : "#6b7280";
        var textoBoton = nivel.Completado ? "REPETIR" : "JUGAR";

        return new Border
        {
            BackgroundColor = Color.FromArgb("#1e293b"),
            Padding = new Thickness(15, 8),
            Content = new Button
            {
                Text = textoBoton,
                FontSize = 16,
                TextColor = Colors.White,
                BackgroundColor = Color.FromArgb(colorBoton),
                CornerRadius = 12,
                FontAttributes = FontAttributes.Bold,
                Command = _viewModel.NavegarGameHistoria,
                CommandParameter = nivel,
                IsEnabled = esJugable,
                Shadow = new Shadow
                {
                    Offset = new Point(0, 4),
                    Radius = 10,
                    Opacity = 0.3f,
                    Brush = Color.FromArgb(colorBoton).AsPaint()
                }
            }
            //}.BindCommand("HistoriaNarrativaView")
            //    .Bind(Button.CommandParameterProperty, source: nivel)
        };
    }
    enum RowPrincipal { Header, Content }
    enum RowTarjeta { Imagen, Contenido, Botones }
    enum RowContenido { Descripcion, Separador, Info }
}