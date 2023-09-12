using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace inmobiliariaVGM.Models;

public class CambiarPassword
{
    public string Password { get; set; }
    public string Confirmacion { get; set; }
    
}

