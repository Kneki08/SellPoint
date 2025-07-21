using System;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Validations.Pedidos;
using SellPoint.Aplication.Validations.Mensajes; // <-- Importante

namespace SellPoint.Tests.PedidoValidation
{
    public class ValidarSaveTests
    {
        private SavePedidoDTO CrearDtoValido()
        {
            return new SavePedidoDTO
            {
                IdUsuario = 1,
                FechaPedido = DateTime.UtcNow,
                Estado = "EnPreparacion",
                IdDireccionEnvio = 1,
                CuponId = null,
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC123",
                Subtotal = 100,
                Descuento = 0,
                CostoEnvio = 0,
                Total = 100, 
                Notas = "Entrega rápida"
            };
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoIdUsuarioEsCero()
        {
            var dto = CrearDtoValido();
            dto.IdUsuario = 0;

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.UsuarioIdInvalido, resultado.Message);
        }

        [Fact]
        public void ValidarSave_DeberiaSerExitoso_CuandoDTOEsValido()
        {
            var dto = CrearDtoValido();

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.True(resultado.IsSuccess);
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoMetodoPagoEsMuyLargo()
        {
            var dto = CrearDtoValido();
            dto.MetodoPago = new string('A', 51);

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoMuyLargo, resultado.Message);
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = CrearDtoValido();
            dto.ReferenciaPago = new string('B', 101);

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, resultado.Message);
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoNotasEsMuyLarga()
        {
            var dto = CrearDtoValido();
            dto.Notas = new string('C', 501);

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, resultado.Message);
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoSubtotalEsNegativo()
        {
            var dto = CrearDtoValido();
            dto.Subtotal = -1;

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.SubtotalNegativo, resultado.Message);
        }

        [Fact]
        public void ValidarSave_DeberiaFallar_CuandoTotalEsInconsistente()
        {
            var dto = CrearDtoValido();

            dto.Total = 50;

            var resultado = PedidoValidator.ValidarSave(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.TotalInconsistente, resultado.Message);
        }
    }
}