using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PropertyChanged;

namespace EnumaElishApp.MVVM.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class CreateSalaViewModel
    {

        public ICommand popModal => new Command(() => App.Current.MainPage.Navigation.PopModalAsync());
        public string foto {  get; set; }
        public int contadorImagen { get; set; }
        public async Task CarrucelDeFotos()
        {
            while (true)
            {
                if (contadorImagen <= 0) contadorImagen = 1;

                foto = "foto" + contadorImagen + ".jpeg";
                await Task.Delay(3000);
                contadorImagen++;
                if (contadorImagen > 5) contadorImagen = 0; 
            }
        }

        public CreateSalaViewModel()
        {
            contadorImagen = 0;
            CarrucelDeFotos();
        }

    }
}
