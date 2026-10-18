
namespace JaroniApp
{
    // almacena la información de una acción realizada
    public class Accion
    {
        // Guarda el nombre del módulo donde se realizó la acción
        public string Modulo { get; set; }

        // Guarda la descripción de la acción realizada
        public string Descripcion { get; set; }

        // Guarda la fecha y hora de la acción
        public DateTime FechaHora { get; set; }

        // Constructor que recibe el módulo y la descripción
        public Accion(string modulo, string descripcion)
        {
            // Asigna los datos de la acción
            Modulo = modulo;
            Descripcion = descripcion;

            // Registra la fecha y hora actual
            FechaHora = DateTime.Now;
        }

        // Devuelve la información de la acción en forma de texto
        public override string ToString()
        {
            return $"[{FechaHora:dd/MM/yyyy HH:mm:ss}] {Modulo}: {Descripcion}";
        }
    }
}
