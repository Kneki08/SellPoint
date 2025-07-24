namespace SellPoint.Aplication.Validations.Mensajes
{
    public static class MensajesValidacion
    {
        public const string EntidadNula = "La entidad no puede ser nula.";
        public const string PedidoIdInvalido = "El ID del pedido debe ser mayor que cero.";
        public const string UsuarioIdInvalido = "El ID del usuario debe ser mayor que cero.";
        public const string SubtotalNegativo = "El subtotal no puede ser negativo.";
        public const string DescuentoNegativo = "El descuento no puede ser negativo.";
        public const string CostoEnvioNegativo = "El costo de envío no puede ser negativo.";
        public const string TotalInconsistente = "El total del pedido no coincide con la suma de subtotal, descuento y costo de envío.";
        public const string MetodoPagoRequerido = "El método de pago es obligatorio.";
        public const string MetodoPagoMuyLargo = "El método de pago no debe superar los 50 caracteres.";
        public const string ReferenciaPagoMuyLarga = "La referencia de pago no debe superar los 100 caracteres.";
        public const string NotasMuyLargas = "Las notas no deben superar los 500 caracteres.";
        public const string FechaActualizacionInvalida = "La fecha de actualización no es válida.";
        public const string EstadoRequerido = "El estado del pedido es obligatorio.";
        public const string EstadoNoValido = "El estado del pedido no es válido.";
        public const string PedidosObtenidosCorrectamente = "Pedidos obtenidos correctamente.";
        public const string ErrorObtenerPedidos = "No se pudieron obtener los pedidos.";
        public const string PedidoNoEncontrado = "No se encontró el pedido con ID {0}";
        public const string PedidoEncontrado = "Pedido encontrado.";
        public const string OperacionExitosa = "Operación exitosa.";
        public const string IdPedidoInvalido = "El ID del pedido debe ser mayor que cero.";
        public const string EstadoPedidoRequerido = "El estado del pedido es obligatorio.";
        public const string EstadoPedidoInvalido = "El estado del pedido no es válido.";
        public const string ErrorAgregarPedido = "Error inesperado al agregar el pedido.";
        public const string ErrorActualizarPedido = "Error inesperado al actualizar el pedido.";
        public const string ErrorEliminarPedido = "Ocurrió un error inesperado al eliminar el pedido.";
        public const string PedidoAgregado = "Pedido agregado correctamente.";
        public const string PedidoNoAgregado = "No se pudo agregar el pedido.";
        public const string PedidoActualizado = "Pedido actualizado correctamente.";
        public const string PedidoNoActualizado = "No se pudo actualizar el pedido.";
        public const string PedidoEliminado = "Pedido eliminado correctamente.";
        public const string PedidoNoEliminado = "No se pudo eliminar el pedido.";
        public const string PedidoObtenido = "Pedido obtenido correctamente.";
        public const string PedidoNoEncontradoSimple = "No se encontró el pedido.";
        public const string ErrorObtenerPedidoPorId = "Error al obtener el pedido por Id";
        public const string ErrorObtenerTodos = "Error al obtener todos los pedidos";
        public const string FechaPedidoInvalida = "La fecha del pedido no es válida.";
        public const string MetodoPagoNoValido = "El método de pago no es válido.";
        public const string SinPedidosEncontrados = "No se encontraron pedidos en la base de datos.";
    }
}