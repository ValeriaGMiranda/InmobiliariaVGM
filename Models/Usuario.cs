using System.ComponentModel.DataAnnotations;

namespace inmobiliariaVGM.Models;

public enum enRoles
{
    Administrador = 1,

}
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
    
    public string Avatar { get; set; }
    
    public IFormFile AvatarFile { get; set; }

    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";
}