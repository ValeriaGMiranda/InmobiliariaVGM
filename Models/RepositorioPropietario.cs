using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioPropietario
{
 protected readonly string connectionString;

    public RepositorioPropietario()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Propietario> ObtenerPropietarios()
    { 
        var res = new List<Propietario>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Propietario,Apellido,Nombre,Dni,Telefono,Mail FROM propietarios"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
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

    public Propietario ObtenerUnPropietario(int id)
    {
        var res = new Propietario();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Propietario,Apellido,Nombre,Dni,Telefono,Mail 
            FROM propietarios
            WHERE Id_Propietario = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Propietario
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

    public int CrearPropietario(Propietario propietario)
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

    public int EditarPropietario(Propietario propietario)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE Propietarios
            SET Apellido=@Apellido, Nombre=@Nombre, Dni=@Dni, Telefono=@Telefono, Mail=@Mail 
            WHERE Id_Propietario = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                cmd.Parameters.AddWithValue("@Dni", propietario.Dni);
                cmd.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                cmd.Parameters.AddWithValue("@Mail", propietario.Mail);
                cmd.Parameters.AddWithValue("@id", propietario.Id_Propietario);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarPropietario(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Propietarios
            WHERE Id_Propietario = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@id",id);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        return res;
    }
}

