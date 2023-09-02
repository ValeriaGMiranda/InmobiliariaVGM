using System.ComponentModel.DataAnnotations;

namespace inmobiliariaVGM.Models;

public class Propietario
{
    [Key]
    [Display(Name = "Código")]
    public int Id_Propietario { get; set; } // ? = permite valores nulos
    [Required]
    public string? Apellido { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Dni { get; set; }
    [Required,Display(Name = "Teléfono")]
    public string? Telefono { get; set; }
    [Required, Display(Name = "Email")]
    public string? Mail { get; set; }
}
