
namespace Jaroni_ED_Grupo8
{
    internal static class Program
    {
        // Inicia el menú desde el que se abren los módulos activos.
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            // Una sola instancia conserva los datos mientras la aplicación está abierta.
            var estado = new EstadoAplicacion();
            Application.Run(new JaroniApp.FormPrincipal(estado));
        }
    }
}
