namespace inmobiliariaVGM.Models;

public class Inquilino
{
    public int Id_Inquilino { get; set; } // ? = permite valores nulos

    public string? Apellido { get; set; }

    public string? Nombre { get; set; }

    public string? Dni { get; set; }

    public string? Telefono { get; set; }

}