using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioUsuario
{
 protected readonly string connectionString;

    public RepositorioUsuario()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Usuario> ObtenerUsuarios()
    { 
        var res = new List<Usuario>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id_Usuario,Apellido,Nombre,Mail,Password,Rol,Avatar,AvatarFile FROM Usuarios"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Usuario
                        {
                            Id_Usuario = reader.GetInt32("Id_Usuario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Mail = reader.GetString("Mail"),
                            Password  = reader.GetString("Password"),
                            Rol = reader.GetInt32("Rol"), 
                            Avatar = reader.GetString("Avatar"),                    
                            
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Usuario ObtenerUnUsuario(int id)
    {
        var res = new Usuario();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"Id_Usuario,Apellido,Nombre,Mail,Password,Rol,Avatar,AvatarFile FROM Usuarios
            WHERE Id_Usuario = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Usuario
                        {
                            Id_Usuario = reader.GetInt32("Id_Usuario"),
                            Apellido = reader.GetString("Apellido"),
                            Nombre = reader.GetString("Nombre"),
                            Mail = reader.GetString("Mail"),
                            Password  = reader.GetString("Password"),
                            Rol = reader.GetInt32("Rol"), 
                            Avatar = reader.GetString("Avatar"),                     
                            
                        });
                    }
                }
                conn.Close();
            }
        }
        return res; 
    }

    public int CrearUsuario(Usuario usuario)
    { 
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO Usuarios(Apellido,Nombre,Mail,Password,Rol,Avatar,AvatarFile)
            VALUES (@Apellido,@Nombre,@Mail,@Password,@Rol,@Avatar,@AvatarFile);
            SELECT LAST_INSERT_ID()";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Mail", usuario.Mail);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                usuario.Id_Usuario = res;
                conn.Close();
            }
            return res;
        }

    }

    public int EditarUsuario(Usuario usuario)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE Usuarios
            SET Apellido=@Apellido,Nombre=@Nombre,Mail=@Mail,Password=@Password,Rol=@Rol,Avatar=@Avatar,AvatarFile=@AvatarFile 
            WHERE Id_Usuario = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                cmd.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                cmd.Parameters.AddWithValue("@Mail", usuario.Mail);
                cmd.Parameters.AddWithValue("@Password", usuario.Password);
                cmd.Parameters.AddWithValue("@Rol", usuario.Rol);
                cmd.Parameters.AddWithValue("@Avatar", usuario.Avatar);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarUsuario(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Usuarios
            WHERE Id_Usuario = @id";

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
