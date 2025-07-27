using System;
using System.Windows.Forms;
using SellPoint.View.Forms;
using SellPoint.View.Services.Pedido;

namespace SellPoint.View
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize(); 
            Application.Run(new PedidoForm(new PedidoService()));
        }
    }
}   