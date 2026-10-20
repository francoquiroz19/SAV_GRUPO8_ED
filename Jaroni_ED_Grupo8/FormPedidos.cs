
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JaroniApp
{
    public partial class FormPedidos : Form
    {
        private readonly DataGridView tabla = new DataGridView();
        private readonly TextBox txtCliente = new TextBox();
        private readonly TextBox txtProducto = new TextBox();
        private readonly NumericUpDown numCantidad = new NumericUpDown();

        public FormPedidos()
        {
            Text = "GestiÃ³n de pedidos";
            Size = new Size(750, 500);
            StartPosition = FormStartPosition.CenterScreen;

            var lblTitulo = new Label
            {
                Text = "REGISTRO DE PEDIDOS",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(20, 20),
                AutoSize = true
            };
            Controls.Add(lblTitulo);

            Controls.Add(CrearEtiqueta("Cliente:", 25, 85));
            txtCliente.SetBounds(110, 80, 200, 28);
            Controls.Add(txtCliente);

            Controls.Add(CrearEtiqueta("Producto:", 25, 130));
            txtProducto.SetBounds(110, 125, 200, 28);
            Controls.Add(txtProducto);

            Controls.Add(CrearEtiqueta("Cantidad:", 340, 85));
            numCantidad.SetBounds(420, 80, 100, 28);
            numCantidad.Minimum = 1;
            numCantidad.Maximum = 1000;
            numCantidad.Value = 1;
            Controls.Add(numCantidad);

            var btnAgregar = new Button
            {
                Text = "Agregar pedido",
                Location = new Point(340, 122),
                Size = new Size(180, 35)
            };
            btnAgregar.Click += AgregarPedido;
            Controls.Add(btnAgregar);

            tabla.SetBounds(25, 185, 680, 240);
            tabla.AllowUserToAddRows = false;
            tabla.ReadOnly = true;
            tabla.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tabla.Columns.Add("Cliente", "Cliente");
            tabla.Columns.Add("Producto", "Producto");
            tabla.Columns.Add("Cantidad", "Cantidad");

            Controls.Add(tabla);
        }

        private Label CrearEtiqueta(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Location = new Point(x, y),
                AutoSize = true
            };
        }

        private void AgregarPedido(object? sender, EventArgs e)
        {
            string cliente = txtCliente.Text.Trim();
            string producto = txtProducto.Text.Trim();

            if (cliente.Length == 0 || producto.Length == 0)
            {
                MessageBox.Show(
                    "Ingresa el cliente y el producto.",
                    "Datos incompletos",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            tabla.Rows.Add(
                cliente,
                producto,
                numCantidad.Value);

            txtCliente.Clear();
            txtProducto.Clear();
            numCantidad.Value = 1;
        }
    }
}
