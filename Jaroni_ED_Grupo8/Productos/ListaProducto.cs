using Jaroni_ED_Grupo8;
using System.Collections.Generic;

namespace Jaroni_ED_Grupo8
{
    public class ListaProductos
    {
        private NodoProducto cabeza;

        public ListaProductos()
        {
            cabeza = null;
        }

        // 1. Registrar producto
        public bool Registrar(Producto producto)
        {
            if (Buscar(producto.Codigo) != null) return false; // Evita códigos duplicados

            NodoProducto nuevo = new NodoProducto(producto);
            if (cabeza == null)
            {
                cabeza = nuevo;
            }
            else
            {
                NodoProducto actual = cabeza;
                while (actual.Siguiente != null)
                {
                    actual = actual.Siguiente;
                }
                actual.Siguiente = nuevo;
            }
            return true;
        }

        // 2. Buscar producto
        public Producto Buscar(string codigo)
        {
            NodoProducto actual = cabeza;
            while (actual != null)
            {
                if (actual.Dato.Codigo == codigo)
                    return actual.Dato;
                actual = actual.Siguiente;
            }
            return null;
        }

        // 3. Actualizar producto
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

        // 4. Eliminar producto
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

        // 5. Registrar Entradas de stock
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

        // 6. Registrar Salidas de stock (Validando que no quede negativo)
        public bool RegistrarSalida(string codigo, int cantidad)
        {
            Producto p = Buscar(codigo);
            if (p != null && cantidad > 0)
            {
                if (p.Cantidad - cantidad < 0) return false; // Regla de negocio: stock no negativo

                p.Cantidad -= cantidad;
                return true;
            }
            return false;
        }

        // Método auxiliar para pasar los datos al DataGridView en Windows Forms
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

        // 7. Mostrar productos con bajo stock
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