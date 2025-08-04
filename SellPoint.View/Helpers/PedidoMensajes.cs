namespace SellPoint.View.Helpers
{
    public static class PedidoMensajes
    {
        // UI
        public const string Exito = "Éxito";
        public const string Error = "Error";
        public const string Validacion = "Validación";
        public const string Advertencia = "Advertencia";
        public const string ConfirmarEliminacion = "¿Seguro que deseas eliminar este pedido?";
        public const string PedidoCargadoCorrectamente = "Pedido cargado correctamente.";

        // Logs
        public const string LogIntentoAgregar = "Intentando agregar nuevo pedido.";
        public const string LogValidacionFallidaAgregar = "Validación fallida al agregar pedido: {Mensaje}";
        public const string LogAgregadoExito = "Pedido agregado exitosamente.";
        public const string LogErrorAgregar = "Error al agregar pedido: {Mensaje}";

        public const string LogIntentoActualizar = "Intentando actualizar pedido ID: {Id}";
        public const string LogValidacionFallidaActualizar = "Validación fallida al actualizar pedido: {Mensaje}";
        public const string LogActualizadoExito = "Pedido actualizado exitosamente.";
        public const string LogErrorActualizar = "Error al actualizar pedido ID {Id}: {Mensaje}";

        public const string LogIntentoEliminar = "Intentando eliminar pedido con ID: {Id}";
        public const string LogErrorEliminar = "Error al eliminar pedido ID {Id}: {Mensaje}";

        public const string LogIntentoCargar = "Intentando cargar pedido con ID: {Id}";
        public const string LogNoSePudoCargar = "No se pudo cargar pedido con ID {Id}: {Mensaje}";
    }
}