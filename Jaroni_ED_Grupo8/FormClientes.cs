namespace Jaroni_ED_Grupo8
{

    // Formulario que permite registrar, consultar, actualizar y eliminar clientes
    public class FormClientes : Form
    {
        // ---------- LISTA Y VARIABLES DE ESTADO ----------

        // Lista donde se almacenan los clientes
        private readonly ListaClientes lista = new ListaClientes();

        // ---------- CONTROLES DEL FORMULARIO ----------
        private readonly Label lblCodigo = new Label();
        private readonly NumericUpDown numCodigo = new NumericUpDown();
        private readonly Label lblNombre = new Label();
        private readonly TextBox txtNombre = new TextBox();
        private readonly Label lblDocumento = new Label();
        private readonly TextBox txtDocumento = new TextBox();
        private readonly Label lblTelefono = new Label();
        private readonly TextBox txtTelefono = new TextBox();
        private readonly Label lblCorreo = new Label();
        private readonly TextBox txtCorreo = new TextBox();
        private readonly Label lblDireccion = new Label();
        private readonly TextBox txtDireccion = new TextBox();

        private readonly Button btnRegistrar = new Button();
        private readonly Button btnBuscar = new Button();
        private readonly Button btnActualizar = new Button();
        private readonly Button btnEliminar = new Button();
        private readonly Button btnLimpiar = new Button();
        private readonly Button btnListar = new Button();

        private readonly ListBox lstClientes = new ListBox();
        private readonly Label lblInfo = new Label();

        // Constructor del formulario
        public FormClientes()
        {
            // Configuramos el título, tamaño y posición del formulario
            Text = "Gestor de Clientes (Lista Enlazada)";
            Size = new Size(640, 560);
            StartPosition = FormStartPosition.CenterScreen;

            // Crea los controles y carga clientes de ejemplo
            InicializarControles();
            CargarDatosEjemplo();
            RefrescarLista();
        }

        // ---------- CREACIÓN DE CONTROLES (todo en código) ----------
        private void InicializarControles()
        {
            // Código del cliente
            lblCodigo.Text = "Código:";
            lblCodigo.Location = new Point(20, 24);
            lblCodigo.Size = new Size(70, 23);

            numCodigo.Location = new Point(100, 20);
            numCodigo.Size = new Size(80, 23);
            numCodigo.Minimum = 1;
            numCodigo.Maximum = 99999;
            numCodigo.Value = 1;

            // Nombre o razón social
            lblNombre.Text = "Nombre:";
            lblNombre.Location = new Point(20, 58);
            lblNombre.Size = new Size(70, 23);

            txtNombre.Location = new Point(100, 54);
            txtNombre.Size = new Size(240, 23);

            // Documento
            lblDocumento.Text = "Documento:";
            lblDocumento.Location = new Point(20, 92);
            lblDocumento.Size = new Size(75, 23);

            txtDocumento.Location = new Point(100, 88);
            txtDocumento.Size = new Size(240, 23);

            // Teléfono
            lblTelefono.Text = "Teléfono:";
            lblTelefono.Location = new Point(20, 126);
            lblTelefono.Size = new Size(70, 23);

            txtTelefono.Location = new Point(100, 122);
            txtTelefono.Size = new Size(240, 23);

            // Correo
            lblCorreo.Text = "Correo:";
            lblCorreo.Location = new Point(20, 160);
            lblCorreo.Size = new Size(70, 23);

            txtCorreo.Location = new Point(100, 156);
            txtCorreo.Size = new Size(240, 23);

            // Dirección
            lblDireccion.Text = "Dirección:";
            lblDireccion.Location = new Point(20, 194);
            lblDireccion.Size = new Size(70, 23);

            txtDireccion.Location = new Point(100, 190);
            txtDireccion.Size = new Size(240, 23);

            // Botones de operaciones
            btnRegistrar.Text = "Registrar";
            btnRegistrar.Location = new Point(20, 235);
            btnRegistrar.Size = new Size(90, 30);
            btnRegistrar.Click += BtnRegistrar_Click;

            btnBuscar.Text = "Buscar por Código";
            btnBuscar.Location = new Point(120, 235);
            btnBuscar.Size = new Size(120, 30);
            btnBuscar.Click += BtnBuscar_Click;

            btnActualizar.Text = "Actualizar";
            btnActualizar.Location = new Point(250, 235);
            btnActualizar.Size = new Size(90, 30);
            btnActualizar.Click += BtnActualizar_Click;

            btnEliminar.Text = "Eliminar";
            btnEliminar.Location = new Point(350, 235);
            btnEliminar.Size = new Size(80, 30);
            btnEliminar.Click += BtnEliminar_Click;

            btnLimpiar.Text = "Limpiar campos";
            btnLimpiar.Location = new Point(440, 235);
            btnLimpiar.Size = new Size(110, 30);
            btnLimpiar.Click += BtnLimpiar_Click;

            btnListar.Text = "Listar Todos";
            btnListar.Location = new Point(20, 275);
            btnListar.Size = new Size(100, 30);
            btnListar.Click += BtnListar_Click;

            // Lista donde se muestran los clientes (clic = cargar sus datos en los campos)
            lstClientes.Location = new Point(20, 320);
            lstClientes.Size = new Size(580, 150);
            lstClientes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            lstClientes.SelectedIndexChanged += LstClientes_SelectedIndexChanged;

            // Etiqueta de información
            lblInfo.Text = "Total clientes: 0";
            lblInfo.Location = new Point(20, 485);
            lblInfo.Size = new Size(250, 23);
            lblInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;

            // Agrega todos los controles al formulario
            Controls.Add(lblCodigo);
            Controls.Add(numCodigo);
            Controls.Add(lblNombre);
            Controls.Add(txtNombre);
            Controls.Add(lblDocumento);
            Controls.Add(txtDocumento);
            Controls.Add(lblTelefono);
            Controls.Add(txtTelefono);
            Controls.Add(lblCorreo);
            Controls.Add(txtCorreo);
            Controls.Add(lblDireccion);
            Controls.Add(txtDireccion);
            Controls.Add(btnRegistrar);
            Controls.Add(btnBuscar);
            Controls.Add(btnActualizar);
            Controls.Add(btnEliminar);
            Controls.Add(btnLimpiar);
            Controls.Add(btnListar);
            Controls.Add(lstClientes);
            Controls.Add(lblInfo);
        }

        // ---------- MÉTODOS AUXILIARES ----------

        // Agrega algunos clientes de ejemplo al iniciar
        private void CargarDatosEjemplo()
        {
            lista.Agregar(new Cliente(1, "Ana Pérez", "45871236", "987654321", "ana@correo.com", "Av. Lima 123"));
            lista.Agregar(new Cliente(2, "Distribuidora Sol SAC", "20456789123", "014567890", "ventas@sol.com", "Jr. Cusco 456"));
            lista.Agregar(new Cliente(3, "Luis Gómez", "70123456", "955123456", "luis@correo.com", "Calle Los Pinos 789"));
        }

        // Actualiza el ListBox con todos los clientes y el total
        private void RefrescarLista()
        {
            lstClientes.Items.Clear();

            foreach (Cliente cliente in lista.ObtenerTodos())
            {
                lstClientes.Items.Add(cliente);
            }

            lblInfo.Text = $"Total clientes: {lista.Count}";
        }

        // Limpia los campos de texto y sugiere el siguiente código libre
        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDocumento.Clear();
            txtTelefono.Clear();
            txtCorreo.Clear();
            txtDireccion.Clear();

            // Busca el primer código que no esté usado
            int sugerido = (int)numCodigo.Value;
            while (lista.ExisteCodigo(sugerido) && sugerido < (int)numCodigo.Maximum)
                sugerido++;
            numCodigo.Value = sugerido;
        }

        // Muestra en los campos los datos de un cliente
        private void CargarCampos(Cliente cliente)
        {
            numCodigo.Value = cliente.Codigo;
            txtNombre.Text = cliente.Nombre;
            txtDocumento.Text = cliente.Documento;
            txtTelefono.Text = cliente.Telefono;
            txtCorreo.Text = cliente.Correo;
            txtDireccion.Text = cliente.Direccion;
        }

        // Verifica que los datos obligatorios estén completos
        private bool DatosValidos()
        {
            if (string.IsNullOrEmpty(txtNombre.Text.Trim()) ||
                string.IsNullOrEmpty(txtDocumento.Text.Trim()) ||
                string.IsNullOrEmpty(txtTelefono.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar nombre, documento y teléfono.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        // ---------- EVENTOS DE BOTONES ----------

        // Registrar un nuevo cliente
        private void BtnRegistrar_Click(object? sender, EventArgs e)
        {
            if (!DatosValidos())
                return;

            int codigo = (int)numCodigo.Value;

            // Valida que el código no esté repetido
            if (lista.ExisteCodigo(codigo))
            {
                MessageBox.Show($"Ya existe un cliente con el código {codigo}.", "Código repetido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Cliente nuevo = new Cliente(codigo, txtNombre.Text.Trim(), txtDocumento.Text.Trim(),
                txtTelefono.Text.Trim(), txtCorreo.Text.Trim(), txtDireccion.Text.Trim());
            lista.Agregar(nuevo);

            RefrescarLista();
            LimpiarCampos();
            MessageBox.Show("Cliente registrado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Buscar un cliente por su código
        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            Cliente? encontrado = lista.Buscar((int)numCodigo.Value);

            if (encontrado == null)
            {
                MessageBox.Show("No se encontró ningún cliente con ese código.", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Carga los datos en los campos y selecciona el cliente en la lista
            CargarCampos(encontrado);
            lstClientes.SelectedItem = encontrado;
            MessageBox.Show($"Cliente encontrado: {encontrado}", "Búsqueda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Actualizar los datos del cliente con el código indicado
        private void BtnActualizar_Click(object? sender, EventArgs e)
        {
            if (!DatosValidos())
                return;

            int codigo = (int)numCodigo.Value;
            bool actualizado = lista.Actualizar(codigo, txtNombre.Text.Trim(), txtDocumento.Text.Trim(),
                txtTelefono.Text.Trim(), txtCorreo.Text.Trim(), txtDireccion.Text.Trim());

            if (actualizado)
            {
                RefrescarLista();
                MessageBox.Show("Cliente actualizado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("No existe un cliente con ese código.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar el cliente con el código indicado
        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            int codigo = (int)numCodigo.Value;
            Cliente? cliente = lista.Buscar(codigo);

            if (cliente == null)
            {
                MessageBox.Show("No existe un cliente con ese código.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Pide confirmación antes de eliminar
            DialogResult respuesta = MessageBox.Show($"¿Eliminar a {cliente.Nombre}?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (respuesta != DialogResult.Yes)
                return;

            lista.Eliminar(codigo);
            RefrescarLista();
            LimpiarCampos();
            MessageBox.Show("Cliente eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Limpiar los campos de texto
        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            LimpiarCampos();
        }

        // Listar todos los clientes (fuerza el refresco del ListBox)
        private void BtnListar_Click(object? sender, EventArgs e)
        {
            RefrescarLista();
        }

        // Al seleccionar un cliente en la lista, se cargan sus datos en los campos
        private void LstClientes_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (lstClientes.SelectedItem is Cliente seleccionado)
                CargarCampos(seleccionado);
        }
    }
}