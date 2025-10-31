using Microsoft.Data.Sqlite;
using System.Collections.Generic;

public class PresupuestoDetalleRepository
{
    string connectionString = "Data Source=db/Tienda.db";

    public List<PresupuestoDetalle> GetDetallesPorPresupuesto(int idPresupuesto)
    {
        string query = @"SELECT pd.idProducto, p.Descripcion, p.Precio, pd.Cantidad
                         FROM PresupuestoDetalle pd
                         INNER JOIN Productos p ON pd.idProducto = p.idProducto
                         WHERE pd.idPresupuesto = @idPresupuesto";

        var detalles = new List<PresupuestoDetalle>();

        using (var conexion = new SqliteConnection(connectionString))
        {
            conexion.Open();
            using (var comando = new SqliteCommand(query, conexion))
            {
                comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));

                using (var lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        var producto = new Producto
                        {
                            IdProducto = lector.GetInt32(0),
                            Descripcion = lector.GetString(1),
                            Precio = lector.GetDecimal(2)
                        };

                        var detalle = new PresupuestoDetalle
                        {
                            Producto = producto,
                            Cantidad = lector.GetInt32(3)
                        };

                        detalles.Add(detalle);
                    }
                }
            }
        }

        return detalles;
    }

    public void InsertarDetalle(int idPresupuesto, PresupuestoDetalle detalle)
    {
        using var conexion = new SqliteConnection(connectionString);
        conexion.Open();

        string sql = @"INSERT INTO PresupuestoDetalle (idPresupuesto, idProducto, Cantidad)
                       VALUES (@idPresupuesto, @idProducto, @Cantidad)";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@idProducto", detalle.Producto.IdProducto));
        comando.Parameters.Add(new SqliteParameter("@Cantidad", detalle.Cantidad));

        comando.ExecuteNonQuery();
    }

    public void EliminarDetallesDePresupuesto(int idPresupuesto)
    {
        using var conexion = new SqliteConnection(connectionString);
        conexion.Open();

        string sql = "DELETE FROM PresupuestoDetalle WHERE idPresupuesto = @idPresupuesto";
        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        comando.ExecuteNonQuery();
    }

    public void ActualizarCantidad(int idPresupuesto, int idProducto, int nuevaCantidad)
    {
        using var conexion = new SqliteConnection(connectionString);
        conexion.Open();

        string sql = @"UPDATE PresupuestoDetalle 
                       SET Cantidad = @Cantidad 
                       WHERE idPresupuesto = @idPresupuesto AND idProducto = @idProducto";

        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.Add(new SqliteParameter("@Cantidad", nuevaCantidad));
        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@idProducto", idProducto));

        comando.ExecuteNonQuery();
    }
}
