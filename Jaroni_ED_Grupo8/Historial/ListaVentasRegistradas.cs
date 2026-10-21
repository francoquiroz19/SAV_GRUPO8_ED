namespace Jaroni_ED_Grupo8;

// Conserva las ventas confirmadas aunque salgan de la pila de revisión.
public sealed class ListaVentasRegistradas
{
    private sealed class NodoVenta
    {
        public VentaRegistrada Dato { get; }
        public NodoVenta? Siguiente { get; set; }

        public NodoVenta(VentaRegistrada dato) => Dato = dato;
    }

    private NodoVenta? cabeza;
    private NodoVenta? cola;
    public int Longitud { get; private set; }

    // Inserta al final de la lista simplemente enlazada.
    public void Agregar(VentaRegistrada venta)
    {
        var nuevo = new NodoVenta(venta);
        if (cola is null)
            cabeza = nuevo;
        else
            cola.Siguiente = nuevo;
        cola = nuevo;
        Longitud++;
    }

    // Recorre desde la cabeza hasta el último registro, sin retirarlo.
    public void Recorrer(Action<VentaRegistrada> visitar)
    {
        var actual = cabeza;
        while (actual is not null)
        {
            visitar(actual.Dato);
            actual = actual.Siguiente;
        }
    }

    public VentaRegistrada? Buscar(string codigo)
    {
        var actual = cabeza;
        while (actual is not null)
        {
            if (actual.Dato.Codigo == codigo) return actual.Dato;
            actual = actual.Siguiente;
        }
        return null;
    }
}
