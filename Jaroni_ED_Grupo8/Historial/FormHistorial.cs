using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using JaroniApp;

namespace Jaroni_ED_Grupo8
{
    // Muestra las ventas pendientes almacenadas en una pila enlazada.
    public class FormHistorial : Form
    {
        private const int RegistrosIniciales = 10;
        private readonly PilaHistorial historial = new();
        private static readonly CultureInfo CulturaSoles = CultureInfo.GetCultureInfo("es-PE");

        private readonly DataGridView tabla = new();
        private readonly Button btnVerUltima = new();
        private readonly Button btnRetirarUltima = new();
        private readonly Button btnVerPendientes = new();
        private readonly Button btnReiniciar = new();
        private readonly Label lblUltima = new();
        private readonly Label lblCantidad = new();
        private readonly Label lblRevisadas = new();
        private readonly Label lblEstado = new();

        public FormHistorial()
        {
            Text = "Historial de ventas";
            Size = new Size(780, 570);
            MinimumSize = new Size(780, 570);
            StartPosition = FormStartPosition.CenterParent;

            InicializarControles();
            CargarVentasIniciales();
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
            tabla.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Producto", FillWeight = 25F });
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

            lblEstado.Location = new Point(20, 472);
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

        // Carga diez registros fijos para la revisión en memoria.
        private void CargarVentasIniciales()
        {
            // Diez Push en orden cronológico: V010 termina en el tope de la pila.
            // Las fechas son fijas; actualizar la tabla no cambia los registros.
            historial.Push(new VentaHistorialDemo("V001", new DateTime(2026, 9, 1, 9, 0, 0), "Mouse óptico", 1, 35.90m));
            historial.Push(new VentaHistorialDemo("V002", new DateTime(2026, 9, 2, 9, 0, 0), "Teclado", 2, 60.00m));
            historial.Push(new VentaHistorialDemo("V003", new DateTime(2026, 9, 3, 9, 0, 0), "Tinta para impresora", 1, 45.00m));
            historial.Push(new VentaHistorialDemo("V004", new DateTime(2026, 9, 4, 9, 0, 0), "Papel bond A4", 3, 25.00m));
            historial.Push(new VentaHistorialDemo("V005", new DateTime(2026, 9, 5, 9, 0, 0), "Memoria USB", 2, 30.00m));
            historial.Push(new VentaHistorialDemo("V006", new DateTime(2026, 9, 6, 9, 0, 0), "Cuaderno A4", 4, 10.00m));
            historial.Push(new VentaHistorialDemo("V007", new DateTime(2026, 9, 7, 9, 0, 0), "Lapicero azul", 5, 2.50m));
            historial.Push(new VentaHistorialDemo("V008", new DateTime(2026, 9, 8, 9, 0, 0), "Cable HDMI", 1, 28.00m));
            historial.Push(new VentaHistorialDemo("V009", new DateTime(2026, 9, 9, 9, 0, 0), "Cartucho de tinta", 2, 75.00m));
            historial.Push(new VentaHistorialDemo("V010", new DateTime(2026, 9, 10, 9, 0, 0), "Audífonos", 1, 85.00m));
        }

        // Refleja el estado de la pila en la tabla y en los indicadores.
        private void ActualizarPresentacion()
        {
            tabla.Rows.Clear();
            // La grilla se reconstruye recorriendo los nodos desde el tope hacia la base.
            historial.Recorrer(venta => tabla.Rows.Add(venta.Codigo,
                venta.FechaHora.ToString("dd/MM/yyyy HH:mm:ss"), venta.Producto,
                venta.Cantidad, Soles(venta.PrecioUnitario), Soles(venta.Total)));

            lblUltima.Text = historial.EstaVacia
                ? "Última venta pendiente: Ninguna"
                : "Última venta pendiente: " + historial.Top().Codigo;
            lblCantidad.Text = "Ventas pendientes de revisión: " + historial.Longitud;
            lblRevisadas.Text = "Ventas revisadas en esta sesión: " + (RegistrosIniciales - historial.Longitud);
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
            var ultima = historial.Top();
            MessageBox.Show(this,
                $"Código: {ultima.Codigo}\nProducto: {ultima.Producto}\nCantidad: {ultima.Cantidad}\nTotal: {Soles(ultima.Total)}",
                "Última venta pendiente", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            ActualizarPresentacion();
            lblEstado.Text = $"Venta {retirada.Codigo} revisada y retirada de pendientes.";
        }

        // Vuelve a mostrar las ventas pendientes desde el tope.
        private void BtnVerPendientes_Click(object? sender, EventArgs e)
        {
            // Recorrer lee cada nodo desde el tope sin modificar la pila.
            ActualizarPresentacion();
            lblEstado.Text = historial.EstaVacia
                ? "No hay ventas pendientes de revisión."
                : $"Se muestran {historial.Longitud} ventas pendientes, de la más reciente a la más antigua.";
        }

        // Vacía y vuelve a cargar los registros iniciales para repetir la revisión.
        private void BtnReiniciar_Click(object? sender, EventArgs e)
        {
            if (MessageBox.Show(this, "¿Restaurar los 10 registros iniciales y reiniciar la revisión?",
                    "Confirmar reinicio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            // Clear evita duplicados; los diez registros vuelven a entrar mediante Push.
            historial.Clear();
            CargarVentasIniciales();
            ActualizarPresentacion();
            lblEstado.Text = "Revisión reiniciada: las 10 ventas vuelven a estar pendientes.";
        }
    }
}
