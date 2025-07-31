using System.Windows.Forms;

namespace SellPoint.View.Helpers
{
    public static class MessageBoxHelper
    {
        public static void MostrarError(string mensaje, string titulo = "Error")
            => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
            => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static void MostrarExito(string mensaje, string titulo = "Éxito")
            => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void MostrarInfo(string mensaje, string titulo = "Información")
            => MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static bool MostrarPregunta(string mensaje, string titulo = "Confirmar")
            => MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}