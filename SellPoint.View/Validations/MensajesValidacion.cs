namespace SellPoint.View.Validations
{
    public static class MensajesValidacion
    {
        public const string IdInvalido = "El ID es inválido o menor que 1.";
        public const string UsuarioInvalido = "El usuario seleccionado es inválido o no existe.";
        public const string DireccionInvalida = "La dirección de envío seleccionada es inválida o no existe.";
        public const string MetodoPagoVacio = "El método de pago es obligatorio.";
        public const string EstadoInvalido = "El estado seleccionado no es válido.";
        public const string FechaPedidoInvalida = "La fecha del pedido es inválida o futura.";
        public const string FechaActualizacionInvalida = "La fecha de actualización es inválida o anterior a la fecha del pedido.";
        public const string SubtotalInvalido = "El subtotal es obligatorio, debe ser mayor o igual a cero.";
        public const string DescuentoInvalido = "El descuento debe ser mayor o igual a cero.";
        public const string CostoEnvioInvalido = "El costo de envío debe ser mayor o igual a cero.";
        public const string TotalInvalido = "El total debe ser mayor o igual a cero.";
        public const string NotasMuyLargas = "Las notas no pueden exceder los 500 caracteres.";
        public const string CampoObligatorio = "Este campo es obligatorio.";
        public const string CuponIdInvalido = "El cupón seleccionado es inválido.";
    }
}