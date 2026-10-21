#nullable enable
using System.Drawing;
using System.Windows.Forms;

namespace JaroniApp
{
    partial class FormVentas
    {
        private System.ComponentModel.IContainer? components = null;
        private Panel panelVentas = null!;
        private Label lblAviso = null!;
        private Label lblProducto = null!;
        private Label lblPrecio = null!;
        private Label lblCantidad = null!;
        private ComboBox cboProducto = null!;
        private TextBox txtPrecio = null!;
        private NumericUpDown nudCantidad = null!;
        private Button btnAgregarVenta = null!;
        private DataGridView dgvDetalle = null!;
        private DataGridViewTextBoxColumn colProducto = null!;
        private DataGridViewTextBoxColumn colCantidad = null!;
        private DataGridViewTextBoxColumn colPrecio = null!;
        private DataGridViewTextBoxColumn colSubtotal = null!;
        private Label lblActual = null!;
        private Label lblTotalValor = null!;
        private Button btnQuitarVenta = null!;
        private Button btnSiguienteVenta = null!;
        private Button btnConfirmarVenta = null!;
        private Button btnLimpiarVenta = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                components?.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelVentas = new Panel();
            lblAviso = new Label();
            lblProducto = new Label();
            lblPrecio = new Label();
            lblCantidad = new Label();
            cboProducto = new ComboBox();
            txtPrecio = new TextBox();
            nudCantidad = new NumericUpDown();
            btnAgregarVenta = new Button();
            dgvDetalle = new DataGridView();
            colProducto = new DataGridViewTextBoxColumn();
            colCantidad = new DataGridViewTextBoxColumn();
            colPrecio = new DataGridViewTextBoxColumn();
            colSubtotal = new DataGridViewTextBoxColumn();
            lblActual = new Label();
            lblTotalValor = new Label();
            btnQuitarVenta = new Button();
            btnSiguienteVenta = new Button();
            btnConfirmarVenta = new Button();
            btnLimpiarVenta = new Button();
            panelVentas.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).BeginInit();
            SuspendLayout();

            // panelVentas
            panelVentas.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            panelVentas.Controls.Add(lblAviso);
            panelVentas.Controls.Add(lblProducto);
            panelVentas.Controls.Add(lblPrecio);
            panelVentas.Controls.Add(lblCantidad);
            panelVentas.Controls.Add(cboProducto);
            panelVentas.Controls.Add(txtPrecio);
            panelVentas.Controls.Add(nudCantidad);
            panelVentas.Controls.Add(btnAgregarVenta);
            panelVentas.Controls.Add(dgvDetalle);
            panelVentas.Controls.Add(lblActual);
            panelVentas.Controls.Add(lblTotalValor);
            panelVentas.Controls.Add(btnQuitarVenta);
            panelVentas.Controls.Add(btnSiguienteVenta);
            panelVentas.Controls.Add(btnConfirmarVenta);
            panelVentas.Controls.Add(btnLimpiarVenta);
            panelVentas.Location = new Point(20, 20);
            panelVentas.Name = "panelVentas";
            panelVentas.Size = new Size(580, 350);

            // etiquetas de entrada
            lblAviso.Location = new Point(0, 2);
            lblAviso.Name = "lblAviso";
            lblAviso.Size = new Size(420, 23);
            lblAviso.Text = "Detalle de venta";
            lblProducto.Location = new Point(0, 29);
            lblProducto.Name = "lblProducto";
            lblProducto.Size = new Size(225, 22);
            lblProducto.Text = "Producto";
            lblPrecio.Location = new Point(240, 29);
            lblPrecio.Name = "lblPrecio";
            lblPrecio.Size = new Size(100, 22);
            lblPrecio.Text = "Precio";
            lblCantidad.Location = new Point(350, 29);
            lblCantidad.Name = "lblCantidad";
            lblCantidad.Size = new Size(80, 22);
            lblCantidad.Text = "Cantidad";

            // producto, precio y cantidad
            cboProducto.DropDownStyle = ComboBoxStyle.DropDownList;
            cboProducto.Location = new Point(0, 52);
            cboProducto.Name = "cboProducto";
            cboProducto.Size = new Size(225, 23);
            cboProducto.SelectedIndexChanged += ActualizarPrecioVenta;
            txtPrecio.Location = new Point(240, 52);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.ReadOnly = true;
            txtPrecio.Size = new Size(100, 23);
            nudCantidad.Location = new Point(350, 52);
            nudCantidad.Maximum = 100000;
            nudCantidad.Minimum = 1;
            nudCantidad.Name = "nudCantidad";
            nudCantidad.Size = new Size(80, 23);
            nudCantidad.Value = 1;
            btnAgregarVenta.Location = new Point(445, 51);
            btnAgregarVenta.Name = "btnAgregarVenta";
            btnAgregarVenta.Size = new Size(130, 26);
            btnAgregarVenta.Text = "Agregar";
            btnAgregarVenta.UseVisualStyleBackColor = true;
            btnAgregarVenta.Click += Agregar_Click;

            // dgvDetalle: solo presenta los nodos de la lista circular.
            dgvDetalle.AllowUserToAddRows = false;
            dgvDetalle.AllowUserToDeleteRows = false;
            dgvDetalle.AllowUserToResizeRows = false;
            dgvDetalle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            dgvDetalle.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvDetalle.BackgroundColor = Color.White;
            dgvDetalle.Columns.AddRange(new DataGridViewColumn[] { colProducto, colCantidad, colPrecio, colSubtotal });
            dgvDetalle.Location = new Point(0, 91);
            dgvDetalle.MultiSelect = false;
            dgvDetalle.Name = "dgvDetalle";
            dgvDetalle.ReadOnly = true;
            dgvDetalle.RowHeadersVisible = false;
            dgvDetalle.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDetalle.Size = new Size(580, 155);
            colProducto.FillWeight = 43F;
            colProducto.HeaderText = "Producto";
            colProducto.Name = "colProducto";
            colCantidad.FillWeight = 16F;
            colCantidad.HeaderText = "Cantidad";
            colCantidad.Name = "colCantidad";
            colPrecio.FillWeight = 20F;
            colPrecio.HeaderText = "Precio";
            colPrecio.Name = "colPrecio";
            colSubtotal.FillWeight = 21F;
            colSubtotal.HeaderText = "Subtotal";
            colSubtotal.Name = "colSubtotal";

            // estado y total
            lblActual.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            lblActual.Location = new Point(0, 253);
            lblActual.Name = "lblActual";
            lblActual.Size = new Size(580, 22);
            lblActual.Text = "Artículo actual: ninguno";
            lblTotalValor.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTotalValor.Font = new Font(Font, FontStyle.Bold);
            lblTotalValor.Location = new Point(355, 278);
            lblTotalValor.Name = "lblTotalValor";
            lblTotalValor.Size = new Size(225, 24);
            lblTotalValor.Text = "Total: S/ 0.00";
            lblTotalValor.TextAlign = ContentAlignment.MiddleRight;

            // botones de la venta
            btnQuitarVenta.Location = new Point(0, 311);
            btnQuitarVenta.Name = "btnQuitarVenta";
            btnQuitarVenta.Size = new Size(105, 29);
            btnQuitarVenta.Text = "Quitar";
            btnQuitarVenta.UseVisualStyleBackColor = true;
            btnQuitarVenta.Click += Quitar_Click;
            btnSiguienteVenta.Location = new Point(115, 311);
            btnSiguienteVenta.Name = "btnSiguienteVenta";
            btnSiguienteVenta.Size = new Size(105, 29);
            btnSiguienteVenta.Text = "Siguiente";
            btnSiguienteVenta.UseVisualStyleBackColor = true;
            btnSiguienteVenta.Click += Siguiente_Click;
            btnConfirmarVenta.Location = new Point(230, 311);
            btnConfirmarVenta.Name = "btnConfirmarVenta";
            btnConfirmarVenta.Size = new Size(120, 29);
            btnConfirmarVenta.Text = "Confirmar venta";
            btnConfirmarVenta.UseVisualStyleBackColor = true;
            btnConfirmarVenta.Click += ConfirmarVenta_Click;
            btnLimpiarVenta.Location = new Point(360, 311);
            btnLimpiarVenta.Name = "btnLimpiarVenta";
            btnLimpiarVenta.Size = new Size(105, 29);
            btnLimpiarVenta.Text = "Limpiar";
            btnLimpiarVenta.UseVisualStyleBackColor = true;
            btnLimpiarVenta.Click += LimpiarVenta_Click;

            // FormVentas
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(624, 381);
            Controls.Add(panelVentas);
            MinimumSize = new Size(640, 420);
            Name = "FormVentas";
            StartPosition = FormStartPosition.CenterParent;
            Text = "JARONI - PANEL DE VENTAS";
            panelVentas.ResumeLayout(false);
            panelVentas.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudCantidad).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetalle).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}
