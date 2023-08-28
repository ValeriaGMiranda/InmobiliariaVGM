using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioInquilino
{
 protected readonly string connectionString;

    public RepositorioInquilino()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Inquilino> ObtenerInquilinos()
    { 
        var res = new List<Inquilino>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Inquilino,Apellido,Nombre,Dni,Telefono FROM inquilinos"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Inquilino
                        {
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Dni = reader.GetString("Dni"),
                            Telefono  = reader.GetString("Telefono"),                                            
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Inquilino ObtenerUnInquilino(int id)
    {
        var res = new Inquilino();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Inquilino,Apellido,Nombre,Dni,Telefono
            FROM inquilinos
            WHERE Id_Inquilino = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Inquilino
                        {
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Dni = reader.GetString("Dni"),
                            Telefono  = reader.GetString("Telefono"),                                  
                        });
                    }
                }
                conn.Close();
            }
        }
        return res; 
    }

    public int CrearInquilino(Inquilino inquilino)
    { 
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO inquilinos(Apellido,Nombre,Dni,Telefono)
            VALUES (@Apellido,@Nombre,@Dni,@Telefono);
            SELECT LAST_INSERT_ID()";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
                cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                inquilino.Id_Inquilino = res;
                conn.Close();
            }
            return res;
        }

    }

    public int EditarInquilino( Inquilino inquilino)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE Inquilinos
            SET Apellido=@Apellido, Nombre=@Nombre, Dni=@Dni, Telefono=@Telefono 
            WHERE Id_Inquilino = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", inquilino.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", inquilino.Nombre);
                cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
                cmd.Parameters.AddWithValue("@Telefono", inquilino.Telefono);
                cmd.Parameters.AddWithValue("@id", inquilino.Id_Inquilino);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarInquilino(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Inquilinos
            WHERE Id_Inquilino = @id";

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

