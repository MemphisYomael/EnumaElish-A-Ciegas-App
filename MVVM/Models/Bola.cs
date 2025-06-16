using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace EnumaElishApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]

    public class Bola
    {
        public Color? color { get; set; }
        public int posicion { get; set; }
        public bool seleccionada { get; set; }
        public string? imagen { get; set; }
        public bool consumida { get; set; } = false;

    }
}
