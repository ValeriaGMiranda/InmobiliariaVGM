using System.ComponentModel.DataAnnotations;

namespace inmobiliariaVGM.Models;

public class Inquilino
{
    [Key]
    [Display(Name = "Código")]
    public int Id_Inquilino { get; set; } // ? = permite valores nulos
    [Required]
    public string? Apellido { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Dni { get; set; }
    [Required,Display(Name = "Teléfono")]
    public string? Telefono { get; set; }

}