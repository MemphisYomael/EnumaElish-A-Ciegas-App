using System.Windows.Input;
using EnumaElishApp.MVVM.Models;
using EnumaElishApp.MVVM.Views.C_;
using EnumaElishApp.MVVM.Views.XAML;
using PropertyChanged;

namespace EnumaElishApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class HomeViewModel
    {
        public List<Usuario> personas { get; set; } = new List<Usuario>();
        public List<string> salas { get; set; } = new List<string> { "Sala 1", "Sala 2", "Sala 3", "Sala 4", "Sala 5", "Sala 6", "Sala 7", "Sala 8", "Sala 9" };

        CarrucelDeNivelesModoHistoria ModoHistoria;
        public Color colorInicial { get; set; }

        public string nombrePersona { get; set; }
        public char nombreLetraInicial { get; set; }

        public ICommand CrearSalaCommand => new Command(() => App.Current.MainPage.Navigation.PushModalAsync(new CreateSala()));
        public ICommand unirseASala => new Command(() => App.Current.MainPage.Navigation.PushModalAsync(new EntrarASala()));

        public ICommand nivelesCarrucel => new Command(() =>
        {
            App.Current.MainPage.Navigation.PushAsync(PageCache.CachedHeavyPage);
        }
        );
            
        public HomeViewModel()
        {

            Task.Run(() =>
            {
                PageCache.CachedHeavyPage = new CarrucelDeNivelesModoHistoria();
            });

            nombrePersona = "Memphis Yomael";
            nombreLetraInicial = nombrePersona[0];
            Color colorInicial = GetColorFromInitial(nombreLetraInicial);

            //personas.Add(new Usuario { Id = 1, nick = "Memphis Yomael", email = "memphisyomael@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Aqutan", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Jerico", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Rubi", email = "nunezmemphis@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Gilgamesh", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Artoria Pendragon", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Memphis Yomael", email = "memphisyomael@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Aqutan", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Jerico", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Rubi", email = "nunezmemphis@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Gilgamesh", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Artoria Pendragon", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Memphis Yomael", email = "memphisyomael@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Aqutan", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Jerico", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Rubi", email = "nunezmemphis@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Gilgamesh", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Artoria Pendragon", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Memphis Yomael", email = "memphisyomael@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Aqutan", email = "nunezmemphis@gmail.com", online = true });
            //personas.Add(new Usuario { Id = 1, nick = "Jerico", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Rubi", email = "nunezmemphis@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Gilgamesh", email = "memphisyomael@gmail.com", online = false });
            //personas.Add(new Usuario { Id = 1, nick = "Artoria Pendragon", email = "nunezmemphis@gmail.com", online = true });
            ModoHistoria = new CarrucelDeNivelesModoHistoria();

        }



        Color GetColorFromInitial(char initial)
        {
            int hash = initial % 16; // 16 colores base
            string[] colorsHex = new string[]
            {
        "f44336", "e91e63", "9c27b0", "673ab7",
        "3f51b5", "2196f3", "03a9f4", "00bcd4",
        "009688", "4caf50", "8bc34a", "cddc39",
        "ffeb3b", "ffc107", "ff9800", "ff5722"
            };

            return Color.FromArgb(colorsHex[hash]);
        }
    }

}