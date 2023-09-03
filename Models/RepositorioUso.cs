using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioUso
{
 protected readonly string connectionString;

    public RepositorioUso()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Uso> ObtenerUsos()
    { 
        var res = new List<Uso>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Uso,Nombre FROM usos"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Uso
                        {
                            Id_Uso = reader.GetInt32("Id_Uso"),
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

