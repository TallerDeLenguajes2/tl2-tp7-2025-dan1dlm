using System.IO.Compression;

public class Producto{
    private int idProducto;
    private string descripcion;
    private decimal precio;

    public Producto(){}

    public int IdProducto{get => idProducto; set => idProducto = value;}
    public string Descripcion{get => descripcion; set => descripcion = value;}
    public decimal Precio{get => precio; set => precio = value;}

}