using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inmobiliariaVGM.Models;

public class Tipo
{
    [Key]
    public int Id_Tipo { get; set; } // ? = permite valores nulos
    [Required, Display(Name = "Tipo de Inmmueble")]
    public string? Nombre { get; set; }
    
}

