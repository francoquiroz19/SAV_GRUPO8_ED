namespace Jaroni_ED_Grupo8;

public sealed class VentasDetalle
{
    public string Codigo { get; }
    public string Nombre { get; }
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; }
    public decimal Subtotal => Cantidad * PrecioUnitario;

    public VentasDetalle(VentasProductoDemo producto, int cantidad)
    {
        Codigo = producto.Codigo;
        Nombre = producto.Nombre;
        PrecioUnitario = producto.PrecioUnitario;
        Cantidad = cantidad;
    }

    public void Acumular(int cantidad) => Cantidad = checked(Cantidad + cantidad);
}
