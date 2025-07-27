namespace SellPoint.View.Validations
{
    public static class PedidoIdValidator
    {
        public static (bool IsSuccess, string Message) Validar(int id)
        {
            if (id <= 0)
                return (false, MensajesValidacion.IdInvalido);

            return (true, "");
        }
    }
}