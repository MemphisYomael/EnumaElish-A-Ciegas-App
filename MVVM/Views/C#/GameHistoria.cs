using CommunityToolkit.Maui.Markup;
using EnumaElishApp.Converters;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.Models.Canvas;
using EnumaElishApp.MVVM.ViewModels.Niveles;
using Microsoft.Maui.Controls.Shapes;

namespace EnumaElishApp.MVVM.Views.C_;

public class GameHistoria : ContentPage
{
    protected override bool OnBackButtonPressed()
    {
        Dispatcher.Dispatch(async () =>
        {
            var result = await DisplayAlert(
                "Salir del juego",
                "¿Estás seguro de que quieres abandonar la partida? Se perderá todo el progreso.",
                "Sí, salir",
                "Continuar jugando"
            );

            if (result)
            {
                // Opcional: limpiar recursos o guardar estado  
                viewModel?.DetenerAnimacion();
                await Navigation.PopAsync();
            }
        });
        return true; // Siempre bloquea la navegación automática  
    }


    Grid mainGrid;
    Nivel1ViewModel viewModel;
    MetodosGeneracionColores metodosGeneracionColores;
    ControlPersonalizadoSkia controlReset;
    BolaIsSelected bolaIsSelectedConverter = new BolaIsSelected();
    PorcentajeDeLaCarga calcularPorcentajeCargaConverter = new PorcentajeDeLaCarga();
    BolaConsumidaConverter bolaConsumidaConverter = new BolaConsumidaConverter();
    Image imagenAqua;
    public GameHistoria()
    {
        NavigationPage.SetHasNavigationBar(this, false);
        viewModel = new Nivel1ViewModel();
        BindingContext = viewModel;
        BackgroundColor = Colors.Black;
        metodosGeneracionColores = new MetodosGeneracionColores();
        imagenAqua = new Image
        {
            Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
            Aspect = Aspect.AspectFit
        };
        controlReset = new ControlPersonalizadoSkia
        {
#if WINDOWS
            CircleSize = 120,
#else
            CircleSize = 90,
#endif
            PorcentageCarga = 0.9f,
            ComandoAccionPressed = new Command((habilidad) =>
            {
                RerenderizarBolas();
            }),
        }
        .Center()
#if WINDOWS
        .Width(100)
        .Height(100);
#else
        .Width(80)
        .Height(80);
#endif

        mainGrid = new Grid
        {
#if WINDOWS
            Padding = 20,
            ColumnSpacing = 20,
#else
            RowSpacing = 10,
#endif
            //Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
            //Padding = new Thickness(10),
            //Shadow = new Shadow
            //{
            //    Offset = new Point(0, 0),
            //    Radius = 30
            //},
            RowDefinitions = new RowDefinitionCollection {
#if WINDOWS
        new RowDefinition(new GridLength(80)), // Altura aumentada para Windows
        new RowDefinition(new GridLength(80)),
        new RowDefinition(new GridLength(80)),
        new RowDefinition(new GridLength(80)),
        new RowDefinition(new GridLength(80)),
#else
         new RowDefinition(new GridLength(40)), // Altura fija más pequeña
        new RowDefinition(new GridLength(40)),
        new RowDefinition(new GridLength(40)),
        new RowDefinition(new GridLength(40)),
        new RowDefinition(new GridLength(40)),
#endif
        },
            ColumnDefinitions = new ColumnDefinitionCollection {
          new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
            new ColumnDefinition(GridLength.Star),
        }
        }.End();
        Content = new Grid
        {
            Children = {
        new Image {
             Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
          Source = "escenario1.jpeg",
            Aspect = Aspect.AspectFill
        }
        .Fill()
        .Row(0)
        .Column(0)
        .ZIndex(-1),

        new Grid {
#if WINDOWS
            Padding = 60,
#else
            Padding = 40,
#endif
            ColumnDefinitions = new ColumnDefinitionCollection {
              new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star),
                new ColumnDefinition(GridLength.Star)
            },
            RowDefinitions = new RowDefinitionCollection {
              new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star),
                new RowDefinition(GridLength.Star)
            },

            Children = {
              new Border {
                StrokeShape = new RoundRectangle {
                    CornerRadius = 15
                  },
#if WINDOWS
                Padding = 15,
                  StrokeThickness = 3,
#else
                Padding = 10,
                  StrokeThickness = 2,
#endif
                  Stroke = Color.FromArgb("f5e1ce"),
                  Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                  Shadow = new Shadow {
                    Offset = new Point(0, 3),
                      Radius = 8,
                      Brush = Colors.Black.AsPaint()
                  },
                  Content = new Grid{
                      new Label()
                      .Bind(Label.TextProperty, "BarrapersonajeYhp")
                      .Center()
                  }

              }
              .ColumnSpan(2)
              .Row(0)
              .Column(0),

              new Border {
                StrokeShape = new RoundRectangle {
                    CornerRadius = 15
                  },
#if WINDOWS
                  StrokeThickness = 3,
#else
                  StrokeThickness = 2,
#endif
                  Stroke = Color.FromArgb("f5e1ce"),
                  Background = metodosGeneracionColores.crearDegradado3Colores("2f2c79", "171a4a", "000020"),
                  Shadow = new Shadow {
                    Offset = new Point(0, 3),
                      Radius = 8,
                      Brush = Colors.Black.AsPaint()
                  },
                  Content = 
                      new Grid{
                      new Label()
                      .Bind(Label.TextProperty, "hpEnemy")
                      .Center()
                  }


              }
              .ColumnSpan(2)
              .Row(0)
              .Column(6),

              mainGrid
              .Row(2)
              .Column(2)
              .RowSpan(8)
              .ColumnSpan(4),

              imagenAqua
              .End()
              .Row(0)
              .Column(1)
#if WINDOWS
              .Width(100)
              .Height(100)
#else
              .Width(80)
              .Height(80)
#endif
              .ZIndex(1)
              .Bind(Image.SourceProperty, "imagenPersonajePosicionInicial"),


              new Image
        {
                   Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
            Aspect = Aspect.AspectFit
        }.End()
              .Row(0)
              .Column(2)
#if WINDOWS
              .Width(150)
              .Height(150)
#else
              .Width(120)
              .Height(120)
#endif
              .ZIndex(1)
              .Bind(Image.SourceProperty, "ctlImagen1"),

              new Image
        {
                   Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
            Aspect = Aspect.AspectFit
        }.End()
              .Row(0)
              .Column(3)
#if WINDOWS
              .Width(150)
              .Height(150)
#else
              .Width(120)
              .Height(120)
#endif
              .ZIndex(1)
              .Bind(Image.SourceProperty, "ctlImagen2"),
              new Image
        {
                   Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
            Aspect = Aspect.AspectFit
        }.End()
              .Row(0)
              .Column(4)
#if WINDOWS
              .Width(150)
              .Height(150)
#else
              .Width(120)
              .Height(120)
#endif
              .ZIndex(1)
              .Bind(Image.SourceProperty, "ctlImagen3"),
              new Image
        {
                   Shadow = new Shadow
            {
                Offset = new Point(0, 0),
                Radius = 30
            },
            Aspect = Aspect.AspectFit
        }.End()
              .Row(0)
              .Column(5)
#if WINDOWS
              .Width(150)
              .Height(150)
#else
              .Width(120)
              .Height(120)
#endif
              .ZIndex(1)
              .Bind(Image.SourceProperty, "ctlImagen4"),

              new CollectionView {
                ItemTemplate = new DataTemplate(() => {
                  var control = new ControlPersonalizadoSkia {
#if WINDOWS
                      CircleSize = 95,
#else
                      CircleSize = 75,
#endif
                      ColorBorder = Colors.DarkBlue,

                    }
                    .Bind(ControlPersonalizadoSkia.ColorFondoProperty, "Color")
                    .Bind(ControlPersonalizadoSkia.comandoAccionPressedProperty, source: viewModel, path: "ComandoPressedHabilidad")
                    .Bind(ControlPersonalizadoSkia.CommandParameterProperty, path: ".") // pasa el objeto Habilidades como parámetro
                    .Bind(ControlPersonalizadoSkia.ColorBorderProperty, "Color")
                    .Bind(ControlPersonalizadoSkia.PorcentageCargaProperty, "PorcentajeCargado")

#if WINDOWS
                    .Width(95).Height(95);
#else
                    .Width(75).Height(75);
#endif

                  var grid = new Grid {
                      Children = {
                        new Image {
                          Aspect = Aspect.AspectFit
                        }
                        .Bind(Image.SourceProperty, "imagenAtaque")
#if WINDOWS
                        .Width(50).Height(50)
#else
                        .Width(40).Height(40)
#endif
                        .Center(),

                        control.ZIndex(-1),

                      }
                  };
                  return grid;
                })
              }
              .Bind(CollectionView.ItemsSourceProperty, "habilidades")
              .Column(0).Row(2).RowSpan(6),

            controlReset.Row(3).Column(1),
            new Image().Source(ImageSource.FromFile("recharge.png"))
#if WINDOWS
            .Width(60).Height(60)
#endif
            .Row(3).Column(1),

               new CollectionView {
                    ItemsLayout = new LinearItemsLayout(ItemsLayoutOrientation.Vertical)
    {
#if WINDOWS
        ItemSpacing = 8 
#else
        ItemSpacing = 5 
#endif
    },
                ItemTemplate = new DataTemplate(() => {
                  var control = new ControlPersonalizadoSkia {
#if WINDOWS
                      CircleSize = 90,
#else
                      CircleSize = 70,
#endif
                      ColorBorder = Colors.DarkBlue,
                      //ComandoAccionPressed = new Command((habilidad) => {
                      //  //RerenderizarBolas();
                      //}),
                    }
                    .Bind(ControlPersonalizadoSkia.ColorFondoProperty, "Color")
                    .Bind(ControlPersonalizadoSkia.ColorBorderProperty, "Color")
                    .Bind(ControlPersonalizadoSkia.PorcentageCargaProperty, "PorcentajeCargado")
#if WINDOWS
                    .Width(90).Height(90);
#else
                    .Width(70).Height(70);
#endif

                  var grid = new Grid {
                                  Padding = new Thickness(0), // O valores pequeños como 2

                      Children = {
                        new Image {
                          Aspect = Aspect.AspectFit
                        }
                        .Bind(Image.SourceProperty, "imagenAtaque")
#if WINDOWS
                        .Width(50).Height(50)
#else
                        .Width(40).Height(40)
#endif
                        .Center(),

                        control.ZIndex(-1),

                      }
                  };
                  return grid;
                })
              }
              .Bind(CollectionView.ItemsSourceProperty, "habilidadesEnemy")
              .Column(7).Row(2).RowSpan(6)
            },



        }.Column(0).Row(0),


      }

        };

        CreateEsferas();
    }

    private void CreateEsferas()
    {
        if (viewModel.bolas != null && viewModel.bolas.Count > 0) // Validación adicional
        {
            for (int i = 0; i < viewModel.bolas.Count; i++)
            {
                var esfera = CreateEsfera(viewModel.bolas[i]);
                int row = viewModel.bolas[i].posicion / 7;
                int column = viewModel.bolas[i].posicion % 7;

                esfera.SetValue(Grid.RowProperty, row);
                esfera.SetValue(Grid.ColumnProperty, column);

                var imagen = new Image
                {
                    Aspect = Aspect.AspectFit,
                    Source = "mementomori.svg"
                }
#if WINDOWS
                .Width(40).Height(40)
#else
                .Width(30).Height(30)
#endif
                .ZIndex(0)
                .Center();
                imagen.SetValue(Grid.RowProperty, row);
                imagen.SetValue(Grid.ColumnProperty, column);
                imagen.Bind(Image.SourceProperty, "imagen", source: viewModel.bolas[i]);

                mainGrid.Children.Add(esfera);
                mainGrid.Children.Add(imagen);
            }
        }
    }

    private ControlPersonalizadoSkia CreateEsfera(object bolaData)
    {
        var Esferas = new ControlPersonalizadoSkia
        {
#if WINDOWS
            CircleSize = 80,
#else
            CircleSize = 60,
#endif
            CommandParameter = bolaData,
            PorcentageCarga = 0.99f
        }
          .Bind(ControlPersonalizadoSkia.ColorFondoProperty, "color", source: bolaData)
          .Bind(ControlPersonalizadoSkia.comandoAccionPressedProperty, source: viewModel, path: "ComandoSlashBola")
          .Bind(ControlPersonalizadoSkia.ColorBorderProperty, source: bolaData, path: "seleccionada", converter: bolaIsSelectedConverter)
      .Bind(ControlPersonalizadoSkia.PorcentageCargaProperty, source: bolaData, path: "consumida", converter: bolaConsumidaConverter) // NUEVO binding
#if WINDOWS
          .Height(80).Width(80)
#else
          .Height(80).Width(80)
#endif
          .Fill();

        return Esferas;
    }

    //boton de recargar 

    public void RerenderizarBolas()
    {
        viewModel.renderizarBolasAleagorias();
        viewModel.bolasSeleccionadas.Clear();
        mainGrid.Children.Clear();
        CreateEsferas();
    }
}