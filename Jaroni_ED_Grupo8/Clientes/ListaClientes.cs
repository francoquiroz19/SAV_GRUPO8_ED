namespace Jaroni_ED_Grupo8
{
    // Clase que representa cada nodo de la lista de clientes
    public class NodoCliente
    {
        // Guarda el cliente almacenado en el nodo
        public Cliente Dato { get; set; }

        // Referencia al siguiente nodo (null si es el último)
        public NodoCliente? Siguiente { get; set; }

        // Constructor que recibe un cliente y deja el enlace vacío
        public NodoCliente(Cliente cliente)
        {
            Dato = cliente;
            Siguiente = null;
        }
    }

    // Clase que administra los clientes mediante una lista simplemente enlazada
    public class ListaClientes
    {
        // Primer nodo de la lista (cabeza)
        private NodoCliente? cabeza;

        // Cantidad de clientes registrados
        private int count;

        // Devuelve la cantidad de clientes almacenados
        public int Count => count;

        // Verifica si la lista está vacía
        public bool EstaVacia => count == 0;

        // Constructor que inicializa la lista vacía
        public ListaClientes()
        {
            cabeza = null;
            count = 0;
        }

        // MÉTODOS PRINCIPALES

        // Verifica si ya existe un cliente con el código indicado
        public bool ExisteCodigo(int codigo)
        {
            return Buscar(codigo) != null;
        }

        // Registra un cliente al final de la lista.
        // Devuelve false si el código ya existe (no se permiten repetidos).
        public bool Agregar(Cliente cliente)
        {
            // Valida que no se repita el código de cliente
            if (ExisteCodigo(cliente.Codigo))
            {
                return false;
            }

            NodoCliente nuevo = new NodoCliente(cliente);

            if (cabeza == null)
            {
                // Si la lista está vacía, el nuevo nodo es la cabeza
                cabeza = nuevo;
            }
            else
            {
                // Avanza hasta el último nodo y enlaza el nuevo
                NodoCliente actual = cabeza;
                while (actual.Siguiente != null)
                    actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }

            count++;
            return true;
        }

        // Busca un cliente por su código.
        // Retorna null si no lo encuentra.
        public Cliente? Buscar(int codigo)
        {
            NodoCliente? actual = cabeza;

            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                    return actual.Dato;

                actual = actual.Siguiente;
            }

            return null;
        }

        // Busca el primer cliente cuyo nombre contenga el texto indicado
        // (sin distinguir mayúsculas de minúsculas).
        public Cliente? BuscarPorNombre(string texto)
        {
            NodoCliente? actual = cabeza;

            while (actual != null)
            {
                if (actual.Dato.Nombre.IndexOf(texto, StringComparison.OrdinalIgnoreCase) >= 0)
                    return actual.Dato;

                actual = actual.Siguiente;
            }

            return null;
        }

        // Actualiza los datos de un cliente existente (el código no cambia).
        // Devuelve false si no existe el cliente.
        public bool Actualizar(int codigo, string nombre, string documento, string telefono, string correo, string direccion)
        {
            Cliente? cliente = Buscar(codigo);

            if (cliente == null)
                return false;

            cliente.Nombre = nombre;
            cliente.Documento = documento;
            cliente.Telefono = telefono;
            cliente.Correo = correo;
            cliente.Direccion = direccion;
            return true;
        }

        // Elimina un cliente por su código.
        // Devuelve false si no existe.
        public bool Eliminar(int codigo)
        {
            if (cabeza == null)
                return false;

            // Si es la cabeza, la siguiente pasa a ser la nueva cabeza
            if (cabeza.Dato.Codigo == codigo)
            {
                cabeza = cabeza.Siguiente;
                count--;
                return true;
            }

            // Recorre la lista buscando el nodo anterior al que se quiere eliminar
            NodoCliente actual = cabeza;
            while (actual.Siguiente != null)
            {
                if (actual.Siguiente.Dato.Codigo == codigo)
                {
                    // Desenlaza el nodo saltándolo
                    actual.Siguiente = actual.Siguiente.Siguiente;
                    count--;
                    return true;
                }

                actual = actual.Siguiente;
            }

            return false;
        }

        // Devuelve todos los clientes como una List<Cliente> para mostrarlos fácilmente
        public List<Cliente> ObtenerTodos()
        {
            List<Cliente> clientes = new List<Cliente>();
            NodoCliente? actual = cabeza;

            while (actual != null)
            {
                clientes.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            return clientes;
        }

        // Elimina todos los clientes de la lista
        public void Limpiar()
        {
            cabeza = null;
            count = 0;
        }
    }
}