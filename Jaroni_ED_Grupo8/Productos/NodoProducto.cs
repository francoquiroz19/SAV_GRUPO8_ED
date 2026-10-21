using Jaroni_ED_Grupo8;

namespace Jaroni_ED_Grupo8
{
    // Guarda un producto y el enlace al siguiente nodo de la lista.
    public class NodoProducto
    {
        public Producto Dato { get; set; }
        public NodoProducto Siguiente { get; set; }

        public NodoProducto(Producto dato)
        {
            Dato = dato;
            Siguiente = null;
        }
    }
}
