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