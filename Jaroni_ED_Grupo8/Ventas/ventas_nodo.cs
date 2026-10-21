namespace Jaroni_ED_Grupo8;

// Guarda un artículo y su enlace al siguiente nodo de la lista circular.
public sealed class VentasNodo<T>
{
    public T Dato { get; }
    public VentasNodo<T> Siguiente { get; internal set; }

    public VentasNodo(T dato)
    {
        Dato = dato;
        Siguiente = this;
    }
}
