namespace Jaroni_ED_Grupo8;

// Representa un artículo agregado: producto, cantidad y precio unitario.
public sealed class VentasDetalle
{
    public string Codigo { get; }
    public string Nombre { get; }
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; }
    public decimal Subtotal => Cantidad * PrecioUnitario;

    public VentasDetalle(Producto producto, int cantidad)
        : this(producto.Codigo, producto.Nombre, cantidad, producto.Precio)
    {
    }

    // Conserva código, nombre y precio aunque el inventario cambie después.
    public VentasDetalle(string codigo, string nombre, int cantidad, decimal precioUnitario)
    {
        Codigo = codigo;
        Nombre = nombre;
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
    }

    // Suma unidades del mismo producto; el subtotal se recalcula con la cantidad.
    public void Acumular(int cantidad)
    {
        if (cantidad <= 0 || Cantidad > int.MaxValue - cantidad)
            throw new OverflowException("La cantidad del artículo es inválida.");
        Cantidad += cantidad;
    }
}
