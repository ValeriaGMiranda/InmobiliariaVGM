namespace inmobiliariaVGM.Models;

public class Propietario
{
    public int Id_Propietario { get; set; } // ? = permite valores nulos

    public string? Apellido { get; set; }

    public string? Nombre { get; set; }

    public string? Dni { get; set; }

    public string? Telefono { get; set; }

    public string? Mail { get; set; }
}
