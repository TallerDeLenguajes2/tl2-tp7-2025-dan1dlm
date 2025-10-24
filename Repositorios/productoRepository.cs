using Microsoft.Data.Sqlite;

public class ProductoRepository
{
    string connectionString = "Data Source=db/Tienda.db";
    public List<Producto> GetProductos()
    {
        string queryString = "SELECT idProducto, Descripcion, Precio FROM Productos";
        List<Producto> productos = new List<Producto>();
        using (SqliteConnection connection = new SqliteConnection(connectionString))
        {
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Producto producto = new Producto
                    {
                        IdProducto = reader.GetInt32(0),               // Columna 0: idProducto
                        Descripcion = reader.GetString(1),              // Columna 1: Descripcion
                        Precio = reader.GetDecimal(2)
                    };                  // Columna 2: Precio

                    productos.Add(producto);
                }
            }
            connection.Close();
        }

        return productos;
    }

    public Producto GetProducto(int idProducto)
    {
      using var conexion = new SqliteConnection(cadenaConexion);
      conexion.Open();

      string sql = "SELECT idProducto, Descripcion, Precio FROM Productos WHERE idProducto = @idProducto";
      using var comando = new SqliteCommand(sql, conexion);
      comando.Parameters.Add(new SqliteParameter("@idProducto", idProducto));

      using var lector = comando.ExecuteReader();

      if(lector.Read()) 
    }


}