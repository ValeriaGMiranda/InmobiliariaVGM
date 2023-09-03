using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inmobiliariaVGM.Models;

public class Uso
{
    [Key]
    public int Id_Uso { get; set; } // ? = permite valores nulos
    [Required, Display(Name = "Uso del Inmueble")]
    public string? Nombre { get; set; }
    
}

