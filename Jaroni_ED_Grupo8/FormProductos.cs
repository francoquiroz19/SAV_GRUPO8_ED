using System;
using System.Windows.Forms;

namespace Jaroni_ED_Grupo8
{
    public partial class FormProductos : Form
    {
        // Lista enlazada que almacena los productos del inventario.
        private ListaProductos listaProductos = new ListaProductos();

        // Constructor del formulario.
        public FormProductos()
        {
            InitializeComponent();

            // Configura la tabla del inventario.
            dgvInventario.AutoGenerateColumns = true;

            // Permite seleccionar una fila completa.
            dgvInventario.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            // Muestra los productos registrados.
            MostrarTodos();
        }
        // Muestra todos los productos registrados en la tabla.
        private void MostrarTodos()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource =
                listaProductos.ObtenerListaParaGrilla();
        }
        // Registra un producto nuevo.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            int cantidad;
            decimal precio;

            if (!int.TryParse(txtCantidad.Text, out cantidad) ||
                !decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingresa una cantidad y un precio válidos.");
                return;
            }

            Producto producto = new Producto(
                txtCodigo.Text,
                txtNombre.Text,
                txtCategoria.Text,
                cantidad,
                precio,
                txtUbicacion.Text
            );

            if (listaProductos.Registrar(producto))
            {
                MostrarTodos();
                MessageBox.Show("Producto registrado correctamente.");
            }
            else
            {
                MessageBox.Show("Ese código ya existe.");
            }
        }

        // Busca un producto por su código.
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            Producto producto = listaProductos.Buscar(txtCodigo.Text);

            if (producto == null)
            {
                MessageBox.Show("Producto no encontrado.");
                return;
            }

            txtNombre.Text = producto.Nombre;
            txtCategoria.Text = producto.Categoria;
            txtCantidad.Text = producto.Cantidad.ToString();
            txtPrecio.Text = producto.Precio.ToString();
            txtUbicacion.Text = producto.Ubicacion;
        }

        // Actualiza los datos de un producto.
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            decimal precio;

            if (!decimal.TryParse(txtPrecio.Text, out precio))
            {
                MessageBox.Show("Ingresa un precio válido.");
                return;
            }

            if (listaProductos.Actualizar(
                txtCodigo.Text,
                txtNombre.Text,
                txtCategoria.Text,
                precio,
                txtUbicacion.Text))
            {
                MostrarTodos();
                MessageBox.Show("Producto actualizado.");
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        // Elimina un producto por su código.
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (listaProductos.Eliminar(txtCodigo.Text))
            {
                MostrarTodos();
                MessageBox.Show("Producto eliminado.");
            }
            else
            {
                MessageBox.Show("Producto no encontrado.");
            }
        }

        // Aumenta la cantidad disponible en el inventario.
        private void btnEntrada_Click(object sender, EventArgs e)
        {
            int cantidad;

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("Ingresa una cantidad válida.");
                return;
            }

            if (listaProductos.RegistrarEntrada(txtCodigo.Text, cantidad))
            {
                MostrarTodos();
                MessageBox.Show("Entrada registrada.");
            }
            else
            {
                MessageBox.Show("No se pudo registrar la entrada.");
            }
        }

        // Disminuye la cantidad disponible en el inventario.
        private void btnSalida_Click(object sender, EventArgs e)
        {
            int cantidad;

            if (!int.TryParse(txtCantidad.Text, out cantidad))
            {
                MessageBox.Show("Ingresa una cantidad válida.");
                return;
            }

            if (listaProductos.RegistrarSalida(txtCodigo.Text, cantidad))
            {
                MostrarTodos();
                MessageBox.Show("Salida registrada.");
            }
            else
            {
                MessageBox.Show("Stock insuficiente o producto no encontrado.");
            }
        }

        // Muestra los productos que tienen poco stock.
        private void btnBajoStock_Click(object sender, EventArgs e)
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = listaProductos.ObtenerBajoStock(5);
        }

        // Muestra todos los productos registrados.
        private void btnMostrarTodos_Click(object sender, EventArgs e)
        {
            MostrarTodos();
        }
    }

}
