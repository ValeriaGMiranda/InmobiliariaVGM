using System.ComponentModel.DataAnnotations;

namespace inmobiliariaVGM.Models;

public class Usuario
{
    [Key]
    [Display(Name = "Código")]
    public int Id_Usuario { get; set; } // ? = permite valores nulos

    [Required]
    public string Apellido { get; set; }

    [Required]
    public string Nombre { get; set; }

    [Required,Display(Name = "Email")]
    public string Mail { get; set; }

    [Required,DataType(DataType.Password)]
    public string Password { get; set; }

    [Required]
    public int Rol { get; set; }

    [Required]
    public string Avatar { get; set; }

    [Required]
    public IFormFile AvatarFile { get; set; }
}