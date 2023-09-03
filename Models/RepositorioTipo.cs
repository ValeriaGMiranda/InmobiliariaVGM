using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioTipo
{
 protected readonly string connectionString;

    public RepositorioTipo()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Tipo> ObtenerTipos()
    { 
        var res = new List<Tipo>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Tipo,Nombre FROM tipos"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Tipo
                        {
                            Id_Tipo = reader.GetInt32("Id_Tipo"),
                            Nombre = reader.GetString("Nombre"),
                                                                       
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    
}

