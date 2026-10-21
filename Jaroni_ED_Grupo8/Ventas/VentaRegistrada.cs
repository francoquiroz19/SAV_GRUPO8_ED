namespace Jaroni_ED_Grupo8;

// Conserva una copia independiente de los artículos de una venta confirmada.
public sealed class VentaRegistrada
{
    private readonly VentasListaCircular<VentasDetalle> articulos = new();

    public string Codigo { get; }
    public DateTime FechaHora { get; }
    public decimal Total { get; }
    public int CantidadTotal { get; }
    public decimal? PrecioUnitario { get; }
    public string Productos { get; }

    public VentaRegistrada(string codigo, DateTime fechaHora, VentasListaCircular<VentasDetalle> detalle)
    {
        VentasNodo<VentasDetalle>? primerNodo = detalle.ObtenerCabeza();
        if (primerNodo is null)
            throw new ArgumentException("La venta debe contener al menos un artículo.", nameof(detalle));

        Codigo = codigo;
        FechaHora = fechaHora;
        string nombres = "";
        decimal totalVenta = 0;
        int cantidadTotal = 0;
        decimal? precioUnitario = primerNodo.Dato.PrecioUnitario;
        VentasNodo<VentasDetalle> nodoActual = primerNodo;

        // Cada artículo confirmado recibe un nodo nuevo; limpiar el detalle no lo modifica.
        do
        {
            VentasDetalle articulo = nodoActual.Dato;
            VentasDetalle copia = new VentasDetalle(articulo.Codigo, articulo.Nombre,
                articulo.Cantidad, articulo.PrecioUnitario);
            articulos.InsertarAlFinal(copia);
            if (nombres.Length > 0)
            {
                nombres += ", ";
                precioUnitario = null;
            }
            nombres += articulo.Nombre;
            totalVenta += copia.Subtotal;
            if (cantidadTotal > int.MaxValue - copia.Cantidad)
                throw new OverflowException("La cantidad total es demasiado grande.");
            cantidadTotal += copia.Cantidad;
            nodoActual = nodoActual.Siguiente;
        } while (nodoActual != primerNodo);

        Total = totalVenta;
        CantidadTotal = cantidadTotal;
        PrecioUnitario = precioUnitario;
        Productos = nombres;
    }

    // Historial recibe cada artículo sin acceder a los nodos guardados.
    public void RecorrerArticulos(Action<VentasDetalle> visitar)
    {
        articulos.Recorrer(visitar);
    }
}
