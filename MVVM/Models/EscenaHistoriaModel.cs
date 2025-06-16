using PropertyChanged;

namespace EnumaElishApp.MVVM.Models
{
    [AddINotifyPropertyChangedInterface]
    public class EscenaHistoriaModel
    {
        /// <summary>
        /// Ruta de la imagen de fondo para esta escena
        /// </summary>
        public string Imagen { get; set; }

        /// <summary>
        /// Nombre del narrador (opcional)
        /// </summary>
        public string Narrador { get; set; }

        /// <summary>
        /// Texto principal de la escena
        /// </summary>
        public string Texto { get; set; }

        /// <summary>
        /// Indica si esta escena tiene un narrador específico
        /// </summary>
        public bool TieneNarrador => !string.IsNullOrEmpty(Narrador);

        /// <summary>
        /// Duración en segundos para auto-avance (opcional, 0 = manual)
        /// </summary>
        public double DuracionAutoAvance { get; set; } = 0;

        /// <summary>
        /// Efecto de transición para esta escena
        /// </summary>
        public TipoTransicion Transicion { get; set; } = TipoTransicion.Deslizar;

        /// <summary>
        /// Sonido ambiente para esta escena (opcional)
        /// </summary>
        public string SonidoAmbiente { get; set; }

        /// <summary>
        /// Efectos especiales para el texto (opcional)
        /// </summary>
        public EfectoTexto EfectoTexto { get; set; } = EfectoTexto.Ninguno;
    }

    public enum TipoTransicion
    {
        Deslizar,
        Desvanecer,
        Zoom,
        Ninguna
    }

    public enum EfectoTexto
    {
        Ninguno,
        MaquinaDeEscribir,
        Resplandor,
        Temblar
    }
}