
namespace JaroniApp
{
    // Clase que representa un nodo de la pila
    public class NodoAccion
    {
        // Guarda la información de una acción
        public Accion Dato { get; set; }

        // Guarda la referencia al siguiente nodo
        public NodoAccion? Siguiente { get; set; }

        // Constructor que recibe una acción
        public NodoAccion(Accion accion)
        {
            Dato = accion;
            Siguiente = null;
        }
    }

    // Clase que administra el historial de acciones mediante una pila
    public class PilaHistorial
    {
        // Guarda el nodo que se encuentra en la cima de la pila
        private NodoAccion? cima;

        // Guarda la cantidad de acciones registradas
        private int cantidad;

        // Devuelve la cantidad de acciones almacenadas
        public int Cantidad => cantidad;

        // Verifica si la pila está vacía
        public bool EstaVacia => cima == null;

        // Constructor que inicializa la pila vacía
        public PilaHistorial()
        {
            cima = null;
            cantidad = 0;
        }

        // PUSH: Agrega una nueva acción a la pila
        public void Push(Accion accion)
        {
            // Creamos un nuevo nodo con la acción
            NodoAccion nuevo = new NodoAccion(accion);

            // Conectamos el nuevo nodo con la cima anterior
            nuevo.Siguiente = cima;

            // El nuevo nodo pasa a ser la cima
            cima = nuevo;

            // Aumentamos la cantidad de acciones
            cantidad++;
        }

        // Registra una acción indicando el módulo y la descripción
        public void Registrar(string modulo, string descripcion)
        {
            // Crea una acción y la agregamos a la pila
            Push(new Accion(modulo, descripcion));
        }

        // PEEK: Consulta la última acción sin eliminarla
        public Accion Peek()
        {
            // Verifica si la pila está vacía
            if (cima == null)
            {
                throw new InvalidOperationException("El historial está vacío.");
            }

            // devuelve la acción que está en la cima
            return cima.Dato;
        }

        // POP: Retira la última acción registrada
        public Accion Pop()
        {
            // Verifica si la pila está vacía
            if (cima == null)
            {
                throw new InvalidOperationException("El historial está vacío.");
            }

            // Guarda la acción que vamos a retirar
            Accion accion = cima.Dato;

            // El siguiente nodo pasa a ser la nueva cima
            cima = cima.Siguiente;

            // Disminuye la cantidad de acciones
            cantidad--;

            // Devuelve la acción retirada
            return accion;
        }

        // Obtiene todas las acciones desde la más reciente
        public List<Accion> ObtenerTodas()
        {
            // Crea una lista para guardar las acciones
            List<Accion> acciones = new List<Accion>();

            // Comienza el recorrido desde la cima
            NodoAccion? actual = cima;

            // Recorre los nodos hasta llegar al final
            while (actual != null)
            {
                // Agrega la acción a la lista
                acciones.Add(actual.Dato);

                // Avanza al siguiente nodo
                actual = actual.Siguiente;
            }

            // Devuelve todas las acciones encontradas
            return acciones;
        }

        // Elimina todas las acciones del historial
        public void Limpiar()
        {
            // Deja la pila sin nodos
            cima = null;

            // Reinicia la cantidad de acciones
            cantidad = 0;
        }
    }
}
