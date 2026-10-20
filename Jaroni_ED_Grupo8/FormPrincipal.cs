
using Jaroni_ED_Grupo8;
using JaroniApp;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace JaroniApp
{
    public class FormPrincipal : Form
    {
        public FormPrincipal()
        {
            Text = "Sistema de Gestión";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            var titulo = new Label
            {
                Text = "SISTEMA DE GESTIÓN",
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(155, 35)
            };
            Controls.Add(titulo);

            AgregarBoton("Productos", 100, 110, AbrirProductos);
            AgregarBoton("Pedidos", 320, 110, AbrirPedidos);
            AgregarBoton("Ventas", 100, 200, AbrirVentas);
            AgregarBoton("Historial", 320, 200, AbrirHistorial);
        }

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
            new FormProductos().ShowDialog();
        }

        private void AbrirPedidos(object? sender, EventArgs e)
        {
            new FormPedidos().ShowDialog();
        }

        private void AbrirVentas(object? sender, EventArgs e)
        {
            new FormHistorial().ShowDialog();
        }

        private void AbrirHistorial(object? sender, EventArgs e)
        {
            new FormHistorial().ShowDialog();
        }
    }
}
