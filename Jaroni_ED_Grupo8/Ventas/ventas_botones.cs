using System.Text;

using Jaroni_ED_Grupo8;

namespace JaroniApp;

public partial class FormVentas
{
    private void ConectarEventosVentas()
    {
        // Los eventos de Ventas se conectan una vez en su propio formulario.
        cboProducto.SelectedIndexChanged += (_, _) => ActualizarPrecioVenta();
        btnAgregarVenta.Click += Agregar_Click;
        btnQuitarVenta.Click += Quitar_Click;
        btnSiguienteVenta.Click += Siguiente_Click;
        btnSimularVenta.Click += Simular_Click;
        btnLimpiarVenta.Click += LimpiarVenta_Click;
    }

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

    private void Simular_Click(object? sender, EventArgs e)
    {
        if (detalleVenta.ObtenerCabeza() is null)
        {
            MessageBox.Show(this, "Agregue al menos un artículo antes de simular.", "Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        var resumen = new StringBuilder("Detalle de la venta (demostración):\n\n");
        detalleVenta.Recorrer(articulo => resumen.AppendLine($"{articulo.Cantidad} × {articulo.Nombre}: {FormatearSoles(articulo.Subtotal)}"));
        resumen.AppendLine($"\nTotal: {FormatearSoles(CalcularTotalVenta())}");
        resumen.AppendLine("\nSimulación realizada. No se registró una venta real ni se modificó el inventario");
        MessageBox.Show(this, resumen.ToString(), "Simular venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    private void LimpiarVenta_Click(object? sender, EventArgs e)
    {
        // Limpiar descarta solo el detalle circular; el catálogo demo permanece.
        detalleVenta.Vaciar();
        articuloActual = null;
        nudCantidad.Value = 1;
        ActualizarPresentacionVenta();
    }
}
