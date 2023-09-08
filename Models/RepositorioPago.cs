using MySql.Data.MySqlClient;

namespace inmobiliariaVGM.Models;

public class RepositorioPago
{
 protected readonly string connectionString;

    public RepositorioPago()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliaria;SslMode=none";
    }

    public List<Pago> ObtenerPagos()
    { 
        var res = new List<Pago>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM pagos p
            INNER JOIN contratos c ON p.Id_Contrato = c.Id_Contrato"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(
                            new Pago
                        {
                            Id_Pago = reader.GetInt32("Id_Pago"),
                            Fecha = reader.GetDateTime("Fecha"),
                            Importe = reader.GetDecimal("Importe"),
                            Id_Contrato  = reader.GetInt32("Id_Contrato"),

                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }


    public List<Pago> PagosPorContrato(int id)
    { 
        var res = new List<Pago>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM pagos p
            INNER JOIN contratos c ON p.Id_Contrato = c.Id_Contrato"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(
                            new Pago
                        {
                            Id_Pago = reader.GetInt32("Id_Pago"),
                            Fecha = reader.GetDateTime("Fecha"),
                            Importe = reader.GetDecimal("Importe"),
                            Id_Contrato  = reader.GetInt32("Id_Contrato"),

                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }


     public List<Pago> PagosContratoPorInquilino(int id)
    { 
        var res = new List<Pago>();

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"
            SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM pagos p
            INNER JOIN contratos c ON p.Id_Contrato = c.Id_Contrato
            WHERE c.Id_Contrato = @id"; 

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res.Add(
                            new Pago
                        {
                            Id_Pago = reader.GetInt32("Id_Pago"),
                            Fecha = reader.GetDateTime("Fecha"),
                            Importe = reader.GetDecimal("Importe"),
                            Id_Contrato  = reader.GetInt32("Id_Contrato"),

                        });
                    }
                }
                conn.Close();
            }
        }

        return res;
    }



    public Pago ObtenerUnPago(int id)
    {
        var res = new Pago();

          using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT p.Id_Pago,p.Fecha,p.Importe,p.Id_Contrato
            FROM pagos p
            INNER JOIN contratos c ON p.Id_Contrato = c.Id_Contrato
            WHERE Id_Pago = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                using(MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        res = (new Pago
                        {
                            Id_Pago = reader.GetInt32("Id_Pago"),
                            Fecha = reader.GetDateTime("Fecha"),
                            Importe = reader.GetDecimal("Importe"),
                            Id_Contrato  = reader.GetInt32("Id_Contrato"),                    
                        });
                    }
                }
                conn.Close();
            }
        }
        return res; 
    }

    public int CrearPago(Pago pago)
    { 
        var res = -1;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {

            var sql = @"INSERT INTO pagos(Fecha,Importe,Id_Contrato)
            VALUES (@Fecha,@Importe,@Id_Contrato);
            SELECT LAST_INSERT_ID()";

            using(MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
                cmd.Parameters.AddWithValue("@Importe", pago.Importe);
                cmd.Parameters.AddWithValue("@Id_Contrato", pago.Id_Contrato);

                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                pago.Id_Pago= res;
                conn.Close();
            }
            return res;
        }

    }

    public int EditarPago(Pago pago)
    {
        var res = -2;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE pagos
            SET Fecha=@Fecha,Importe=@Importe,Id_Contrato=@Id_Contrato
            WHERE Id_Pago = @id";

            using(MySqlCommand cmd = new MySqlCommand(sql,conn))
            {               
                cmd.Parameters.AddWithValue("@Fecha", pago.Fecha);
                cmd.Parameters.AddWithValue("@Importe", pago.Importe);
                cmd.Parameters.AddWithValue("@Id_Contrato", pago.Id_Contrato);
                cmd.Parameters.AddWithValue("@Id_Pago", pago.Id_Pago);
                conn.Open();
                res = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return res;
        }      
    }

    public int EliminarPago(int id)
    {
        var res = -3;

        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"DELETE FROM Pagos
            WHERE Id_Pago= @id";

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

