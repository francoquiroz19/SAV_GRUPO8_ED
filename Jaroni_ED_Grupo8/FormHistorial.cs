
namespace JaroniApp
{
    // Formulario que permite registrar y consultar el historial de acciones
    public class FormHistorial : Form
    {
        // Pila donde se almacenan las acciones registradas
        private readonly PilaHistorial historial;

        // Controles que utilizaremos en el formulario
        private readonly Label lblModulo = new Label();
        private readonly ComboBox cmbModulo = new ComboBox();
        private readonly Label lblDescripcion = new Label();
        private readonly TextBox txtDescripcion = new TextBox();

        // Botones para realizar las operaciones de la pila
        private readonly Button btnRegistrar = new Button();
        private readonly Button btnVerUltima = new Button();
        private readonly Button btnRetirarUltima = new Button();
        private readonly Button btnLimpiar = new Button();

        // Controles para mostrar las acciones registradas
        private readonly Label lblUltimaAccion = new Label();
        private readonly ListBox lstHistorial = new ListBox();
        private readonly Label lblCantidad = new Label();

        // Constructor que crea el formulario con una pila nueva
        public FormHistorial() : this(new PilaHistorial())
        {
        }

        // Constructor que recibe la pila que vamos a utilizar
        public FormHistorial(PilaHistorial historial)
        {
            this.historial = historial;

            // Configuramos el título, tamaño y posición del formulario
            Text = "Historial de acciones";
            Size = new Size(640, 520);
            StartPosition = FormStartPosition.CenterScreen;

            // Organiza los controles y muestra el historial
            InicializarControles();
            RefrescarHistorial();
        }

        // Método que configura los controles del formulario
        private void InicializarControles()
        {
            // Configura la etiqueta del módulo
            lblModulo.Text = "Módulo:";
            lblModulo.Location = new Point(20, 24);
            lblModulo.Size = new Size(60, 23);

            // Configura la lista de módulos disponibles
            cmbModulo.Location = new Point(90, 20);
            cmbModulo.Size = new Size(180, 23);
            cmbModulo.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbModulo.Items.AddRange(new object[] { "Productos", "Pedidos", "Clientes", "Proveedores", "Historial" });
            cmbModulo.SelectedIndex = 0;

            // Configura la etiqueta de descripción
            lblDescripcion.Text = "Descripción:";
            lblDescripcion.Location = new Point(20, 58);
            lblDescripcion.Size = new Size(70, 23);

            // Configura el cuadro donde se escribe la acción
            txtDescripcion.Location = new Point(90, 54);
            txtDescripcion.Size = new Size(510, 23);
            txtDescripcion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Botón para registrar una nueva acción (Push)
            btnRegistrar.Text = "Registrar acción";
            btnRegistrar.Location = new Point(20, 94);
            btnRegistrar.Size = new Size(130, 30);
            btnRegistrar.Click += BtnRegistrar_Click;

            // Botón para consultar la última acción (Peek)
            btnVerUltima.Text = "Ver última";
            btnVerUltima.Location = new Point(160, 94);
            btnVerUltima.Size = new Size(110, 30);
            btnVerUltima.Click += BtnVerUltima_Click;

            // Botón para retirar la última acción (Pop)
            btnRetirarUltima.Text = "Retirar última";
            btnRetirarUltima.Location = new Point(280, 94);
            btnRetirarUltima.Size = new Size(120, 30);
            btnRetirarUltima.Click += BtnRetirarUltima_Click;

            // Botón para eliminar todas las acciones
            btnLimpiar.Text = "Limpiar historial";
            btnLimpiar.Location = new Point(410, 94);
            btnLimpiar.Size = new Size(130, 30);
            btnLimpiar.Click += BtnLimpiar_Click;

            // Muestra la última acción registrada
            lblUltimaAccion.Location = new Point(20, 140);
            lblUltimaAccion.Size = new Size(580, 25);
            lblUltimaAccion.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblUltimaAccion.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

            // Lista donde se mostrarán todas las acciones
            lstHistorial.Location = new Point(20, 175);
            lstHistorial.Size = new Size(580, 260);
            lstHistorial.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            // Muestra la cantidad de acciones
            lblCantidad.Location = new Point(20, 445);
            lblCantidad.Size = new Size(250, 23);
            lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Agrega todos los controles al formulario
            Controls.Add(lblModulo);
            Controls.Add(cmbModulo);
            Controls.Add(lblDescripcion);
            Controls.Add(txtDescripcion);
            Controls.Add(btnRegistrar);
            Controls.Add(btnVerUltima);
            Controls.Add(btnRetirarUltima);
            Controls.Add(btnLimpiar);
            Controls.Add(lblUltimaAccion);
            Controls.Add(lstHistorial);
            Controls.Add(lblCantidad);
        }

        // Actualiza la lista de acciones que se muestra en pantalla
        private void RefrescarHistorial()
        {
            // Limpia la lista para evitar datos repetidos
            lstHistorial.Items.Clear();

            // Recorre las acciones desde la más reciente
            foreach (Accion accion in historial.ObtenerTodas())
            {
                lstHistorial.Items.Add(accion);
            }

            // Muestra la cantidad de acciones registradas
            lblCantidad.Text = $"Total de acciones: {historial.Cantidad}";

            // Verifica si existen acciones en la pila
            if (historial.EstaVacia)
            {
                lblUltimaAccion.Text = "Última acción: (ninguna)";
            }
            else
            {
                // Muestra la última acción sin retirarla
                lblUltimaAccion.Text = $"Última acción: {historial.Peek()}";
            }
        }

        // Evento del botón Registrar acción
        private void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            // Obtenemos el texto ingresado
            string descripcion = txtDescripcion.Text.Trim();

            // Verifica que la descripción no esté vacía
            if (string.IsNullOrEmpty(descripcion))
            {
                MessageBox.Show("Ingrese la descripción de la acción.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Registramos la acción en el módulo seleccionado
            historial.Registrar(cmbModulo.Text, descripcion);

            // Limpia el cuadro de texto
            txtDescripcion.Clear();

            // Actualiza el historial mostrado
            RefrescarHistorial();
        }

        // Evento del botón Ver última acción
        private void BtnVerUltima_Click(object? sender, EventArgs e)
        {
            // Verifica si la pila está vacía
            if (historial.EstaVacia)
            {
                MessageBox.Show("El historial está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Consulta la última acción sin eliminarla
            MessageBox.Show(historial.Peek().ToString(), "Última acción", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento del botón Retirar última acción
        private void BtnRetirarUltima_Click(object? sender, EventArgs e)
        {
            // Verifica si existen acciones para retirar
            if (historial.EstaVacia)
            {
                MessageBox.Show("No hay acciones para retirar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Retira la última acción de la pila
            Accion retirada = historial.Pop();

            // Actualiza el historial
            RefrescarHistorial();

            // Muestra la acción que fue retirada
            MessageBox.Show(retirada.ToString(), "Acción retirada", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Evento del botón Limpiar historial
        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            // Elimina todas las acciones de la pila
            historial.Limpiar();

            // Actualiza la información del formulario
            RefrescarHistorial();
        }
    }
}
