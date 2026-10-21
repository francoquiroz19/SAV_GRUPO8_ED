using System;

namespace Jaroni_ED_Grupo8
{
    // Almacena los datos de un producto y su cantidad disponible.
    public class Producto
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public string Ubicacion { get; set; }

        public Producto(string codigo, string nombre, string categoria, int cantidad, decimal precio, string ubicacion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Categoria = categoria;
            Cantidad = cantidad;
            Precio = precio;
            Ubicacion = ubicacion;
        }

        public override string ToString() => $"{Codigo} — {Nombre}";
    }
}
