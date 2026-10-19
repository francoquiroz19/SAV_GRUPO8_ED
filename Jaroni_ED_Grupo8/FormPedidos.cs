
using Jaroni_ED_Grupo8;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Jaroni_ED_Grupo8
{
    public partial class FormPedidos : Form
    {
        // Objeto que administra la lista enlazada de pedidos.
        private ListaPedidos listaPedidos = new ListaPedidos();

        // Controles que permiten ingresar y mostrar los datos de los pedidos.
        private TextBox txtCodigo;
        private TextBox txtCliente;
        private TextBox txtProductos;
        private ComboBox cmbEstado;
        private DateTimePicker dtpFecha;
        private DataGridView dgvPedidos;

        public FormPedidos()
        {
            InitializeComponent();

            // Construye la interfaz gráfica del formulario.
            CrearInterfaz();
        }

        // Configura la ventana y crea todos los controles visuales.
        private void CrearInterfaz()
        {
            // Define el título, la posición y el tamaño de la ventana.
            Text = "JARONI - Gestión de pedidos";
            StartPosition = FormStartPosition.CenterScreen;
            Size = new Size(950, 650);
            MinimumSize = new Size(850, 550);

            // Crea el título principal del formulario.
            Label titulo = new Label
            {
                Text = "GESTIÓN DE PEDIDOS",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                Location = new Point(25, 20),
                AutoSize = true
            };
            Controls.Add(titulo);

            // Crea las etiquetas y los campos de entrada.
            CrearEtiqueta("Código:", 30, 85);
            txtCodigo = CrearTexto(130, 80, 180);

            CrearEtiqueta("Cliente:", 350, 85);
            txtCliente = CrearTexto(440, 80, 220);

            CrearEtiqueta("Productos:", 30, 130);
            txtProductos = CrearTexto(130, 125, 530);

            // Permite seleccionar la fecha del pedido.
            CrearEtiqueta("Fecha:", 30, 175);
            dtpFecha = new DateTimePicker
            {
                Location = new Point(130, 170),
                Width = 180,
                Format = DateTimePickerFormat.Short
            };
            Controls.Add(dtpFecha);

            // Permite seleccionar el estado del pedido.
            CrearEtiqueta("Estado:", 350, 175);
            cmbEstado = new ComboBox
            {
                Location = new Point(440, 170),
                Width = 220,

                // Impide escribir valores distintos de las opciones disponibles.
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Estados disponibles para los pedidos.
            cmbEstado.Items.AddRange(new object[]
            {
                "Pendiente",
                "En preparación",
                "Entregado"
            });

            // Selecciona "Pendiente" como estado inicial.
            cmbEstado.SelectedIndex = 0;
            Controls.Add(cmbEstado);

            // Crea los botones y vincula cada uno con su método correspondiente.
            CrearBoton("Registrar", 30, 225, RegistrarPedido);
            CrearBoton("Buscar", 150, 225, BuscarPedido);
            CrearBoton("Actualizar estado", 270, 225, ActualizarEstado);
            CrearBoton("Limpiar", 450, 225, LimpiarCampos);
            CrearBoton("Mostrar todos", 570, 225, MostrarPedidos);

            // Crea la tabla donde se visualizarán los pedidos.
            dgvPedidos = new DataGridView
            {
                Location = new Point(30, 285),
                Size = new Size(870, 290),

                // Permite ajustar la tabla cuando cambia el tamaño de la ventana.
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom
                    | AnchorStyles.Left | AnchorStyles.Right,

                // Evita editar directamente las celdas de la tabla.
                ReadOnly = true,
                AllowUserToAddRows = false,

                // Distribuye el ancho disponible entre las columnas.
                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                // Selecciona una fila completa a la vez.
                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };

            // Define las columnas que mostrarán los datos de cada pedido.
            dgvPedidos.Columns.Add("Codigo", "Código");
            dgvPedidos.Columns.Add("Cliente", "Cliente");
            dgvPedidos.Columns.Add("Productos", "Productos");
            dgvPedidos.Columns.Add("Fecha", "Fecha");
            dgvPedidos.Columns.Add("Estado", "Estado");

            // Agrega la tabla al formulario.
            Controls.Add(dgvPedidos);
        }

        // Crea una etiqueta con el texto y la posición indicados.
        private void CrearEtiqueta(string texto, int x, int y)
        {
            Controls.Add(new Label
            {
                Text = texto,
                Location = new Point(x, y),
                AutoSize = true,
                Font = new Font("Segoe UI", 10)
            });
        }

        // Crea un cuadro de texto y lo agrega al formulario.
        private TextBox CrearTexto(int x, int y, int ancho)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Width = ancho
            };

            Controls.Add(txt);
            return txt;
        }

        // Crea un botón y asigna el método que se ejecutará al hacer clic.
        private void CrearBoton(
            string texto, int x, int y, EventHandler accion)
        {
            Button boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(115, 35)
            };

            // Asocia el evento Click con la acción recibida.
            boton.Click += accion;
            Controls.Add(boton);
        }

        // Valida los datos ingresados y registra un nuevo pedido.
        private void RegistrarPedido(object sender, EventArgs e)
        {
            // Comprueba que el código sea un número entero positivo.
            if (!int.TryParse(txtCodigo.Text, out int codigo)
                || codigo <= 0)
            {
                MessageBox.Show("Ingresa un código numérico positivo.");
                return;
            }

            // Verifica que los campos obligatorios no estén vacíos.
            if (string.IsNullOrWhiteSpace(txtCliente.Text)
                || string.IsNullOrWhiteSpace(txtProductos.Text))
            {
                MessageBox.Show("Completa el cliente y los productos.");
                return;
            }

            // Evita registrar dos pedidos con el mismo código.
            if (listaPedidos.Buscar(codigo) != null)
            {
                MessageBox.Show("Ya existe un pedido con ese código.");
                return;
            }

            // Crea el objeto Pedido con los datos del formulario.
            Pedido pedido = new Pedido(
                codigo,
                txtCliente.Text.Trim(),
                txtProductos.Text.Trim(),
                dtpFecha.Value
            );

            // Inserta el pedido en la lista enlazada.
            listaPedidos.Agregar(pedido);

            // Establece el estado seleccionado por el usuario.
            listaPedidos.ActualizarEstado(codigo, cmbEstado.Text);

            // Actualiza la tabla y limpia los campos de entrada.
            MostrarPedidos(sender, e);
            LimpiarCampos(sender, e);

            MessageBox.Show("Pedido registrado correctamente.");
        }

        // Busca un pedido utilizando su código.
        private void BuscarPedido(object sender, EventArgs e)
        {
            // Comprueba que se haya ingresado un código válido.
            if (!int.TryParse(txtCodigo.Text, out int codigo))
            {
                MessageBox.Show("Ingresa el código del pedido que buscas.");
                return;
            }

            // Busca el pedido dentro de la lista enlazada.
            Pedido pedido = listaPedidos.Buscar(codigo);

            // Informa si el pedido no existe.
            if (pedido == null)
            {
                MessageBox.Show("No se encontró el pedido.");
                return;
            }

            // Muestra los datos encontrados en los controles.
            txtCliente.Text = pedido.Cliente;
            txtProductos.Text = pedido.Productos;
            dtpFecha.Value = pedido.Fecha;
            cmbEstado.SelectedItem = pedido.Estado;

            // Muestra únicamente el pedido encontrado en la tabla.
            MostrarUnPedido(pedido);
        }

        // Actualiza el estado de un pedido existente.
        private void ActualizarEstado(object sender, EventArgs e)
        {
            // Verifica que el código ingresado sea válido.
            if (!int.TryParse(txtCodigo.Text, out int codigo))
            {
                MessageBox.Show("Ingresa el código del pedido.");
                return;
            }

            // Intenta modificar el estado del pedido en la lista.
            bool actualizado =
                listaPedidos.ActualizarEstado(codigo, cmbEstado.Text);

            // Si no se encontró el código, informa al usuario.
            if (!actualizado)
            {
                MessageBox.Show("No se encontró el pedido.");
                return;
            }

            // Actualiza la visualización de los pedidos.
            MostrarPedidos(sender, e);
            MessageBox.Show("Estado actualizado correctamente.");
        }

        // Limpia la tabla y solicita mostrar todos los pedidos.
        private void MostrarPedidos(object sender, EventArgs e)
        {
            dgvPedidos.Rows.Clear();

            // IMPORTANTE:
            // MostrarAdelante() debe permitir obtener o mostrar los datos
            // en el DataGridView. Si solo recorre la lista por consola,
            // la tabla permanecerá vacía.
            // Revisaremos ListaPedidos.cs para completar esta parte.
            listaPedidos.MostrarAdelante();
        }

        // Muestra un único pedido en la tabla.
        private void MostrarUnPedido(Pedido pedido)
        {
            // Elimina las filas anteriores para evitar mezclar resultados.
            dgvPedidos.Rows.Clear();

            // Agrega los datos del pedido encontrado.
            dgvPedidos.Rows.Add(
                pedido.Codigo,
                pedido.Cliente,
                pedido.Productos,
                pedido.Fecha.ToShortDateString(),
                pedido.Estado
            );
        }

        // Restablece los controles a sus valores iniciales.
        private void LimpiarCampos(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCliente.Clear();
            txtProductos.Clear();

            // Restablece la fecha y el estado predeterminado.
            dtpFecha.Value = DateTime.Today;
            cmbEstado.SelectedIndex = 0;

            // Devuelve el cursor al campo del código.
            txtCodigo.Focus();
        }
    }
}
