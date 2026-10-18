
using System;

namespace JaroniApp
{
    // REPRESENTA CADA NODO DE LA LISTA DE PEDIDOS
    public class NodoPedido
    {
        public Pedido Dato { get; set; }
        public NodoPedido Siguiente { get; set; }
        public NodoPedido Anterior { get; set; }

        // GUARDA EL PEDIDO Y SUS ENLACES
        public NodoPedido(Pedido pedido)
        {
            Dato = pedido;
            Siguiente = null;
            Anterior = null;
        }
    }

    // ADMINISTRA LOS PEDIDOS MEDIANTE UNA LISTA DOBLEMENTE ENLAZADA
    public class ListaPedidos
    {
        private NodoPedido cabeza;
        private NodoPedido cola;

        // INICIALIZA LA LISTA VACIA
        public ListaPedidos()
        {
            cabeza = null;
            cola = null;
        }

        // AGREGA UN PEDIDO AL FINAL DE LA LISTA
        public void Agregar(Pedido pedido)
        {
            NodoPedido nuevo = new NodoPedido(pedido);

            if (cabeza == null)
            {
                // SI LA LISTA ESTA VACIA, EL NUEVO NODO ES EL PRIMERO Y EL ULTIMO
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                // ENLAZA EL ULTIMO NODO CON EL NUEVO
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
        }

        // BUSCA UN PEDIDO POR SU CODIGO
        public Pedido Buscar(int codigo)
        {
            NodoPedido actual = cabeza;

            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                {
                    return actual.Dato;
                }

                actual = actual.Siguiente;
            }

            // RETORNA NULL SI NO ENCUENTRA EL PEDIDO
            return null;
        }

        // ACTUALIZA EL ESTADO DE UN PEDIDO
        public bool ActualizarEstado(int codigo, string nuevoEstado)
        {
            Pedido pedido = Buscar(codigo);

            if (pedido == null)
            {
                return false;
            }

            pedido.Estado = nuevoEstado;
            return true;
        }

        // MUESTRA LOS PEDIDOS DESDE EL PRIMERO HASTA EL ULTIMO
        public void MostrarAdelante()
        {
            NodoPedido actual = cabeza;

            while (actual != null)
            {
                Console.WriteLine(
                    actual.Dato.Codigo + " - " +
                    actual.Dato.Cliente + " - " +
                    actual.Dato.Estado);

                actual = actual.Siguiente;
            }
        }

        // MUESTRA LOS PEDIDOS DESDE EL ULTIMO HASTA EL PRIMERO
        public void MostrarAtras()
        {
            NodoPedido actual = cola;

            while (actual != null)
            {
                Console.WriteLine(
                    actual.Dato.Codigo + " - " +
                    actual.Dato.Cliente + " - " +
                    actual.Dato.Estado);

                actual = actual.Anterior;
            }
        }
    }
}
