namespace JaroniApp
{
    // Clase modelo que almacena la información de un cliente
    public class Cliente
    {
        // Código único del cliente (no se puede repetir en la lista)
        public int Codigo { get; set; }

        // Nombre o razón social del cliente
        public string Nombre { get; set; }

        // Documento de identidad (DNI o RUC)
        public string Documento { get; set; }

        // Teléfono de contacto
        public string Telefono { get; set; }

        // Correo electrónico de contacto
        public string Correo { get; set; }

        // Dirección del cliente
        public string Direccion { get; set; }

        // Constructor para crear un cliente con todos sus datos
        public Cliente(int codigo, string nombre, string documento, string telefono, string correo, string direccion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Documento = documento;
            Telefono = telefono;
            Correo = correo;
            Direccion = direccion;
        }

        // Sobrescribe ToString para mostrar el cliente en el ListBox
        public override string ToString()
        {
            return $"{Codigo} - {Nombre} - Doc: {Documento} - Tel: {Telefono}";
        }
    }
}