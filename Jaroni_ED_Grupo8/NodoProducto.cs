using Jaroni_ED_Grupo8;

namespace Jaroni_ED_Grupo8
{
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