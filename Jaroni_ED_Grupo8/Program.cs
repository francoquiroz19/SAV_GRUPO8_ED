
namespace Jaroni_ED_Grupo8
{
    // Clase principal que permite iniciar el programa
    internal static class Program
    {
        // Indica cómo se ejecutará el programa
        [STAThread]

        // Método principal donde comienza la ejecución
        static void Main()
        {
            // Inicializa la configuración de Windows Forms
            ApplicationConfiguration.Initialize();

            // Abre el formulario del historial de acciones
            Application.Run(new JaroniApp.FormHistorial());
        }
    }
}
