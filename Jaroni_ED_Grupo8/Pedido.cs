
using System;

namespace JaroniApp
{
    // CONTIENE LOS DATOS DE UN PEDIDO
    public class Pedido
    {
        public int Codigo { get; set; }
        public string Cliente { get; set; }
        public string Productos { get; set; }
        public DateTime Fecha { get; set; }
        public string Estado { get; set; }

        // INICIALIZA LOS DATOS DEL PEDIDO
        public Pedido(int codigo, string cliente, string productos, DateTime fecha)
        {
            Codigo = codigo;
            Cliente = cliente;
            Productos = productos;
            Fecha = fecha;

            // TODO PEDIDO NUEVO INICIA COMO PENDIENTE
            Estado = "Pendiente";
        }
    }
}
