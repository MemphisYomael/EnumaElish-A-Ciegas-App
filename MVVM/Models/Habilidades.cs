using System.ComponentModel;
using PropertyChanged;

[AddINotifyPropertyChangedInterface]
public class Habilidades 
{
    public string? nombreAtaque { get; set; }
    public string? textoVozPersonaje { get; set; }
    public string? descripcionAtaque { get; set; }
    public string? imagenAtaque { get; set; }
    public string? sonidoAtaque { get; set; }
    public int dañoAtaque { get; set; }
    public int cargaAtaque { get; set; }
    public int cargaEsperadaAtaque { get; set; }
    public Color? Color { get; set; }

    public float PorcentajeCargado => cargaEsperadaAtaque == 0 ? 0 : (float)cargaAtaque / cargaEsperadaAtaque;

    public int posicion { get; set; }
}
