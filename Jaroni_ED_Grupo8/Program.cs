
namespace Jaroni_ED_Grupo8
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new JaroniApp.FormPrincipal());
        }
    }
}
