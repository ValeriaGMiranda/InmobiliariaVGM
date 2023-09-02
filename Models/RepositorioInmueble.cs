using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioInmueble
{
 protected readonly string connectionString;

    public RepositorioInmueble()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Inmueble> ObtenerInmuebles()
    { 
        var res = new List<Inmueble>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Inmueble,Direccion,Id_Uso,Id_Tipo,Ambientes,Latitud,Longitud,Precio,Activo,Id_Propietario 
            FROM inmuebles"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Inmueble
                        {
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Direccion = reader.GetString("Direccion"),
                            Id_Uso = reader.GetInt32("Id_Uso"),
                            Id_Tipo = reader.GetInt32("Id_Tipo"),
                            Ambientes  = reader.GetInt32("Ambientes"),
                            Latitud = reader.GetDecimal("Latitud"),  
                            Longitud = reader.GetDecimal("Longitud"),
                            Precio = reader.GetDecimal("Precio"),
                            Activo = reader.GetBoolean("Activo"),
                            Id_Propietario = reader.GetInt32("Id_Propietario"),                
                            
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Inmueble ObtenerUnInmueble(int id)
    {
        var res = new Inmueble();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Inmueble,Direccion,Id_Uso,Id_Tipo,Ambientes,Latitud,Longitud,Precio,Activo,Id_Propietario 
            FROM inmuebles
            WHERE Id_Inmueble = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Inmueble
                        {
                            Id_Inmueble = reader.GetInt32("Id_Inmueble"),
                            Direccion = reader.GetString("Direccion"),
                            Id_Uso = reader.GetInt32("Id_Uso"),
                            Id_Tipo = reader.GetInt32("Id_Tipo"),
                            Ambientes  = reader.GetInt32("Ambientes"),
                            Latitud = reader.GetDecimal("Latitud"),  
                            Longitud = reader.GetDecimal("Longitud"),
                            Precio = reader.GetDecimal("Precio"),
                            Activo = reader.GetBoolean("Activo"),
                            Id_Propietario = reader.GetInt32("Id_Propietario"),                      
                            
                        });
                    }
                }
                conn.Close();
            }
        }
        return res; 
    }

    public int CrearInmueble(Inmueble inmueble)
    { 
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO inmuebles(Direccion,Id_Uso,Id_Tipo,Ambientes,Latitud,Longitud,Precio,Activo,Id_Propietario)
            VALUES (@Direccion,@Id_Uso,@Id_Tipo,@Ambientes,@Latitud,@Longitud,@Precio,@Activo,@Id_Propietario);
            SELECT LAST_INSERT_ID()";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                cmd.Parameters.AddWithValue("@Id_Uso", inmueble.Id_Uso);
                cmd.Parameters.AddWithValue("@Id_Tipo", inmueble.Id_Tipo);
                cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
                cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
                cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
                cmd.Parameters.AddWithValue("@Id_Propietario", inmueble.Id_Propietario);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                inmueble.Id_Inmueble= res;
                conn.Close();
            }
            return res;
        }

    }

    public int EditarInmueble(Inmueble inmueble)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE Inmuebles
            SET Direccion=@Direccion,Id_Uso=@Id_Uso,Id_Tipo=@Id_Tipo,Ambientes=@Ambientes,Latitud=@Latitud,Longitud=@Longitud,
            Precio=@Precio,Activo=@Activo,Id_Propietario=@Id_Propietario 
            WHERE Id_Inmueble = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@Direccion", inmueble.Direccion);
                cmd.Parameters.AddWithValue("@Id_Uso", inmueble.Id_Uso);
                cmd.Parameters.AddWithValue("@Id_Tipo", inmueble.Id_Tipo);
                cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
                cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
                cmd.Parameters.AddWithValue("@Activo", inmueble.Activo);
                cmd.Parameters.AddWithValue("@Id_Propietario", inmueble.Id_Propietario);
                cmd.Parameters.AddWithValue("@id", inmueble.Id_Inmueble);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarInmueble(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Inmuebles
            WHERE Id_Inmueble = @id";

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

