using JaroniApp;

namespace Jaroni_ED_Grupo8
{
    // Un nodo guarda una venta y el enlace al registro que quedó debajo.
    public sealed class NodoPilaVenta
    {
        public VentaHistorialDemo Dato { get; }
        public NodoPilaVenta? Siguiente { get; set; }

        public NodoPilaVenta(VentaHistorialDemo dato)
        {
            Dato = dato;
        }
    }

    public sealed class PilaHistorial
    {
        // El tope es el último registro agregado. LIFO: último en entrar, primero en salir.
        private NodoPilaVenta? tope;
        private int cantidad;

        public bool EstaVacia => tope is null;
        public int Longitud => cantidad;

        public void Push(VentaHistorialDemo venta)
        {
            // Primero enlazamos el nuevo nodo al antiguo tope; luego pasa a ser la cima.
            var nuevo = new NodoPilaVenta(venta);
            nuevo.Siguiente = tope;
            tope = nuevo;
            cantidad++;
        }

        public VentaHistorialDemo Pop()
        {
            if (tope is null)
                throw new InvalidOperationException("La pila está vacía.");

            // El segundo nodo pasa a ser el nuevo tope; no se elimina ningún intermedio.
            var retirada = tope.Dato;
            tope = tope.Siguiente;
            cantidad--;
            return retirada;
        }

        public VentaHistorialDemo Top()
        {
            if (tope is null)
                throw new InvalidOperationException("La pila está vacía.");

            // Consultar no cambia enlaces ni contador.
            return tope.Dato;
        }

        public void Clear()
        {
            tope = null;
            cantidad = 0;
        }

        public void Recorrer(Action<VentaHistorialDemo> visitar)
        {
            // La pila se recorre del tope a la base siguiendo Siguiente, sin desapilar.
            var actual = tope;
            while (actual is not null)
            {
                visitar(actual.Dato);
                actual = actual.Siguiente;
            }
        }
    }
}
