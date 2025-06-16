using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using EnumaElishApp.MVVM.Views.C_;
using EnumaElishApp.MVVM.Models;
using PropertyChanged;
using Microsoft.Maui.Controls;

namespace EnumaElishApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CarrucelNivelesHistoriaViewModel
    {
        public ObservableCollection<CapituloModel> Capitulos { get; set; }
        public ICommand NavegarGameHistoria { get; set; }
        public ICommand MostrarInfoNivel { get; set; }

        public CarrucelNivelesHistoriaViewModel()
        {
            Capitulos = new ObservableCollection<CapituloModel>();

            NavegarGameHistoria = new Command<NivelModel>((nivel) =>
            {
                if (nivel?.Desbloqueado == true)
                {
                    App.Current.MainPage.Navigation.PushAsync(new GameHistoria());
                }
            });

            MostrarInfoNivel = new Command<NivelModel>(async (nivel) =>
            {
                await MostrarDetallesNivel(nivel);
            });

            CargarNiveles();
        }

        private void CargarNiveles()
        {
            var capitulos = new List<CapituloModel>
            {
                new CapituloModel
                {
                    NumeroCapitulo = 1,
                    Nombre = "El Despertar",
                    Descripcion = "Los primeros pasos en el mundo de Enuma Elish",
                    ColorTema = Color.FromArgb("#6366f1"),
                    ColorSecundario = Color.FromArgb("#4f46e5"),
                    Niveles = new List<NivelModel>
                    {
                        new NivelModel
                        {
                            Id = 1,
                            Titulo = "El Primer Amanecer",
                            Descripcion = "Descubre los secretos del mundo ancestral mientras exploras las ruinas del primer templo.",
                            ImagenFondo = "escenario1.jpeg",
                            NumeroCapitulo = 1,
                            NombreCapitulo = "El Despertar",
                            NumeroNivel = 1,
                            Dificultad = "Fácil",
                            TiempoEstimado = TimeSpan.FromMinutes(15),
                            Objetivos = new List<string> { "Explora el templo", "Encuentra la primera reliquia", "Resuelve el enigma básico" },
                            Desbloqueado = true,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 2,
                            Titulo = "Ecos del Pasado",
                            Descripcion = "Los susurros antiguos te guían a través de los corredores olvidados llenos de trampas y misterios.",
                            ImagenFondo = "escenario2.jpeg",
                            NumeroCapitulo = 1,
                            NombreCapitulo = "El Despertar",
                            NumeroNivel = 2,
                            Dificultad = "Fácil",
                            TiempoEstimado = TimeSpan.FromMinutes(20),
                            Objetivos = new List<string> { "Evita las trampas", "Descifra los jeroglíficos", "Encuentra la salida secreta" },
                            Desbloqueado = true,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 3,
                            Titulo = "Guardianes de Piedra",
                            Descripcion = "Enfréntate a los antiguos guardianes que protegen los secretos del templo desde hace milenios.",
                            ImagenFondo = "escenario3.jpeg",
                            NumeroCapitulo = 1,
                            NombreCapitulo = "El Despertar",
                            NumeroNivel = 3,
                            Dificultad = "Medio",
                            TiempoEstimado = TimeSpan.FromMinutes(25),
                            Objetivos = new List<string> { "Derrota a los guardianes", "Activa los mecanismos", "Obtén la gema de poder" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 4,
                            Titulo = "El Santuario Perdido",
                            Descripcion = "Descubre el santuario oculto donde yacen los primeros escritos de la creación.",
                            ImagenFondo = "escenario4.jpeg",
                            NumeroCapitulo = 1,
                            NombreCapitulo = "El Despertar",
                            NumeroNivel = 4,
                            Dificultad = "Medio",
                            TiempoEstimado = TimeSpan.FromMinutes(30),
                            Objetivos = new List<string> { "Encuentra el santuario", "Lee los escritos antiguos", "Desbloquea el portal" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 5,
                            Titulo = "Ritual de Iniciación",
                            Descripcion = "Completa el ritual ancestral para obtener el poder necesario para continuar tu aventura.",
                            ImagenFondo = "escenario5.jpeg",
                            NumeroCapitulo = 1,
                            NombreCapitulo = "El Despertar",
                            NumeroNivel = 5,
                            Dificultad = "Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(45),
                            Objetivos = new List<string> { "Reúne los elementos", "Realiza el ritual", "Absorbe el poder ancestral" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        }
                    }
                },
                new CapituloModel
                {
                    NumeroCapitulo = 2,
                    Nombre = "Tierras Prohibidas",
                    Descripcion = "Aventúrate en territorios peligrosos donde pocos han sobrevivido",
                    ColorTema = Color.FromArgb("#dc2626"),
                    ColorSecundario = Color.FromArgb("#b91c1c"),
                    Niveles = new List<NivelModel>
                    {
                        new NivelModel
                        {
                            Id = 6,
                            Titulo = "Valle de las Sombras",
                            Descripcion = "Atraviesa el misterioso valle donde las sombras cobran vida y acechan a los viajeros.",
                            ImagenFondo = "escenario6.jpeg",
                            NumeroCapitulo = 2,
                            NombreCapitulo = "Tierras Prohibidas",
                            NumeroNivel = 1,
                            Dificultad = "Medio",
                            TiempoEstimado = TimeSpan.FromMinutes(35),
                            Objetivos = new List<string> { "Sobrevive a las sombras", "Encuentra la luz sagrada", "Cruza el valle" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 7,
                            Titulo = "Fortaleza Abandonada",
                            Descripcion = "Explora la fortaleza que una vez protegió el reino, ahora habitada por criaturas sobrenaturales.",
                            ImagenFondo = "escenario7.jpeg",
                            NumeroCapitulo = 2,
                            NombreCapitulo = "Tierras Prohibidas",
                            NumeroNivel = 2,
                            Dificultad = "Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(40),
                            Objetivos = new List<string> { "Infiltra la fortaleza", "Derrota al comandante espectral", "Recupera las armas ancestrales" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 8,
                            Titulo = "Bosque Maldito",
                            Descripcion = "Navega por el bosque donde la naturaleza misma se ha vuelto hostil y corrompida.",
                            ImagenFondo = "escenario8.jpeg",
                            NumeroCapitulo = 2,
                            NombreCapitulo = "Tierras Prohibidas",
                            NumeroNivel = 3,
                            Dificultad = "Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(50),
                            Objetivos = new List<string> { "Purifica los árboles corrompidos", "Encuentra el corazón del bosque", "Restaura el equilibrio natural" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 9,
                            Titulo = "Laberinto de Cristal",
                            Descripcion = "Resuelve los enigmas del laberinto de cristal que refleja tus peores pesadillas.",
                            ImagenFondo = "escenario9.jpeg",
                            NumeroCapitulo = 2,
                            NombreCapitulo = "Tierras Prohibidas",
                            NumeroNivel = 4,
                            Dificultad = "Muy Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(60),
                            Objetivos = new List<string> { "Navega el laberinto", "Rompe las ilusiones", "Encuentra la salida verdadera" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 10,
                            Titulo = "El Dragón Dormido",
                            Descripcion = "Enfréntate al antiguo dragón guardián que ha estado durmiendo durante mil años.",
                            ImagenFondo = "escenario10.jpeg",
                            NumeroCapitulo = 2,
                            NombreCapitulo = "Tierras Prohibidas",
                            NumeroNivel = 5,
                            Dificultad = "Muy Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(75),
                            Objetivos = new List<string> { "Despierta al dragón", "Sobrevive a su furia", "Gana su respeto", "Obtén su bendición" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        }
                    }
                },
                new CapituloModel
                {
                    NumeroCapitulo = 3,
                    Nombre = "Reino de los Elementos",
                    Descripcion = "Domina los poderes elementales en tierras donde la magia es ley",
                    ColorTema = Color.FromArgb("#059669"),
                    ColorSecundario = Color.FromArgb("#047857"),
                    Niveles = new List<NivelModel>
                    {
                        new NivelModel
                        {
                            Id = 11,
                            Titulo = "Templo del Fuego",
                            Descripcion = "Controla las llamas sagradas y despierta tu poder sobre el elemento fuego.",
                            ImagenFondo = "escenario11.jpeg",
                            NumeroCapitulo = 3,
                            NombreCapitulo = "Reino de los Elementos",
                            NumeroNivel = 1,
                            Dificultad = "Medio",
                            TiempoEstimado = TimeSpan.FromMinutes(40),
                            Objetivos = new List<string> { "Enciende los braseros sagrados", "Domina las llamas", "Obtén el poder del fuego" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 12,
                            Titulo = "Cavernas de Hielo",
                            Descripcion = "Sobrevive al frío eterno y aprende a moldear el hielo con tu voluntad.",
                            ImagenFondo = "escenario12.jpeg",
                            NumeroCapitulo = 3,
                            NombreCapitulo = "Reino de los Elementos",
                            NumeroNivel = 2,
                            Dificultad = "Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(45),
                            Objetivos = new List<string> { "Resiste el frío glacial", "Crea puentes de hielo", "Despierta al espíritu del hielo" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 13,
                            Titulo = "Tormenta Eterna",
                            Descripcion = "Vuela a través de la tormenta perpetua y canaliza el poder del rayo.",
                            ImagenFondo = "escenario13.jpeg",
                            NumeroCapitulo = 3,
                            NombreCapitulo = "Reino de los Elementos",
                            NumeroNivel = 3,
                            Dificultad = "Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(50),
                            Objetivos = new List<string> { "Vuela entre los rayos", "Canaliza la electricidad", "Calma la tormenta" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 14,
                            Titulo = "Abismo Oceánico",
                            Descripcion = "Desciende a las profundidades del océano y aprende los secretos del agua.",
                            ImagenFondo = "escenario14.jpeg",
                            NumeroCapitulo = 3,
                            NombreCapitulo = "Reino de los Elementos",
                            NumeroNivel = 4,
                            Dificultad = "Muy Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(55),
                            Objetivos = new List<string> { "Respira bajo el agua", "Habla con las criaturas marinas", "Encuentra la perla de la sabiduría" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 15,
                            Titulo = "Convergencia Elemental",
                            Descripcion = "Une todos los elementos en una sinfonía de poder y equilibrio perfecto.",
                            ImagenFondo = "escenario15.jpeg",
                            NumeroCapitulo = 3,
                            NombreCapitulo = "Reino de los Elementos",
                            NumeroNivel = 5,
                            Dificultad = "Extremo",
                            TiempoEstimado = TimeSpan.FromMinutes(90),
                            Objetivos = new List<string> { "Equilibra los cuatro elementos", "Crea la convergencia perfecta", "Transciende los límites elementales" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        }
                    }
                },
                new CapituloModel
                {
                    NumeroCapitulo = 4,
                    Nombre = "El Final Eterno",
                    Descripcion = "La batalla final donde se decidirá el destino de todos los mundos",
                    ColorTema = Color.FromArgb("#7c3aed"),
                    ColorSecundario = Color.FromArgb("#6d28d9"),
                    Niveles = new List<NivelModel>
                    {
                        new NivelModel
                        {
                            Id = 16,
                            Titulo = "Puertas del Destino",
                            Descripcion = "Atraviesa las puertas dimensionales que conectan todos los mundos posibles.",
                            ImagenFondo = "escenario16.jpeg",
                            NumeroCapitulo = 4,
                            NombreCapitulo = "El Final Eterno",
                            NumeroNivel = 1,
                            Dificultad = "Muy Difícil",
                            TiempoEstimado = TimeSpan.FromMinutes(60),
                            Objetivos = new List<string> { "Abre las puertas dimensionales", "Navega entre realidades", "Elige tu destino" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 17,
                            Titulo = "Ejército de las Sombras",
                            Descripcion = "Lidera la batalla épica contra las fuerzas de la oscuridad eterna.",
                            ImagenFondo = "escenario17.jpeg",
                            NumeroCapitulo = 4,
                            NombreCapitulo = "El Final Eterno",
                            NumeroNivel = 2,
                            Dificultad = "Extremo",
                            TiempoEstimado = TimeSpan.FromMinutes(80),
                            Objetivos = new List<string> { "Reúne a los aliados", "Batalla contra las sombras", "Protege la luz sagrada" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 18,
                            Titulo = "Torre del Juicio",
                            Descripcion = "Asciende la torre infinita donde serás juzgado por todas tus acciones.",
                            ImagenFondo = "escenario18.jpeg",
                            NumeroCapitulo = 4,
                            NombreCapitulo = "El Final Eterno",
                            NumeroNivel = 3,
                            Dificultad = "Extremo",
                            TiempoEstimado = TimeSpan.FromMinutes(100),
                            Objetivos = new List<string> { "Asciende la torre", "Enfrenta tu pasado", "Acepta el juicio final" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 19,
                            Titulo = "El Creador Primordial",
                            Descripcion = "Enfréntate al mismísimo creador de todas las cosas en un duelo que trasciende la realidad.",
                            ImagenFondo = "escenario19.jpeg",
                            NumeroCapitulo = 4,
                            NombreCapitulo = "El Final Eterno",
                            NumeroNivel = 4,
                            Dificultad = "Imposible",
                            TiempoEstimado = TimeSpan.FromMinutes(120),
                            Objetivos = new List<string> { "Confronta al Creador", "Trasciende la mortalidad", "Redefine la existencia" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        },
                        new NivelModel
                        {
                            Id = 20,
                            Titulo = "Genesis Infinito",
                            Descripcion = "Crea un nuevo universo y conviértete en el arquitecto de una nueva realidad.",
                            ImagenFondo = "escenario20.jpeg",
                            NumeroCapitulo = 4,
                            NombreCapitulo = "El Final Eterno",
                            NumeroNivel = 5,
                            Dificultad = "Divino",
                            TiempoEstimado = TimeSpan.FromMinutes(150),
                            Objetivos = new List<string> { "Reúne la esencia primordial", "Diseña las nuevas leyes", "Crea tu universo perfecto", "Trasciende hacia la divinidad" },
                            Desbloqueado = false,
                            Completado = false,
                            Estrellas = 0
                        }
                    }
                }
            };

            foreach (var capitulo in capitulos)
            {
                Capitulos.Add(capitulo);
            }
        }

        private async Task MostrarDetallesNivel(NivelModel nivel)
        {
            var objetivosTexto = string.Join("\n• ", nivel.Objetivos);
            var mensaje = $"📖 {nivel.Titulo}\n\n" +
                         $"📚 Capítulo: {nivel.NumeroCapitulo} - {nivel.NombreCapitulo}\n" +
                         $"🎯 Nivel: {nivel.NumeroNivel}\n" +
                         $"⚡ Dificultad: {nivel.Dificultad}\n" +
                         $"⏱️ Tiempo estimado: {nivel.TiempoEstimado.TotalMinutes} min\n" +
                         $"⭐ Estrellas: {nivel.Estrellas}/3\n\n" +
                         $"📝 Descripción:\n{nivel.Descripcion}\n\n" +
                         $"🎯 Objetivos:\n• {objetivosTexto}\n\n" +
                         $"🔒 Estado: {(nivel.Desbloqueado ? "Desbloqueado" : "Bloqueado")}\n" +
                         $"✅ Completado: {(nivel.Completado ? "Sí" : "No")}";

            await App.Current.MainPage.DisplayAlert("Información del Nivel", mensaje, "Cerrar");
        }
    }
}