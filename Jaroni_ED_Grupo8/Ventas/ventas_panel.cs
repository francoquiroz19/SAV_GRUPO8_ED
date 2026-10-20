namespace JaroniApp;
using Jaroni_ED_Grupo8;


public partial class FormVentas
{
    private readonly Panel panelVentas = new();
    private readonly ComboBox cboProducto = new();
    private readonly TextBox txtPrecio = new();
    private readonly NumericUpDown nudCantidad = new();
    private readonly Button btnAgregarVenta = new();
    private readonly DataGridView dgvDetalle = new();
    private readonly Label lblActual = new();
    private readonly Label lblTotalValor = new();
    private readonly Button btnQuitarVenta = new();
    private readonly Button btnSiguienteVenta = new();
    private readonly Button btnSimularVenta = new();
    private readonly Button btnLimpiarVenta = new();

    private void InicializarSeccionVentas()
    {
        panelVentas.Location = new Point(20, 20);
        panelVentas.Size = new Size(580, 350);
        panelVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        Controls.Add(panelVentas);

        panelVentas.Controls.Add(new Label
        {
            Text = "Ventas de prueba — sin conexión a Inventario",
            Location = new Point(0, 2), Size = new Size(420, 23)
        });
        panelVentas.Controls.Add(new Label { Text = "Producto de prueba", Location = new Point(0, 29), Size = new Size(225, 22) });
        panelVentas.Controls.Add(new Label { Text = "Precio", Location = new Point(240, 29), Size = new Size(100, 22) });
        panelVentas.Controls.Add(new Label { Text = "Cantidad", Location = new Point(350, 29), Size = new Size(80, 22) });

        cboProducto.Location = new Point(0, 52);
        cboProducto.Size = new Size(225, 23);
        cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
        panelVentas.Controls.Add(cboProducto);

        txtPrecio.Location = new Point(240, 52);
        txtPrecio.Size = new Size(100, 23);
        txtPrecio.ReadOnly = true;
        panelVentas.Controls.Add(txtPrecio);

        nudCantidad.Location = new Point(350, 52);
        nudCantidad.Size = new Size(80, 23);
        nudCantidad.Minimum = 1;
        nudCantidad.Maximum = 100000;
        nudCantidad.Value = 1;
        nudCantidad.DecimalPlaces = 0;
        panelVentas.Controls.Add(nudCantidad);

        btnAgregarVenta.Text = "Agregar";
        btnAgregarVenta.Location = new Point(445, 51);
        btnAgregarVenta.Size = new Size(130, 26);
        panelVentas.Controls.Add(btnAgregarVenta);

        dgvDetalle.Location = new Point(0, 91);
        dgvDetalle.Size = new Size(580, 155);
        dgvDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dgvDetalle.ReadOnly = true;
        dgvDetalle.AllowUserToAddRows = false;
        dgvDetalle.AllowUserToDeleteRows = false;
        dgvDetalle.AllowUserToResizeRows = false;
        dgvDetalle.MultiSelect = false;
        dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dgvDetalle.RowHeadersVisible = false;
        dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDetalle.BackgroundColor = Color.White;
        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", FillWeight = 43F });
        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", FillWeight = 16F });
        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio", FillWeight = 20F });
        dgvDetalle.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Subtotal", FillWeight = 21F });
        panelVentas.Controls.Add(dgvDetalle);

        lblActual.Text = "Artículo actual: ninguno";
        lblActual.Location = new Point(0, 253);
        lblActual.Size = new Size(580, 22);
        lblActual.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        panelVentas.Controls.Add(lblActual);

        lblTotalValor.Text = "Total: S/ 0.00";
        lblTotalValor.Location = new Point(355, 278);
        lblTotalValor.Size = new Size(225, 24);
        lblTotalValor.TextAlign = ContentAlignment.MiddleRight;
        lblTotalValor.Font = new Font(Font, FontStyle.Bold);
        lblTotalValor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        panelVentas.Controls.Add(lblTotalValor);

        btnQuitarVenta.Text = "Quitar";
        btnQuitarVenta.Location = new Point(0, 311);
        btnQuitarVenta.Size = new Size(105, 29);
        btnSiguienteVenta.Text = "Siguiente";
        btnSiguienteVenta.Location = new Point(115, 311);
        btnSiguienteVenta.Size = new Size(105, 29);
        btnSimularVenta.Text = "Simular venta";
        btnSimularVenta.Location = new Point(230, 311);
        btnSimularVenta.Size = new Size(120, 29);
        btnLimpiarVenta.Text = "Limpiar";
        btnLimpiarVenta.Location = new Point(360, 311);
        btnLimpiarVenta.Size = new Size(105, 29);
        foreach (var boton in new[] { btnQuitarVenta, btnSiguienteVenta, btnSimularVenta, btnLimpiarVenta })
        {
            boton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            panelVentas.Controls.Add(boton);
        }

        ConectarEventosVentas();
        CargarProductosDemo();
        ActualizarPresentacionVenta();
    }

    private void CargarProductosDemo()
    {
        // Los productos ficticios existen solo aquí; Ventas no altera Inventario.
        cboProducto.Items.Add(new VentasProductoDemo("M001", "Mouse óptico", 35.90m));
        cboProducto.Items.Add(new VentasProductoDemo("T001", "Teclado USB", 59.00m));
        cboProducto.Items.Add(new VentasProductoDemo("I001", "Tinta negra", 48.50m));
        cboProducto.Items.Add(new VentasProductoDemo("P001", "Papel A4 (paquete)", 24.90m));
        cboProducto.SelectedIndex = 0;
    }

    private void ActualizarPrecioVenta()
    {
        txtPrecio.Text = cboProducto.SelectedItem is VentasProductoDemo producto
            ? FormatearSoles(producto.PrecioUnitario)
            : string.Empty;
    }

    private static string FormatearSoles(decimal importe) => "S/ " + importe.ToString("N2", CulturaSoles);

    private decimal CalcularTotalVenta()
    {
        decimal total = 0;
        detalleVenta.Recorrer(articulo => total += articulo.Subtotal);
        return total;
    }

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
}
