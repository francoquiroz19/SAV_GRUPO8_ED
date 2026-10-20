using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Jaroni_ED_Grupo8;

namespace JaroniApp;

// Gestiona en memoria los artículos de una venta mediante una lista circular.
public partial class FormVentas : Form
{
    private readonly VentasListaCircular<VentasDetalle> detalleVenta = new();
    private VentasNodo<VentasDetalle>? articuloActual;
    private static readonly CultureInfo CulturaSoles = CultureInfo.GetCultureInfo("es-PE");

    public FormVentas()
    {
        InitializeComponent();
        CargarProductosDemo();
        ActualizarPresentacionVenta();
    }

    private void CargarProductosDemo()
    {
        // Carga productos de ejemplo en memoria; no consulta ni altera Inventario.
        cboProducto.Items.Add(new VentasProductoDemo("M001", "Mouse óptico", 35.90m));
        cboProducto.Items.Add(new VentasProductoDemo("T001", "Teclado USB", 59.00m));
        cboProducto.Items.Add(new VentasProductoDemo("I001", "Tinta negra", 48.50m));
        cboProducto.Items.Add(new VentasProductoDemo("P001", "Papel A4 (paquete)", 24.90m));
        cboProducto.SelectedIndex = 0;
    }

    // Muestra el precio del producto seleccionado, sin modificar el detalle.
    private void ActualizarPrecioVenta(object? sender, EventArgs e)
    {
        txtPrecio.Text = cboProducto.SelectedItem is VentasProductoDemo producto
            ? FormatearSoles(producto.PrecioUnitario)
            : string.Empty;
    }

    private static string FormatearSoles(decimal importe) => "S/ " + importe.ToString("N2", CulturaSoles);

    // Suma los subtotales recorriendo los artículos de la lista.
    private decimal CalcularTotalVenta()
    {
        decimal total = 0;
        detalleVenta.Recorrer(articulo => total += articulo.Subtotal);
        return total;
    }

    // Reconstruye la tabla y destaca el nodo actualmente seleccionado.
    private void ActualizarPresentacionVenta(bool volvioAlPrimero = false)
    {
        dgvDetalle.Rows.Clear();
        // La tabla se reconstruye desde los nodos; no guarda el detalle de la venta.
        detalleVenta.Recorrer(articulo =>
        {
            var indice = dgvDetalle.Rows.Add(articulo.Nombre, articulo.Cantidad,
                FormatearSoles(articulo.PrecioUnitario), FormatearSoles(articulo.Subtotal));
            dgvDetalle.Rows[indice].Tag = articulo.Codigo;
        });

        lblTotalValor.Text = "Total: " + FormatearSoles(CalcularTotalVenta());
        lblActual.Text = articuloActual is null
            ? "Artículo actual: ninguno"
            : "Artículo actual: " + articuloActual.Dato.Nombre + (volvioAlPrimero ? " (volvió al primero)" : "");

        dgvDetalle.ClearSelection();
        if (articuloActual is null) return;
        foreach (DataGridViewRow fila in dgvDetalle.Rows)
        {
            if (fila.Tag as string != articuloActual.Dato.Codigo) continue;
            fila.Selected = true;
            dgvDetalle.CurrentCell = fila.Cells[0];
            break;
        }
    }

    // Inserta un artículo o acumula su cantidad si ya existe en la lista.
    private void Agregar_Click(object? sender, EventArgs e)
    {
        if (cboProducto.SelectedItem is not VentasProductoDemo producto ||
            nudCantidad.Value < 1 || nudCantidad.Value != decimal.Truncate(nudCantidad.Value))
        {
            MessageBox.Show(this, "Seleccione un producto y una cantidad entera positiva.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var cantidad = decimal.ToInt32(nudCantidad.Value);
        // Buscar recorre los nodos; agregar el mismo código acumula en su artículo.
        var existente = detalleVenta.Buscar(articulo => articulo.Codigo == producto.Codigo);
        try
        {
            if (existente is null)
                articuloActual = detalleVenta.InsertarAlFinal(new VentasDetalle(producto, cantidad));
            else
            {
                existente.Dato.Acumular(cantidad);
                articuloActual = existente;
            }
        }
        catch (OverflowException)
        {
            MessageBox.Show(this, "La cantidad excede el límite permitido.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        ActualizarPresentacionVenta();
    }

    // Elimina el nodo elegido en la tabla y actualiza el artículo actual.
    private void Quitar_Click(object? sender, EventArgs e)
    {
        if (dgvDetalle.SelectedRows.Count == 0)
        {
            MessageBox.Show(this, "Seleccione un artículo del detalle.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var codigo = dgvDetalle.SelectedRows[0].Tag as string;
        if (codigo is null) return;

        // Se elimina el nodo real. Si era el actual, avanzamos antes de desconectarlo.
        var retirado = detalleVenta.Buscar(articulo => articulo.Codigo == codigo);
        if (retirado is null) return;
        if (retirado == articuloActual)
            articuloActual = retirado.Siguiente == retirado ? null : retirado.Siguiente;

        detalleVenta.Eliminar(articulo => articulo.Codigo == codigo);
        ActualizarPresentacionVenta();
    }

    // Avanza por Siguiente; después de la cola regresa a la cabeza.
    private void Siguiente_Click(object? sender, EventArgs e)
    {
        var cabeza = detalleVenta.ObtenerCabeza();
        if (cabeza is null)
        {
            MessageBox.Show(this, "Agregue un artículo para recorrer el detalle.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        // El avance usa el enlace del nodo: tras la cola vuelve a la cabeza.
        var siguiente = articuloActual?.Siguiente ?? cabeza;
        var volvioAlPrimero = articuloActual is not null && siguiente == cabeza && articuloActual != cabeza;
        articuloActual = siguiente;
        ActualizarPresentacionVenta(volvioAlPrimero);
    }

    // Presenta un resumen sin registrar la venta ni alterar el inventario.
    private void Simular_Click(object? sender, EventArgs e)
    {
        if (detalleVenta.ObtenerCabeza() is null)
        {
            MessageBox.Show(this, "Agregue al menos un artículo para ver el resumen.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var resumen = new StringBuilder("Resumen de venta:\n\n");
        detalleVenta.Recorrer(articulo => resumen.AppendLine($"{articulo.Cantidad} × {articulo.Nombre}: {FormatearSoles(articulo.Subtotal)}"));
        resumen.AppendLine($"\nTotal: {FormatearSoles(CalcularTotalVenta())}");
        resumen.AppendLine("\nEste resumen no registra la venta ni modifica el inventario.");
        MessageBox.Show(this, resumen.ToString(), "Resumen de venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    // Vacía el detalle en preparación y conserva el catálogo inicial.
    private void LimpiarVenta_Click(object? sender, EventArgs e)
    {
        // Limpiar descarta solo el detalle circular; el catálogo demo permanece.
        detalleVenta.Vaciar();
        articuloActual = null;
        nudCantidad.Value = 1;
        ActualizarPresentacionVenta();
    }
}
