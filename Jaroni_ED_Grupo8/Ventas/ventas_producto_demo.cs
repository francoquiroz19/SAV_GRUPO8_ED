namespace Jaroni_ED_Grupo8;

// Producto de ejemplo disponible solo para el catálogo en memoria de Ventas.
public sealed class VentasProductoDemo
{
    public string Codigo { get; }
    public string Nombre { get; }
    public decimal PrecioUnitario { get; }

    public VentasProductoDemo(string codigo, string nombre, decimal precioUnitario)
    {
        Codigo = codigo;
        Nombre = nombre;
        PrecioUnitario = precioUnitario;
    }

    public override string ToString() => $"{Codigo} — {Nombre}";
}
