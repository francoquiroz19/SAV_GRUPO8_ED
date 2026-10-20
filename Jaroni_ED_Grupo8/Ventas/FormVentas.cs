using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Jaroni_ED_Grupo8;

namespace JaroniApp;

// Formulario independiente: el detalle existe solo durante esta demostración.
public partial class FormVentas : Form
{
    private readonly VentasListaCircular<VentasDetalle> detalleVenta = new();
    private VentasNodo<VentasDetalle>? articuloActual;
    private static readonly CultureInfo CulturaSoles = CultureInfo.GetCultureInfo("es-PE");

    public FormVentas()
    {
        Text = "Ventas de prueba";
        Size = new Size(640, 420);
        MinimumSize = new Size(640, 420);
        StartPosition = FormStartPosition.CenterParent;
        InicializarSeccionVentas();
    }
}
