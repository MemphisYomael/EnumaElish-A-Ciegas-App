using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumaElishApp.MVVM.Models
{
    public class CapituloModel
    {
        public int NumeroCapitulo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public List<NivelModel> Niveles { get; set; }
        public Color ColorTema { get; set; }
        public Color ColorSecundario { get; set; }

        public CapituloModel()
        {
            Niveles = new List<NivelModel>();
        }
    }
}
