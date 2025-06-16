using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using EnumaElishApp.MVVM.Models;
using PropertyChanged;
using Font = Microsoft.Maui.Font;

namespace EnumaElishApp.MVVM.ViewModels.Niveles
{
    [AddINotifyPropertyChangedInterface]
    public class Nivel1ViewModel
    {
        public List<Bola> bolas { get; set; }
        public List<Habilidades> habilidades { get; set; }
        public List<Habilidades> habilidadesEnemy { get; set; }
        public ICommand ComandoPressedHabilidad { get; set; }
        public ICommand ComandoSlashBola { get; set; }

        private bool _ejecutandoHabilidad { get; set; }
        public bool posicion1 { get; set; } = true;

        public string imagenPersonajePosicionInicial { get; set; }
        public string ctlImagen1 { get; set; }
        public string ctlImagen2 { get; set; }
        public string ctlImagen3 { get; set; }
        public string ctlImagen4 { get; set; }
        public string BarrapersonajeYhp { get; set; }
        public int hpPersonaje { get; set; } = 100;
        public int hpEnemy { get; set; } = 100;
        public bool isGameOver { get; set; } = false;
        public ObservableCollection<Bola> bolasSeleccionadas { get; set; }

        // ✅ SOLUCIÓN: Control de animación mejorado
        private CancellationTokenSource _animationCancellationToken;
        private bool _animacionEnCurso = false;

        public Nivel1ViewModel()
        {
            habilidadesEnemy = new List<Habilidades>();
            BarrapersonajeYhp = "AQUA || HP: 100";
            _ejecutandoHabilidad = false;
            bolasSeleccionadas = new ObservableCollection<Bola>();

            ComandoPressedHabilidad = new Command(async (object param) => {
                await EjecutarHabilidad((Habilidades)param);
            });

            ComandoSlashBola = new Command((object param) => {
                Bola bolaSeleccionada = (Bola)param;
                bolaSeleccionada.seleccionada = !bolaSeleccionada.seleccionada;
                if (bolaSeleccionada.seleccionada)
                {
                    bolasSeleccionadas.Add(bolaSeleccionada);
                }
                else
                {
                    bolasSeleccionadas.Remove(bolaSeleccionada);
                }
                agregarYVerificarSucesionDeBolas();
            });

            habilidades = new List<Habilidades>();
            bolas = new List<Bola>();
            renderizarBolasAleagorias();
            renderizarHablidades();

            // ✅ Iniciar animación de forma controlada
            _ = IniciarAnimacionContinua();
            _ = IniciarAtaqueEnemigo();
        }

        // ✅ MÉTODO REFACTORIZADO: Ejecutar habilidad de forma asíncrona
        private async Task EjecutarHabilidad(Habilidades habilidadSeleccionada)
        {
            if (_ejecutandoHabilidad) return; // Prevenir ejecuciones múltiples

            // Validar carga para "Ataque de Fuego"
            if (habilidadSeleccionada.nombreAtaque == "Ataque de Fuego" &&
                habilidadSeleccionada.cargaAtaque != habilidadSeleccionada.cargaEsperadaAtaque)
            {
                return;
            }

            _ejecutandoHabilidad = true;

            try
            {
                // Aplicar daño y resetear carga
                habilidadSeleccionada.cargaAtaque = 0;
                hpEnemy -= habilidadSeleccionada.dañoAtaque;
                BarrapersonajeYhp = "AQUA || HP: " + hpPersonaje;

                // ✅ Ejecutar animación de ataque sin bloquear
                await EjecutarAnimacionAtaque();

                DeterminarGanador();
            }
            finally
            {
                _ejecutandoHabilidad = false;
            }
        }

        // ✅ MÉTODO NUEVO: Animación de ataque optimizada
        private async Task EjecutarAnimacionAtaque()
        {
            // Detener animación continua temporalmente
            var wasAnimating = _animacionEnCurso;
            if (wasAnimating)
            {
                DetenerAnimacion();
            }

            try
            {
                imagenPersonajePosicionInicial = "";

                // Secuencia de animación rápida
                ctlImagen1 = "aqua_posicion_inicial0000.png";
                await Task.Delay(50);

                ctlImagen1 = "";
                ctlImagen2 = "aqua_posicion_inicial0001.png";
                await Task.Delay(50);

                ctlImagen2 = "";
                ctlImagen3 = "aqua_posicion_inicial0002.png";
                await Task.Delay(50);

                ctlImagen3 = "";
                ctlImagen4 = "aqua_posicion_inicial0002.png";
                await Task.Delay(300); // Tiempo de impacto reducido

                // Limpiar animación
                ctlImagen4 = "";
                ctlImagen1 = "";
                imagenPersonajePosicionInicial = "aqua_posicion_inicial0000.png";
            }
            finally
            {
                // Reanudar animación continua si estaba activa
                if (wasAnimating && !isGameOver)
                {
                    _ = IniciarAnimacionContinua();
                }
            }
        }

        // ✅ MÉTODO REFACTORIZADO: Animación continua controlada
        private async Task IniciarAnimacionContinua()
        {
            if (_animacionEnCurso) return;

            _animacionEnCurso = true;
            _animationCancellationToken = new CancellationTokenSource();
            int contador = 0;

            try
            {
                while (!_animationCancellationToken.Token.IsCancellationRequested &&
                       posicion1 && !isGameOver)
                {
                    if (!_ejecutandoHabilidad)
                    {
                        Application.Current.Dispatcher.Dispatch(() =>
                        {
                            imagenPersonajePosicionInicial = $"aqua_posicion_inicial{contador.ToString("0000")}.png";
                        });

                        await Task.Delay(500, _animationCancellationToken.Token);
                        contador = (contador + 1) % 2; // Alternar entre 0 y 1
                    }
                    else
                    {
                        // Esperar mientras se ejecuta habilidad
                        await Task.Delay(100, _animationCancellationToken.Token);
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Animación cancelada correctamente
            }
            finally
            {
                _animacionEnCurso = false;
            }
        }

        // ✅ MÉTODO MEJORADO: Detener animación
        public void DetenerAnimacion()
        {
            _animationCancellationToken?.Cancel();
            _animacionEnCurso = false;
        }

        // ✅ MÉTODO REFACTORIZADO: Ataque enemigo sin bloquear
        private async Task IniciarAtaqueEnemigo()
        {
            while (!isGameOver)
            {
                try
                {
                    for (int ciclos = 0; ciclos < habilidadesEnemy.Count && !isGameOver; ciclos++)
                    {
                        if (posicion1 && !_ejecutandoHabilidad)
                        {
                            habilidadesEnemy[ciclos].cargaAtaque += 20;
                            if (habilidadesEnemy[ciclos].cargaAtaque >= habilidadesEnemy[ciclos].cargaEsperadaAtaque)
                            {
                                hpPersonaje -= habilidadesEnemy[ciclos].dañoAtaque;
                                BarrapersonajeYhp = "AQUA || HP: " + hpPersonaje;
                                habilidadesEnemy[ciclos].cargaAtaque = 0;

                                DeterminarGanador();

#if !WINDOWS
                                if (habilidadesEnemy[ciclos].PorcentajeCargado < 80)
                                {

                                    var snackbarOptions = new SnackbarOptions
                                    {
                                        BackgroundColor = Colors.Black,
                                        TextColor = Colors.White,
                                        ActionButtonTextColor = Colors.White,
                                        CornerRadius = 20,
                                        Font = Font.SystemFontOfSize(14)
                                    };

                                    var snackbar = Snackbar.Make(
                                        "Ahi viene un ataque!!!",
                                        () => Console.Write(""),
                                        "Cuidado!",
                                        TimeSpan.FromSeconds(.2),
                                        snackbarOptions);

                                    await snackbar.Show();
                                }

#endif
                            }
                        }

                        await Task.Delay(2000);
                    }
                }
                catch (Exception ex)
                {
                    // Log error si es necesario
                    System.Diagnostics.Debug.WriteLine($"Error en ataque enemigo: {ex.Message}");
                    break;
                }
            }
        }

        // ✅ Los demás métodos permanecen igual...
        public void renderizarHablidades()
        {
            habilidades.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Fuego",
                textoVozPersonaje = "¡Siente el poder del fuego!",
                descripcionAtaque = "Un ataque devastador que quema a los enemigos.",
                imagenAtaque = "tigerhead.png",
                sonidoAtaque = "fuego.mp3",
                dañoAtaque = 50,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.IndianRed
            });

            habilidadesEnemy.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Fuego",
                textoVozPersonaje = "¡Siente el poder del fuego!",
                descripcionAtaque = "Un ataque devastador que quema a los enemigos.",
                imagenAtaque = "tigerhead.png",
                sonidoAtaque = "fuego.mp3",
                dañoAtaque = 50,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.IndianRed
            });

            habilidades.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Agua",
                textoVozPersonaje = "¡El agua purifica todo a su paso!",
                descripcionAtaque = "Un ataque que ahoga a los enemigos con una poderosa corriente.",
                imagenAtaque = "mementomori.png",
                sonidoAtaque = "agua.mp3",
                dañoAtaque = 40,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.CornflowerBlue
            });

            habilidadesEnemy.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Agua",
                textoVozPersonaje = "¡El agua purifica todo a su paso!",
                descripcionAtaque = "Un ataque que ahoga a los enemigos con una poderosa corriente.",
                imagenAtaque = "mementomori.png",
                sonidoAtaque = "agua.mp3",
                dañoAtaque = 40,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.CornflowerBlue
            });

            habilidades.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Tierra",
                textoVozPersonaje = "¡La fuerza de la tierra es imparable!",
                descripcionAtaque = "Un ataque que aplasta a los enemigos con rocas y tierra.",
                imagenAtaque = "tigerhead.png",
                sonidoAtaque = "tierra.mp3",
                dañoAtaque = 60,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.Orange
            });

            habilidadesEnemy.Add(new Habilidades
            {
                nombreAtaque = "Ataque de Tierra",
                textoVozPersonaje = "¡La fuerza de la tierra es imparable!",
                descripcionAtaque = "Un ataque que aplasta a los enemigos con rocas y tierra.",
                imagenAtaque = "tigerhead.png",
                sonidoAtaque = "tierra.mp3",
                dañoAtaque = 60,
                cargaAtaque = 0,
                cargaEsperadaAtaque = 100,
                Color = Colors.Orange
            });
        }

        public async void agregarYVerificarSucesionDeBolas()
        {
            // Método permanece igual...
            if (bolasSeleccionadas.Count() <= 0) return;

            var bolasSeleccionadasCount = bolasSeleccionadas.Where(b => b.consumida == true).Count() > 0;
            Color colorSeleccionado = bolasSeleccionadas[0].color;
            string imagenSeleccionada = bolasSeleccionadas[0].imagen;

            if (bolasSeleccionadas.Where(b => b.seleccionada == true && b.color == colorSeleccionado).Count() == 3 && !bolasSeleccionadasCount)
            {
                var habilidadAsociada = habilidades.Where(x => x.Color == colorSeleccionado && x.imagenAtaque == imagenSeleccionada).FirstOrDefault();

                foreach (var bola in bolasSeleccionadas.ToList())
                {
                    bola.consumida = true;
                    bola.seleccionada = false;
                }

                bolasSeleccionadas.Clear();

                for (int i = 0; i < bolas.Count; i++)
                {
                    if (!bolas[i].consumida)
                    {
                        bolas[i].seleccionada = false;
                    }
                }

                if (habilidadAsociada != null)
                {
                    if (habilidadAsociada.cargaAtaque >= 100)
                    {
                        var currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                        if (currentPage != null)
                        {
                            await currentPage.DisplayAlert("¡Habilidad Cargada!", $"Tu habilidad ya esta llena", "OK");
                        }
                        return;
                    }

                    habilidadAsociada.cargaAtaque += 20;

                    if (habilidadAsociada.cargaAtaque >= habilidadAsociada.cargaEsperadaAtaque)
                    {
                        habilidadAsociada.cargaAtaque = 100;
                        var currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                        if (currentPage != null)
                        {
#if !WINDOWS
                            var snackbarOptions = new SnackbarOptions
                            {
                                BackgroundColor = Colors.Green,
                                TextColor = Colors.White,
                                ActionButtonTextColor = Colors.White,
                                CornerRadius = 20,
                                Font = Font.SystemFontOfSize(14)
                            };

                            var snackbar = Snackbar.Make(
                                "¡Habilidad Cargada! Has cargado la habilidad: {habilidadAsociada.nombreAtaque}",
                                () => ComandoPressedHabilidad.Execute(habilidadAsociada),
                                "ATACAR!",
                                TimeSpan.FromSeconds(3),
                                snackbarOptions);

                            await snackbar.Show();
#endif
                        }
                    }
                }
            }
            else if (bolasSeleccionadas.Count == 3 && !bolasSeleccionadasCount)
            {
                bolasSeleccionadas.Clear();
                for (int i = 0; i < bolas.Count; i++)
                {
                    bolas[i].seleccionada = false;
                }
                var currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (currentPage != null)
                {
#if !WINDOWS
                    var snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Colors.DarkRed,
                        TextColor = Colors.White,
                        ActionButtonTextColor = Colors.White,
                        CornerRadius = 20,
                        Font = Font.SystemFontOfSize(14)
                    };
                    var snackbar = Snackbar.Make(
                   "Tus bolas no son de igual color!",
                   () => Console.WriteLine("Ok"), // Acción
                   "Ok",
                   TimeSpan.FromSeconds(.5), snackbarOptions);
                    await snackbar.Show();
#endif
                }
            }
            else if (bolasSeleccionadasCount)
            {
                bolasSeleccionadas.Clear();
                for (int i = 0; i < bolas.Count; i++)
                {
                    bolas[i].seleccionada = false;
                }
                var currentPage = Application.Current?.Windows.FirstOrDefault()?.Page;
                if (currentPage != null)
                {

#if !WINDOWS
                    //await currentPage.DisplayAlert("", "OK");
                    var snackbarOptions = new SnackbarOptions
                    {
                        BackgroundColor = Colors.DarkRed,
                        TextColor = Colors.White,
                        ActionButtonTextColor = Colors.White,
                        CornerRadius = 20,
                        Font = Font.SystemFontOfSize(14)
                    };

                    var snackbar = Snackbar.Make(
                        "Ya has consumido estas bolas!",
                        () => Console.WriteLine("Ok"),
                        "Ok",
                        TimeSpan.FromSeconds(0.5),
                        snackbarOptions);

                    await snackbar.Show();
#endif
                }
            }
        }

        public void renderizarBolasAleagorias()
        {
            Random random = new Random();
            bolas = new List<Bola>();

            for (int i = 0; i < 35; i++)
            {
                int numeroAleatorio = random.Next(1, 6);
                Bola bola = new Bola();
                bola.posicion = i;
                bola.seleccionada = false;

                switch (numeroAleatorio)
                {
                    case 1:
                        bola.color = Colors.IndianRed;
                        bola.imagen = "tigerhead.png";
                        break;
                    case 2:
                        bola.color = Colors.DarkSeaGreen;
                        bola.imagen = "empty.png";
                        break;
                    case 3:
                        bola.color = Colors.CornflowerBlue;
                        bola.imagen = "mementomori.png";
                        break;
                    case 4:
                        bola.color = Colors.Grey;
                        bola.imagen = "empty.png";
                        break;
                    default:
                        bola.color = Colors.Orange;
                        bola.imagen = "tigerhead.png";
                        break;
                }

                bolas.Add(bola);
            }
        }

        public void DeterminarGanador()
        {
            if (hpEnemy <= 0)
            {
                isGameOver = true;
                DetenerAnimacion();
                App.Current.MainPage.DisplayAlert("¡Victoria!", "Has derrotado al enemigo.", "OK");
                App.Current.MainPage.Navigation.PopAsync();
            }
            else if (hpPersonaje <= 0)
            {
                isGameOver = true;
                DetenerAnimacion();
                App.Current.MainPage.DisplayAlert("¡Derrota!", "Tu personaje ha sido derrotado.", "OK");
                App.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}