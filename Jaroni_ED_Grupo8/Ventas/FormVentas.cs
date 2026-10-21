using System.Globalization;
using System.Windows.Forms;
using Jaroni_ED_Grupo8;

namespace JaroniApp;

// Gestiona en memoria los artículos de una venta mediante una lista circular.
public partial class FormVentas : Form
{
    private readonly EstadoAplicacion estado;
    private readonly VentasListaCircular<VentasDetalle> detalleVenta;
    private VentasNodo<VentasDetalle>? articuloActual;
    private bool registrandoVenta;
    private static readonly CultureInfo CulturaSoles = CultureInfo.GetCultureInfo("es-PE");

    public FormVentas(EstadoAplicacion estado)
    {
        this.estado = estado;
        detalleVenta = estado.ArticulosVentaActual;
        InitializeComponent();
        CargarProductosRegistrados();
        ActualizarPresentacionVenta();
    }

    private void CargarProductosRegistrados()
    {
        // El selector muestra la lista compartida; no mantiene otro catálogo.
        cboProducto.Items.Clear();
        foreach (var producto in estado.ProductosRegistrados.ObtenerListaParaGrilla())
            cboProducto.Items.Add(producto);

        btnAgregarVenta.Enabled = cboProducto.Items.Count > 0;
        if (btnAgregarVenta.Enabled)
        {
            lblAviso.Text = "Detalle de venta";
            cboProducto.SelectedIndex = 0;
        }
        else
            lblAviso.Text = "Registre productos en Productos antes de vender.";
    }

    // Muestra el precio del producto seleccionado, sin modificar el detalle.
    private void ActualizarPrecioVenta(object? sender, EventArgs e)
    {
        if (cboProducto.SelectedItem is Producto producto)
            txtPrecio.Text = FormatearSoles(producto.Precio);
        else
            txtPrecio.Clear();
    }

    private static string FormatearSoles(decimal importe) => "S/ " + importe.ToString("N2", CulturaSoles);

    // Suma los subtotales recorriendo los artículos de la lista.
    private decimal CalcularTotalVenta()
    {
        VentasNodo<VentasDetalle>? primerNodo = detalleVenta.ObtenerCabeza();
        if (primerNodo is null) return 0;

        decimal totalVenta = 0;
        VentasNodo<VentasDetalle> nodoActual = primerNodo;
        do
        {
            totalVenta += nodoActual.Dato.Subtotal;
            nodoActual = nodoActual.Siguiente;
        } while (nodoActual != primerNodo);
        return totalVenta;
    }

    // Reconstruye la tabla y destaca el nodo actualmente seleccionado.
    private void ActualizarPresentacionVenta(bool volvioAlPrimero = false)
    {
        dgvDetalle.Rows.Clear();
        // La tabla se reconstruye desde los nodos; no guarda el detalle de la venta.
        VentasNodo<VentasDetalle>? primerNodo = detalleVenta.ObtenerCabeza();
        if (primerNodo is not null)
        {
            VentasNodo<VentasDetalle> nodoActual = primerNodo;
            do
            {
                VentasDetalle articulo = nodoActual.Dato;
                int fila = dgvDetalle.Rows.Add(articulo.Nombre, articulo.Cantidad,
                    FormatearSoles(articulo.PrecioUnitario), FormatearSoles(articulo.Subtotal));
                dgvDetalle.Rows[fila].Tag = articulo.Codigo;
                nodoActual = nodoActual.Siguiente;
            } while (nodoActual != primerNodo);
        }

        lblTotalValor.Text = "Total: " + FormatearSoles(CalcularTotalVenta());
        if (articuloActual is null)
            lblActual.Text = "Artículo actual: ninguno";
        else
        {
            lblActual.Text = "Artículo actual: " + articuloActual.Dato.Nombre;
            if (volvioAlPrimero) lblActual.Text += " (volvió al primero)";
        }

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
        if (cboProducto.SelectedItem is not Producto producto)
        {
            MessageBox.Show(this, "Seleccione un producto.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (nudCantidad.Value < 1 || nudCantidad.Value != decimal.Truncate(nudCantidad.Value))
        {
            MessageBox.Show(this, "Ingrese una cantidad entera positiva.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int cantidad = decimal.ToInt32(nudCantidad.Value);
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
        {
            if (retirado.Siguiente == retirado)
                articuloActual = null;
            else
                articuloActual = retirado.Siguiente;
        }

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
        VentasNodo<VentasDetalle> siguiente;
        if (articuloActual is null)
            siguiente = cabeza;
        else
            siguiente = articuloActual.Siguiente;
        bool volvioAlPrimero = articuloActual is not null && siguiente == cabeza && articuloActual != cabeza;
        articuloActual = siguiente;
        ActualizarPresentacionVenta(volvioAlPrimero);
    }

    // Confirma el detalle circular, descuenta stock y conserva una copia registrada.
    private void ConfirmarVenta_Click(object? sender, EventArgs e)
    {
        if (registrandoVenta) return;
        VentasNodo<VentasDetalle>? primerNodo = detalleVenta.ObtenerCabeza();
        if (primerNodo is null)
        {
            MessageBox.Show(this, "Agregue al menos un artículo antes de confirmar.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        registrandoVenta = true;
        btnConfirmarVenta.Enabled = false;
        // finally reactiva el botón incluso si se cancela el cuadro de confirmación.
        try
        {
            string resumen = "Confirmar venta:\n\n";
            VentasNodo<VentasDetalle> nodoActual = primerNodo;
            do
            {
                VentasDetalle articulo = nodoActual.Dato;
                resumen += $"{articulo.Cantidad} × {articulo.Nombre}: {FormatearSoles(articulo.Subtotal)}\n";
                nodoActual = nodoActual.Siguiente;
            } while (nodoActual != primerNodo);
            resumen += "\nTotal: " + FormatearSoles(CalcularTotalVenta());
            if (MessageBox.Show(this, resumen, "Confirmar venta",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (!RegistrarVentaActual(out var ventaRegistrada, out var error))
            {
                MessageBox.Show(this, error, "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            MessageBox.Show(this, $"Venta {ventaRegistrada!.Codigo} registrada en esta sesión.",
                "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        finally
        {
            registrandoVenta = false;
            btnConfirmarVenta.Enabled = true;
        }
    }

    // Validatodo el detalle antes de descontar; la venta se guarda solo si todos los descuentos terminan
    private bool RegistrarVentaActual(out VentaRegistrada? ventaRegistrada, out string? error)
    {
        ventaRegistrada = null;
        error = null;
        VentasNodo<VentasDetalle>? primerNodo = detalleVenta.ObtenerCabeza();
        if (primerNodo is null)
        {
            error = "No hay artículos para registrar.";
            return false;
        }

        // Primero revisamos todos los nodos. No se descuenta nada si falta stock.
        VentasNodo<VentasDetalle> nodoActual = primerNodo;
        do
        {
            VentasDetalle articulo = nodoActual.Dato;
            Producto? productoEncontrado = estado.ProductosRegistrados.Buscar(articulo.Codigo);
            if (productoEncontrado is null || articulo.Cantidad <= 0 ||
                productoEncontrado.Cantidad < articulo.Cantidad)
            {
                error = $"No hay existencias suficientes para {articulo.Nombre}.";
                return false;
            }
            nodoActual = nodoActual.Siguiente;
        } while (nodoActual != primerNodo);

        // El registro copia los artículos antes de vaciar la lista editable.
        try
        {
            ventaRegistrada = new VentaRegistrada(estado.TomarCodigoVenta(), DateTime.Now, detalleVenta);
        }
        catch (OverflowException)
        {
            error = "La cantidad o el total excede el límite permitido.";
            return false;
        }

        // Descontamos después de validar toda la venta.
        nodoActual = primerNodo;
        do
        {
            VentasDetalle articulo = nodoActual.Dato;
            if (!estado.ProductosRegistrados.RegistrarSalida(articulo.Codigo, articulo.Cantidad))
            {
                // Si falló un descuento, devolvemos las unidades de los nodos anteriores.
                VentasNodo<VentasDetalle> nodoRestaurar = primerNodo;
                while (nodoRestaurar != nodoActual)
                {
                    VentasDetalle descontado = nodoRestaurar.Dato;
                    estado.ProductosRegistrados.RegistrarEntrada(descontado.Codigo, descontado.Cantidad);
                    nodoRestaurar = nodoRestaurar.Siguiente;
                }
                ventaRegistrada = null;
                error = "No se pudo completar la venta. El inventario no cambió.";
                return false;
            }
            nodoActual = nodoActual.Siguiente;
        } while (nodoActual != primerNodo);

        estado.VentasRegistradas.Agregar(ventaRegistrada);
        estado.VentasPendientesRevision.Push(ventaRegistrada);
        detalleVenta.Vaciar();
        articuloActual = null;
        nudCantidad.Value = 1;
        ActualizarPresentacionVenta();
        return true;
    }

    // Vacía solamente el detalle en preparación; no afecta ventas confirmadas.
    private void LimpiarVenta_Click(object? sender, EventArgs e)
    {
        // El catálogo sigue proviniendo de la lista compartida de Productos.
        detalleVenta.Vaciar();
        articuloActual = null;
        nudCantidad.Value = 1;
        ActualizarPresentacionVenta();
    }
}
