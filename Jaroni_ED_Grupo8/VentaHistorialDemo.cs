namespace JaroniApp
{
    // Registro ficticio de un solo producto; no representa una venta real del sistema.
    public sealed class VentaHistorialDemo
    {
        public string Codigo { get; }
        public DateTime FechaHora { get; }
        public string Producto { get; }
        public int Cantidad { get; }
        public decimal PrecioUnitario { get; }
        public decimal Total => Cantidad * PrecioUnitario;

        public VentaHistorialDemo(string codigo, DateTime fechaHora, string producto, int cantidad, decimal precioUnitario)
        {
            Codigo = codigo;
            FechaHora = fechaHora;
            Producto = producto;
            Cantidad = cantidad;
            PrecioUnitario = precioUnitario;
        }
    }
}
