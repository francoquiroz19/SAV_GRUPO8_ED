using System.Collections.Generic;

namespace Jaroni_ED_Grupo8
{
    // Administra productos mediante una lista simplemente enlazada.
    public class ListaProductos
    {
        // La cabeza señala el primer producto; null indica lista vacía.
        private NodoProducto cabeza;

        public ListaProductos()
        {
            cabeza = null;
        }

        // Inserta al final, después de comprobar que el código no exista.
        public bool Registrar(Producto producto)
        {
            if (Buscar(producto.Codigo) != null) return false;
            NodoProducto nuevo = new NodoProducto(producto);
            if (cabeza == null) cabeza = nuevo;
            else
            {
                NodoProducto actual = cabeza;
                while (actual.Siguiente != null) actual = actual.Siguiente;
                actual.Siguiente = nuevo;
            }
            return true;
        }

        // Recorre los enlaces hasta hallar el código solicitado.
        public Producto Buscar(string codigo)
        {
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo) return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        // Modifica los datos del producto encontrado sin reemplazar su nodo.
        public bool Actualizar(string codigo, string nuevoNombre, string nuevaCategoria, decimal nuevoPrecio, string nuevaUbicacion)
        {
            Producto p = Buscar(codigo);
            if (p != null)
            {
                p.Nombre = nuevoNombre;
                p.Categoria = nuevaCategoria;
                p.Precio = nuevoPrecio;
                p.Ubicacion = nuevaUbicacion;
                return true;
            }
            return false;
        }

        // Desenlaza la cabeza o el nodo encontrado, conservando el resto de la lista.
        public bool Eliminar(string codigo)
        {
            if (cabeza == null) return false;
            if (cabeza.Dato.Codigo == codigo)
            {
                cabeza = cabeza.Siguiente;
                return true;
            }
            NodoProducto actual = cabeza;
            while (actual.Siguiente != null && actual.Siguiente.Dato.Codigo != codigo)
            {
                actual = actual.Siguiente;
            }
            if (actual.Siguiente != null)
            {
                actual.Siguiente = actual.Siguiente.Siguiente;
                return true;
            }
            return false;
        }

        // Incrementa la cantidad de un producto existente.
        public bool RegistrarEntrada(string codigo, int cantidad)
        {
            Producto p = Buscar(codigo);
            if (p != null && cantidad > 0)
            {
                p.Cantidad += cantidad;
                return true;
            }
            return false;
        }

        // Descuenta unidades solo cuando la cantidad disponible alcanza.
        public bool RegistrarSalida(string codigo, int cantidad)
        {
            Producto p = Buscar(codigo);
            if (p != null && cantidad > 0)
            {
                if (p.Cantidad - cantidad < 0) return false;
                p.Cantidad -= cantidad;
                return true;
            }
            return false;
        }

        // Recorre los nodos y prepara una vista para el DataGridView.
        public List<Producto> ObtenerListaParaGrilla()
        {
            List<Producto> lista = new List<Producto>();
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                lista.Add(actual.Dato);
                actual = actual.Siguiente;
            }
            return lista;
        }

        // Recorre la lista y devuelve productos bajo el umbral indicado.
        public List<Producto> ObtenerBajoStock(int umbralMinimo)
        {
            List<Producto> listaBajoStock = new List<Producto>();
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Cantidad <= umbralMinimo)
                {
                    listaBajoStock.Add(actual.Dato);
                }
                actual = actual.Siguiente;
            }
            return listaBajoStock;
        }
    }
}
