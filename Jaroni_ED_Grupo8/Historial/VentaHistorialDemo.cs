namespace JaroniApp
{
    // Registro inicial en memoria: producto, fecha, cantidad e importe calculado.
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
