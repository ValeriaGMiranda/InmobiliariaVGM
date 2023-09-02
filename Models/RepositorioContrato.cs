using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioContrato
{
 protected readonly string connectionString;

    public RepositorioContrato()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Contrato> ObtenerContratos()
    { 
        var res = new List<Contrato>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Contrato,Fecha_Inicio,Fecha_Fin,Monto,Id_Inmueble,Id_Inquilino
            FROM contratos"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble  = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),                 
                            
                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }

    public Contrato ObtenerUnContrato(int id)
    {
        var res = new Contrato();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT Id_Contrato,Fecha_Inicio,Fecha_Fin,Monto,Id_Inmueble,Id_Inquilino
            FROM contratos 
            WHERE Id_Contrato = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Contrato
                        {
                            Id_Contrato = reader.GetInt32("Id_Contrato"),
                            Fecha_Inicio = reader.GetDateTime("Fecha_Inicio"),
                            Fecha_Fin = reader.GetDateTime("Fecha_Fin"),
                            Monto = reader.GetDecimal("Monto"),
                            Id_Inmueble  = reader.GetInt32("Id_Inmueble"),
                            Id_Inquilino = reader.GetInt32("Id_Inquilino"),                     
                            
                        });
                    }
                }
                conn.Close();
            }
        }
        return res; 
    }

    public int CrearContrato(Contrato contrato)
    { 
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO contratos(Id_Contrato,Fecha_Inicio,Fecha_Fin,Monto,Id_Inmueble,Id_Inquilino)
            VALUES (@Id_Contrato,@Fecha_Inicio,@Fecha_Fin,@Monto,@Id_Inmueble,@Id_Inquilino);
            SELECT LAST_INSERT_ID()";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id_Contrato", contrato.Id_Contrato);
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                contrato.Id_Contrato= res;
                conn.Close();
            }
            return res;
        }

    }

    public int EditarContrato(Contrato contrato)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE contratos
            SET Fecha_Inicio=@Fecha_Inicio,Fecha_Fin=@Fecha_Fin,Monto=@Monto,Id_Inmueble=@Id_Inmueble,
            Id_Inquilino=@Id_Inquilino
            WHERE Id_Contrato = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {
                cmd.Parameters.AddWithValue("@Fecha_Inicio", contrato.Fecha_Inicio);
                cmd.Parameters.AddWithValue("@Fecha_Fin", contrato.Fecha_Fin);
                cmd.Parameters.AddWithValue("@Monto", contrato.Monto);
                cmd.Parameters.AddWithValue("@Id_Inmueble", contrato.Id_Inmueble);
                cmd.Parameters.AddWithValue("@Id_Inquilino", contrato.Id_Inquilino);
                cmd.Parameters.AddWithValue("@id", contrato.Id_Contrato);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarContrato(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Contratos
            WHERE Id_Contrato= @id";

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

