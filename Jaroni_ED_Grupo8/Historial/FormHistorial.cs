using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace Jaroni_ED_Grupo8
{
    // Muestra las ventas pendientes almacenadas en una pila enlazada.
    public class FormHistorial : Form
    {
        private readonly EstadoAplicacion estado;
        private readonly PilaHistorial historial;
        private static readonly CultureInfo CulturaSoles = CultureInfo.GetCultureInfo("es-PE");

        private readonly DataGridView tabla = new();
        private readonly Button btnVerUltima = new();
        private readonly Button btnRetirarUltima = new();
        private readonly Button btnVerPendientes = new();
        private readonly Button btnReiniciar = new();
        private readonly Label lblUltima = new();
        private readonly Label lblCantidad = new();
        private readonly Label lblRevisadas = new();
        private readonly Label lblRegistradas = new();
        private readonly Label lblEstado = new();

        public FormHistorial(EstadoAplicacion estado)
        {
            this.estado = estado;
            historial = estado.VentasPendientesRevision;
            Text = "JARONI - PANEL DEL HISTORIAL DE VENTAS";
            Size = new Size(780, 570);
            MinimumSize = new Size(780, 570);
            StartPosition = FormStartPosition.CenterParent;

            InicializarControles();
            ActualizarPresentacion();
        }

        private static string Soles(decimal importe) => "S/ " + importe.ToString("N2", CulturaSoles);

        private void InicializarControles()
        {
            Controls.Add(new Label
            {
                Text = "HISTORIAL DE VENTAS",
                Font = new Font(Font, FontStyle.Bold),
                Location = new Point(20, 16),
                Size = new Size(300, 26)
            });
            Controls.Add(new Label
            {
                Text = "Ventas pendientes de revisión",
                Location = new Point(20, 47),
                Size = new Size(420, 22)
            });

            tabla.Location = new Point(20, 82);
            tabla.Size = new Size(724, 295);
            tabla.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.AllowUserToDeleteRows = false;
            tabla.AllowUserToResizeRows = false;
            tabla.MultiSelect = false;
            tabla.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            tabla.RowHeadersVisible = false;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Código", FillWeight = 12F });
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Fecha y hora", FillWeight = 23F });
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Productos", FillWeight = 25F });
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Cantidad", FillWeight = 12F });
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Precio unitario", FillWeight = 15F });
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Total", FillWeight = 13F });
            Controls.Add(tabla);

            ConfigurarBoton(btnVerUltima, "Consultar última venta", 20, 170, BtnVerUltima_Click);
            ConfigurarBoton(btnRetirarUltima, "Marcar última como revisada", 200, 210, BtnRetirarUltima_Click);
            ConfigurarBoton(btnVerPendientes, "Ver ventas pendientes", 420, 160, BtnVerPendientes_Click);
            ConfigurarBoton(btnReiniciar, "Reiniciar revisión", 590, 155, BtnReiniciar_Click);

            lblUltima.Location = new Point(20, 439);
            lblUltima.Size = new Size(220, 23);
            lblUltima.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(lblUltima);

            lblCantidad.Location = new Point(250, 439);
            lblCantidad.Size = new Size(240, 23);
            lblCantidad.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(lblCantidad);

            lblRevisadas.Location = new Point(500, 439);
            lblRevisadas.Size = new Size(245, 23);
            lblRevisadas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(lblRevisadas);

            lblRegistradas.Location = new Point(20, 472);
            lblRegistradas.Size = new Size(250, 23);
            lblRegistradas.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            Controls.Add(lblRegistradas);

            lblEstado.Location = new Point(20, 500);
            lblEstado.Size = new Size(720, 23);
            lblEstado.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Controls.Add(lblEstado);
        }

        private void ConfigurarBoton(Button boton, string texto, int x, int ancho, EventHandler accion)
        {
            boton.Text = texto;
            boton.Location = new Point(x, 394);
            boton.Size = new Size(ancho, 30);
            boton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            boton.Click += accion;
            Controls.Add(boton);
        }

        // Refleja el estado de la pila en la tabla y en los indicadores.
        private void ActualizarPresentacion()
        {
            tabla.Rows.Clear();
            // La grilla se reconstruye recorriendo los nodos desde el tope hacia la base.
            historial.Recorrer(venta =>
            {
                string precio = "Varios";
                if (venta.PrecioUnitario.HasValue)
                    precio = Soles(venta.PrecioUnitario.Value);
                tabla.Rows.Add(venta.Codigo, venta.FechaHora.ToString("dd/MM/yyyy HH:mm:ss"),
                    venta.Productos, venta.CantidadTotal, precio, Soles(venta.Total));
            });

            if (historial.EstaVacia)
                lblUltima.Text = "Última venta pendiente: Ninguna";
            else
                lblUltima.Text = "Última venta pendiente: " + historial.Top().Codigo;
            lblCantidad.Text = "Ventas pendientes de revisión: " + historial.Longitud;
            lblRevisadas.Text = "Ventas revisadas en esta sesión: " + estado.VentasRevisadasEnSesion;
            lblRegistradas.Text = "Ventas registradas: " + estado.VentasRegistradas.Longitud;
            btnReiniciar.Enabled = estado.VentasRegistradas.Longitud > 0;
            tabla.ClearSelection();
        }

        // Consulta la venta superior sin retirarla.
        private void BtnVerUltima_Click(object? sender, EventArgs e)
        {
            if (historial.EstaVacia)
            {
                MessageBox.Show(this, "No hay ventas pendientes de revisión.",
                    "Consultar última venta", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Top solo consulta: la venta continúa en la cima y Longitud no cambia.
            VentaRegistrada ultima = historial.Top();
            string resumen = $"Código: {ultima.Codigo}\nFecha: {ultima.FechaHora:dd/MM/yyyy HH:mm}\n";
            ultima.RecorrerArticulos(articulo =>
                resumen += $"{articulo.Cantidad} × {articulo.Nombre} — {Soles(articulo.PrecioUnitario)}\n");
            resumen += "Total: " + Soles(ultima.Total);
            MessageBox.Show(this, resumen, "Última venta pendiente",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Marca como revisada solo la venta situada en el tope.
        private void BtnRetirarUltima_Click(object? sender, EventArgs e)
        {
            if (historial.EstaVacia)
            {
                lblEstado.Text = "No hay ventas pendientes para marcar como revisadas.";
                return;
            }

            // Pop solo retira el tope de esta pila temporal; no anula una venta real ni toca Inventario.
            var retirada = historial.Pop();
            estado.RegistrarRevision();
            ActualizarPresentacion();
            lblEstado.Text = $"Venta {retirada.Codigo} revisada y retirada de pendientes.";
        }

        // Vuelve a mostrar las ventas pendientes desde el tope.
        private void BtnVerPendientes_Click(object? sender, EventArgs e)
        {
            // Recorrer lee cada nodo desde el tope sin modificar la pila.
            ActualizarPresentacion();
            if (historial.EstaVacia)
                lblEstado.Text = "No hay ventas pendientes de revisión.";
            else
                lblEstado.Text = $"Se muestran {historial.Longitud} ventas pendientes, de la más reciente a la más antigua.";
        }

        // Reconstruye pendientes solo con ventas confirmadas en esta sesión.
        private void BtnReiniciar_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Volver a marcar como pendientes las ventas registradas en esta sesión?",
                    "Confirmar reinicio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Clear y Push restauran las ventas reales sin duplicarlas en el registro general.
            estado.ReiniciarRevision();
            ActualizarPresentacion();
            lblEstado.Text = "Revisión reiniciada con las ventas registradas en esta sesión.";
        }
    }
}
