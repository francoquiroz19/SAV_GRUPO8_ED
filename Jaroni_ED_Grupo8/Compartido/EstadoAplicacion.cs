namespace Jaroni_ED_Grupo8;

// Comparte las mismas estructuras durante la sesión; no guarda datos en disco.
public sealed class EstadoAplicacion
{
    public ListaProductos ProductosRegistrados { get; } = new ListaProductos();
    public ListaClientes ClientesRegistrados { get; } = new ListaClientes();
    public ListaPedidos PedidosRegistrados { get; } = new ListaPedidos();
    public VentasListaCircular<VentasDetalle> ArticulosVentaActual { get; } = new VentasListaCircular<VentasDetalle>();
    public ListaVentasRegistradas VentasRegistradas { get; } = new ListaVentasRegistradas();
    public PilaHistorial VentasPendientesRevision { get; } = new PilaHistorial();

    public int VentasRevisadasEnSesion { get; private set; }
    private int siguienteCodigoVenta = 1;
    private int siguienteCodigoPedido = 1;

    public int TomarCodigoPedido()
    {
        int codigo = siguienteCodigoPedido;
        siguienteCodigoPedido++;
        return codigo;
    }

    public string TomarCodigoVenta()
    {
        int numero = siguienteCodigoVenta;
        siguienteCodigoVenta++;
        return "V" + numero.ToString("000");
    }

    public void RegistrarRevision()
    {
        VentasRevisadasEnSesion++;
    }

    // Reconstruye los pendientes solo con ventas confirmadas durante esta sesión.
    public void ReiniciarRevision()
    {
        VentasPendientesRevision.Clear();
        VentasRegistradas.Recorrer(venta => VentasPendientesRevision.Push(venta));
        VentasRevisadasEnSesion = 0;
    }
}
