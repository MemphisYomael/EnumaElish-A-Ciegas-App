using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumaElishApp.MVVM.Models
{
    public class NivelModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string ImagenFondo { get; set; }
        public int NumeroCapitulo { get; set; }
        public string NombreCapitulo { get; set; }
        public int NumeroNivel { get; set; }
        public string Dificultad { get; set; }
        public TimeSpan TiempoEstimado { get; set; }
        public List<string> Objetivos { get; set; }
        public bool Desbloqueado { get; set; }
        public bool Completado { get; set; }
        public int Estrellas { get; set; }

        public NivelModel()
        {
            Objetivos = new List<string>();
        }
    }
}
