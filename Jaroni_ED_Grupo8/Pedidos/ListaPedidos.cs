
using System;

namespace Jaroni_ED_Grupo8
{
    // Guarda un pedido y los enlaces hacia los nodos anterior y siguiente.
    public class NodoPedido
    {
        public Pedido Dato { get; set; }
        public NodoPedido Siguiente { get; set; }
        public NodoPedido Anterior { get; set; }

        // Crea el nodo sin vecinos hasta que se inserte en la lista.
        public NodoPedido(Pedido pedido)
        {
            Dato = pedido;
            Siguiente = null;
            Anterior = null;
        }
    }



    // Administra pedidos mediante una lista doblemente enlazada.
    public class ListaPedidos
    {
        private NodoPedido cabeza;
        private NodoPedido cola;

        // La lista comienza sin cabeza ni cola.
        public ListaPedidos()
        {
            cabeza = null;
            cola = null;
        }

        // Agrega el pedido al final y conserva ambos enlaces.
        public void Agregar(Pedido pedido)
        {
            NodoPedido nuevo = new NodoPedido(pedido);

            if (cabeza == null)
            {
                // En la lista vacía, el nuevo nodo es cabeza y cola.
                cabeza = nuevo;
                cola = nuevo;
            }
            else
            {
                // Enlaza la cola anterior con el nuevo nodo.
                cola.Siguiente = nuevo;
                nuevo.Anterior = cola;
                cola = nuevo;
            }
        }

        // Recorre desde la cabeza hasta encontrar el código.
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

            // Devuelve null si el pedido no existe.
            return null;
        }

        // Cambia el estado del pedido encontrado sin mover sus enlaces.
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

        // Recorre hacia adelante y escribe cada pedido en la consola.
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

        // Recorre hacia atrás utilizando el enlace Anterior.
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
        // Permite acceder al primer nodo de la lista.
        public NodoPedido ObtenerCabeza()
        {
            return cabeza;
        }

    }

}
