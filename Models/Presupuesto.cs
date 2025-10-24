public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime FechaCreacion;
    List<PresupuestoDetalle> detalle;

    Presupuesto(int idPresupuesto, string nombreDestinatario, DateTime FechaCreacion, List<PresupuestoDetalle> detalle)
    {
        this.idPresupuesto = idPresupuesto;
        this.nombreDestinatario = nombreDestinatario;
        this.FechaCreacion = FechaCreacion;
        this.detalle = detalle;
    }

    //metodos

    public int montoPresupuesto()
    {
        int total = 0;
        foreach (var p in detalle)
        {
            total += p.Cantidad * p.Producto.Precio;
        }

        return total;
    }

    public double montoPresupuestoConIVA()
    {
        return (montoPresupuesto() * (1.21));
    }

    public int cantidadProductos()
    {
        int total = 0;
        foreach (var d in detalle)
        {
            total += d.Cantidad;
        }

        return total;
    }
}
