
using System;

namespace Jaroni_ED_Grupo8
{
    // Contiene los datos básicos y el estado de un pedido.
    public class Pedido
    {
        public int Codigo { get; set; }
        public string Cliente { get; set; }
        public string Productos { get; set; }
        public int Cantidad { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // Inicializa el pedido con sus datos de registro.
        public Pedido(int codigo, string cliente, string productos, DateTime fecha)
        {
            Codigo = codigo;
            Cliente = cliente;
            Productos = productos;
            Fecha = fecha;

            Estado = "Pendiente";
        }

        public Pedido(int codigo, string cliente, string productos, DateTime fecha, int cantidad)
            : this(codigo, cliente, productos, fecha)
        {
            Cantidad = cantidad;
        }
    }
}
