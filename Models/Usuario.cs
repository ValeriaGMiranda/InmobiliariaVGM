using System.ComponentModel.DataAnnotations;

namespace inmobiliariaVGM.Models;

public enum enRoles
{
    Administrador = 1,
    Empleado = 2,

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
    
    public string? Avatar { get; set; }
    
    public IFormFile? AvatarFile { get; set; }

    public string RolNombre => Rol > 0 ? ((enRoles)Rol).ToString() : "";

    public static IDictionary<int, string> ObtenerRoles()
		{
			SortedDictionary<int, string> roles = new SortedDictionary<int, string>();
			Type tipoEnumRol = typeof(enRoles);
			foreach (var valor in Enum.GetValues(tipoEnumRol))
			{
				roles.Add((int)valor, Enum.GetName(tipoEnumRol, valor));
			}
			return roles;
		}
}