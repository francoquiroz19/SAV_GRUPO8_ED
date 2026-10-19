namespace Jaroni_ED_Grupo8;

public sealed class VentasNodo<T>
{
    // Cada nodo guarda un artículo y el enlace al siguiente artículo de la venta.
    public T Dato { get; }
    public VentasNodo<T> Siguiente { get; internal set; }

    public VentasNodo(T dato)
    {
        Dato = dato;
        Siguiente = this;
    }
}
