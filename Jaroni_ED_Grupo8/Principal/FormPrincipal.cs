
using Jaroni_ED_Grupo8;
using JaroniApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JaroniApp
{
    // Menú inicial que abre cada módulo en su propio formulario.
    public partial class FormPrincipal : Form
    {
        private readonly EstadoAplicacion estado;

        public FormPrincipal(EstadoAplicacion estado)
        {
            InitializeComponent();
            this.estado = estado;
            Text = "JARONI - PANEL PRINCIPAL DE GESTION COMECIAL";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            var titulo = new Label
            {
                Text = "JARONI",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(155, 35)
            };
            Controls.Add(titulo);

            AgregarBoton("Productos", 100, 105, AbrirProductos);
            AgregarBoton("Pedidos", 320, 105, AbrirPedidos);
            AgregarBoton("Ventas", 100, 175, AbrirVentas);
            AgregarBoton("Clientes", 320, 175, AbrirClientes);
            AgregarBoton("Historial", 210, 245, AbrirHistorial);
        }

        // Crea un acceso al módulo y conecta su acción de apertura.
        private void AgregarBoton(string texto, int x, int y, EventHandler accion)
        {
            var boton = new Button
            {
                Text = texto,
                Location = new Point(x, y),
                Size = new Size(170, 55),
                Font = new Font("Segoe UI", 11)
            };

            boton.Click += accion;
            Controls.Add(boton);
        }

        private void AbrirProductos(object? sender, EventArgs e)
        {
            new FormProductos(estado).ShowDialog();
        }

        private void AbrirPedidos(object? sender, EventArgs e)
        {
            new FormPedidos(estado).ShowDialog();
        }

        private void AbrirVentas(object? sender, EventArgs e)
        {
            new FormVentas(estado).ShowDialog(this);
        }

        private void AbrirClientes(object? sender, EventArgs e)
        {
            new FormClientes(estado).ShowDialog(this);
        }

        private void AbrirHistorial(object? sender, EventArgs e)
        {
            new FormHistorial(estado).ShowDialog();
        }
    }
}
