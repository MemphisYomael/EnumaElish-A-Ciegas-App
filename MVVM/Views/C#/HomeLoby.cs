using CommunityToolkit.Maui.Markup;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.ViewModels;
using EnumaElishApp.MVVM.Views.XAML;
using Microsoft.Maui.Controls.Shapes;
using Plugin.Maui.Audio;

namespace EnumaElishApp.MVVM.Views.C_;

public class HomeLoby : ContentPage
{
    private readonly IAudioManager _audioManager;
    private IAudioPlayer _player;

    public HomeLoby()
    {
       
        _audioManager = AudioManager.Current;

        MetodosGeneracionColores metodosGeneracionColores = new MetodosGeneracionColores();
        NavigationPage.SetHasNavigationBar(this, false);

        BindingContext = new HomeViewModel();

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
                // Fondo principal
                new Image
                {
                    Source = "fondo_enuma_elish.jpeg",
                    Aspect = Aspect.AspectFill
                }
                .Fill()
                .Row(0)
                .Column(0)
                .ZIndex(-1),

                // Grid principal con la distribución original (10 columnas, 5 filas)
                new Grid
                {
                    Padding = 15,
                    ColumnDefinitions = new ColumnDefinitionCollection
                    {
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition(),
                        new ColumnDefinition(),
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
                        new RowDefinition(),
                    },
                    Children = {
                        // PERFIL DEL JUGADOR - Posición original (primeras 3 columnas, fila 0)
                        new Border
                        {
                            Background = Color.FromArgb("D9171a4a"), // Opacity incluida en el color (85% = D9)
                            StrokeShape = new RoundRectangle { CornerRadius = 20 },
                            StrokeThickness = 2,
                            Stroke = Color.FromArgb("f5e1ce"),
                            Shadow = new Shadow { Offset = new Point(2, 4), Radius = 10, Brush = Colors.Black.AsPaint() },
                            Content = new Grid
                            {
                                Padding = 10,
                                ColumnDefinitions = new ColumnDefinitionCollection{
                                    new ColumnDefinition(),
                                    new ColumnDefinition(),
                                    new ColumnDefinition(),
                                    new ColumnDefinition(),
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
                                    // Avatar
                                    new Border
                                    {
                                        WidthRequest = 40,
                                        HeightRequest = 40,
                                        StrokeShape = new RoundRectangle { CornerRadius = 20 },
                                        StrokeThickness = 2,
                                        Stroke = Color.FromArgb("f5e1ce"),
                                        Background = metodosGeneracionColores.crearDegradado("000020", "ffffff"),
                                        HorizontalOptions = LayoutOptions.Center,
                                        VerticalOptions = LayoutOptions.Center,
                                        Content = new Label
                                        {
                                            TextColor = Color.FromArgb("f5e1ce"),
                                            FontAttributes = FontAttributes.Bold,
                                            HorizontalOptions = LayoutOptions.Center,
                                            VerticalOptions = LayoutOptions.Center,
                                            HorizontalTextAlignment = TextAlignment.Center,
                                            VerticalTextAlignment = TextAlignment.Center,
                                            FontSize = 20
                                        }
                                        .Bind(Label.TextProperty, "nombreLetraInicial")
                                    }
                                    .Column(0).Row(0).ColumnSpan(2).Margin(3).RowSpan(4),

                                    // Nombre del jugador
                                    new Label
                                    {
                                        TextColor = Color.FromArgb("f5e1ce"),
                                        FontSize = 16,
                                        FontAttributes = FontAttributes.Bold
                                    }
#if !WINDOWS      
                                    .Font("text", italic: true, bold: true, size: 12)      
#endif      
                                    .TextColor(Color.FromArgb("f5e1ce"))      
#if WINDOWS      
                                    .Font("text")      
                                    .FontSize(20)      
#endif      
                                    .Column(2)
                                    .ColumnSpan(4)
                                    .Row(0)
                                    .RowSpan(2)
                                    .Bind(Label.TextProperty, "nombrePersona"),

                                    // Información adicional del jugador
                                    new Label
                                    {
                                        Text = "Detective Novato • Casos: 0 | Rango: ⭐",
                                        TextColor = Color.FromArgb("e8c39e"),
                                        FontSize = 9
                                    }
                                    .Column(2)
                                    .ColumnSpan(6)
                                    .Row(2)
                                },
                            }
                        }
                        .Column(0).Row(0).ColumnSpan(3),

                        // MENÚ PRINCIPAL - Posición original (columnas 0-2, filas 2-4)
                        new Border
                        {
                            Background = Color.FromArgb("CC000020"), // Opacity 80% = CC
                            StrokeShape = new RoundRectangle { CornerRadius = 20 },
                            StrokeThickness = 4,
                            Stroke = Color.FromArgb("f5e1ce"),
                            Shadow = new Shadow { Offset = new Point(0, 8), Radius = 20, Brush = Colors.Black.AsPaint() },
                            Content = new Grid
                            {
                                Background = metodosGeneracionColores.crearDegradado("000020", "2f2c79"),
                                RowSpacing = 12,
                                Padding = 10,
                                ColumnDefinitions = new ColumnDefinitionCollection{
                                    new ColumnDefinition()
                                },
                                RowDefinitions = new RowDefinitionCollection
                                {
                                    new RowDefinition(),
                                    new RowDefinition(),
                                    new RowDefinition(),
                                },

                                Children =
                                {
                                    // Crear Nueva Sala
                                    new Border
                                    {
                                        StrokeShape = new RoundRectangle { CornerRadius = 15 },
                                        StrokeThickness = 2,
                                        Stroke = Color.FromArgb("f5e1ce"),
                                        Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                                        Shadow = new Shadow { Offset = new Point(0, 3), Radius = 8, Brush = Colors.Black.AsPaint() },
                                        Content = new Button
                                        {
                                            Text = "🏠 CREAR NUEVA SALA",
                                            TextColor = Color.FromRgba("ffffff"),
                                            FontSize = 16,
                                            FontAttributes = FontAttributes.Bold,
                                            BackgroundColor = Colors.Transparent,
                                            Padding = new Thickness(20, 8)
                                        }.BindCommand("CrearSalaCommand")
                                    }
                                    .Row(0),

                                    // Unirse a Sala
                                    new Border
                                    {
                                        StrokeShape = new RoundRectangle { CornerRadius = 15 },
                                        StrokeThickness = 2,
                                        Stroke = Color.FromArgb("e8c39e"),
                                        Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                                        Shadow = new Shadow { Offset = new Point(0, 3), Radius = 8, Brush = Colors.Black.AsPaint() },
                                        Content = new Button
                                        {
                                            Text = "🔍 UNIRSE A SALA",
                                            TextColor = Color.FromRgba("ffffff"),
                                            FontSize = 16,
                                            FontAttributes = FontAttributes.Bold,
                                            BackgroundColor = Colors.Transparent,
                                            Padding = new Thickness(20, 8)
                                        }.BindCommand("unirseASala")
                                    }
                                    .Row(1),

                                    // Modo Historia
                                    new Border
                                    {
                                        StrokeShape = new RoundRectangle { CornerRadius = 15 },
                                        StrokeThickness = 2,
                                        Stroke = Color.FromArgb("e8c39e"),
                                        Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                                        Shadow = new Shadow { Offset = new Point(0, 3), Radius = 8, Brush = Colors.Black.AsPaint() },
                                        Content = new Button
                                        {
                                            Text = "📖 MODO HISTORIA",
                                            TextColor = Color.FromRgba("ffffff"),
                                            FontSize = 16,
                                            FontAttributes = FontAttributes.Bold,
                                            BackgroundColor = Colors.Transparent,
                                            Padding = new Thickness(20, 8)
                                        }.BindCommand("nivelesCarrucel")
                                    }
                                    .Row(2)
                                }
                            }
                        }
                        .Column(0)
                        .Row(2)
                        .RowSpan(3)
                        .ColumnSpan(3),

                        // BOTONES LATERALES - Corregido para móvil
                        // Botón de Configuración (⚙️)
                        new Border
                        {
                            Background = Color.FromArgb("E6171a4a"), // Opacity 90% = E6
                            StrokeShape = new RoundRectangle { CornerRadius = 15 },
                            StrokeThickness = 2,
                            Stroke = Color.FromArgb("f5e1ce"),
                            Shadow = new Shadow { Offset = new Point(2, 2), Radius = 6, Brush = Colors.Black.AsPaint() },
                            Content = new Button
                            {
                                Text = "⚙️",
                                TextColor = Color.FromArgb("f5e1ce"),
                                FontSize = 18,
                                BackgroundColor = Colors.Transparent,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
#if WINDOWS
                        .Width(80)
                        .Height(80)
                        .Column(9)
                        .Row(1)
#else
                        .Width(50)
                        .Height(50)
                        .Column(8)
                        .Row(0)
#endif
                        ,

                        // Botón de Sonido (🔊)
                        new Border
                        {
                            Background = Color.FromArgb("E6171a4a"), // Opacity 90% = E6
                            StrokeShape = new RoundRectangle { CornerRadius = 15 },
                            StrokeThickness = 2,
                            Stroke = Color.FromArgb("f5e1ce"),
                            Shadow = new Shadow { Offset = new Point(2, 2), Radius = 6, Brush = Colors.Black.AsPaint() },
                            Content = new Button
                            {
                                Text = "🔊",
                                TextColor = Color.FromArgb("f5e1ce"),
                                FontSize = 18,
                                BackgroundColor = Colors.Transparent,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.Center
                            }
                        }
#if WINDOWS
                        .Width(80)
                        .Height(80)
                        .Column(9)
                        .Row(0)
#else
                        .Width(50)
                        .Height(50)
                        .Column(9)
                        .Row(0)
#endif
                        ,

                        // TÍTULO DEL JUEGO - Posición original
                        new Border
                        {
                            Background = Color.FromArgb("B3000020"), // Opacity 70% = B3
                            StrokeShape = new RoundRectangle { CornerRadius = 12 },
                            StrokeThickness = 2,
                            Stroke = Color.FromArgb("f5e1ce"),
                            Shadow = new Shadow { Offset = new Point(0, 4), Radius = 15, Brush = Colors.Black.AsPaint() },
                            Content = new StackLayout
                            {
                                Orientation = StackOrientation.Vertical,
                                Padding = 8,
                                Spacing = 2,
                                Children = {
                                    new Label
                                    {
                                        Text = "🕵️ ENUMA ELISH",
                                        TextColor = Color.FromArgb("f5e1ce"),
                                        FontSize = 16,
                                        FontAttributes = FontAttributes.Bold,
                                        HorizontalTextAlignment = TextAlignment.Center
                                    },
                                    new Label
                                    {
                                        Text = "Mystery Detective • Close Beta",
                                        TextColor = Color.FromArgb("e8c39e"),
                                        FontSize = 10,
                                        FontAttributes = FontAttributes.Italic,
                                        HorizontalTextAlignment = TextAlignment.Center
                                    }
                                }
                            }
                        }
                        .Column(6)
                        .Row(4)
                        .ColumnSpan(4)
                    }
                }
                .Column(0)
                .Row(0)
            }
        };
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            var file = await FileSystem.OpenAppPackageFileAsync("claro_de_luna_1er.mp3");
            _player = _audioManager.CreatePlayer(file);
            _player.Loop = true;
            _player.Volume = 0.3;
            _player.Play();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Error loading audio: {ex.Message}");
        }
    }


    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _player?.Stop();
        _player?.Dispose();
    }
}// Fix for CS0103: Define the 'PageCache' class or namespace if missing  
// Fix for CS0246: Define the 'MyHeavyPage' class or namespace if missing  

// Assuming 'PageCache' is a static class used for caching pages  
public static class PageCache
{
    public static ContentPage CachedHeavyPage { get; set; }
}

