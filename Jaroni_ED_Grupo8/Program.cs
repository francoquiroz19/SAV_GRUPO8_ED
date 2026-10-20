
namespace Jaroni_ED_Grupo8
{
    // Clase principal que permite iniciar el programa
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormHistorial());
        }
    }
}
