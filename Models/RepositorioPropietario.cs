using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioPropietario
{
 protected readonly string connectionString;

    public RepositorioPropietario()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Propietario> ObtenerPropietarios() // funcion de ejemplo
    { 
        var res = new List<Propietario>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Propietario,Apellido,Nombre,Dni,Telefono,Mail FROM propietarios"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn)){

                conn.Open();

                using(MySqlDataReader reader = cmd.ExecuteReader())
                {

                    while(reader.Read())
                    {

                        res.Add(new Propietario
                        {

                            Id_Propietario = reader.GetInt32("Id_Propietario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Dni = reader.GetString("Dni"),
                            Telefono  = reader.GetString("Telefono"),
                            Mail = reader.GetString("Mail"),                      
                            
                        });
                    }
                }
                conn.Close();
            }

        }

        return res;
    }

    public int CrearPropietario(Propietario propietario)//Alta Propietario
    {
        
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO propietarios(Apellido,Nombre,Dni,Telefono,Mail)
            VALUES (@Apellido,@Nombre,@Dni,@Telefono,@Mail);
            SELECT LAST_INSERT_ID()";


            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                cmd.Parameters.AddWithValue("@Dni", propietario.Dni);
                cmd.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                cmd.Parameters.AddWithValue("@Mail", propietario.Mail);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                propietario.Id_Propietario = res;
                conn.Close();
            }
            return res;
        }

    }
}

