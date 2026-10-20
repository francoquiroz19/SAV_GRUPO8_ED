namespace Jaroni_ED_Grupo8;

// Representa un artículo agregado: producto, cantidad y precio unitario.
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

    // Suma unidades del mismo producto; el subtotal se recalcula con la cantidad.
    public void Acumular(int cantidad) => Cantidad = checked(Cantidad + cantidad);
}
