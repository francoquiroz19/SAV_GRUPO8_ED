using System;

namespace Jaroni_ED_Grupo8;

// Almacena el detalle de venta en nodos cuyo último enlace regresa al primero.
public sealed class VentasListaCircular<T>
{
    // cola es null cuando no hay artículos. En otro caso apunta al último nodo.
    private VentasNodo<T>? cola;

    // El último enlaza con el primero; por eso cola.Siguiente es la cabeza.
    public VentasNodo<T>? ObtenerCabeza() => cola?.Siguiente;

    // Inserta detrás de la cola y mantiene cerrado el círculo.
    public VentasNodo<T> InsertarAlFinal(T dato)
    {
        var nuevo = new VentasNodo<T>(dato);
        if (cola is null)
        {
            nuevo.Siguiente = nuevo;
        }
        else
        {
            // El nuevo apunta a la cabeza anterior antes de enlazarlo desde la cola.
            nuevo.Siguiente = cola.Siguiente;
            cola.Siguiente = nuevo;
        }

        cola = nuevo;
        return nuevo;
    }

    // Busca durante una vuelta completa sin usar la tabla como almacenamiento.
    public VentasNodo<T>? Buscar(Predicate<T> coincide)
    {
        if (cola is null) return null;

        var inicio = cola.Siguiente;
        var actual = inicio;
        // Una vuelta completa termina al regresar al inicio; null nunca marca el final.
        do
        {
            if (coincide(actual.Dato)) return actual;
            actual = actual.Siguiente;
        } while (actual != inicio);

        return null;
    }

    // Desenlaza el nodo encontrado, sea único, cabeza, intermedio o cola.
    public bool Eliminar(Predicate<T> coincide)
    {
        if (cola is null) return false;

        var anterior = cola;
        var actual = cola.Siguiente;
        var inicio = actual;
        do
        {
            if (coincide(actual.Dato))
            {
                if (actual == anterior)
                {
                    // Era el único nodo.
                    cola = null;
                }
                else
                {
                    // Sirve para cabeza e intermedio; al quitar la cola se mueve atrás.
                    anterior.Siguiente = actual.Siguiente;
                    if (actual == cola) cola = anterior;
                }

                actual.Siguiente = actual;
                return true;
            }

            anterior = actual;
            actual = actual.Siguiente;
        } while (actual != inicio);

        return false;
    }

    public void Recorrer(Action<T> visitar)
    {
        if (cola is null) return;

        var inicio = cola.Siguiente;
        var actual = inicio;
        // do-while visita también la lista de un solo nodo y evita un ciclo infinito.
        do
        {
            visitar(actual.Dato);
            actual = actual.Siguiente;
        } while (actual != inicio);
    }

    // Cuenta los nodos mediante el mismo recorrido circular.
    public int ObtenerLongitud()
    {
        var longitud = 0;
        Recorrer(_ => longitud++);
        return longitud;
    }

    // Al soltar la cola, la lista vuelve a estar vacía.
    public void Vaciar()
    {
        cola = null;
    }
}
