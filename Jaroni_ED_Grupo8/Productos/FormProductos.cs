using System;
using System.Globalization;
using System.Windows.Forms;

namespace Jaroni_ED_Grupo8
{
    // Permite consultar y mantener productos almacenados en la lista enlazada.
    public partial class FormProductos : Form
    {
        // Conserva la misma lista de inventario al cerrar y reabrir el formulario.
        private readonly ListaProductos listaProductos;

        // Constructor del formulario.
        public FormProductos(EstadoAplicacion estado)
        {
            InitializeComponent();
            listaProductos = estado.ProductosRegistrados;

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

        // Acepta coma o punto decimal sin interpretar la coma como separador de millares.
        private static bool LeerPrecio(string texto, out decimal precio)
        {
            string valor = texto.Trim().Replace(',', '.');
            return decimal.TryParse(valor, NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture, out precio);
        }

        // Registra un producto nuevo.
        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) || string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el código y el nombre del producto.");
                return;
            }
            if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad < 0)
            {
                MessageBox.Show("Ingrese una cantidad no negativa.");
                return;
            }
            if (!LeerPrecio(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio mayor que cero.");
                return;
            }

            Producto producto = new Producto(
                txtCodigo.Text.Trim(),
                txtNombre.Text.Trim(),
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
            Producto? producto = listaProductos.Buscar(txtCodigo.Text.Trim());

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
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto.");
                return;
            }
            if (!LeerPrecio(txtPrecio.Text, out decimal precio) || precio <= 0)
            {
                MessageBox.Show("Ingrese un precio mayor que cero.");
                return;
            }

            if (listaProductos.Actualizar(
                txtCodigo.Text.Trim(),
                txtNombre.Text.Trim(),
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
