using System;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Validations.PedidoValidator;
using SellPoint.Aplication.Validations.Mensajes; 

namespace SellPoint.Tests.PedidoValidation
{
    public class ValidarUpdateTests
    {
        private UpdatePedidoDTO CrearDtoValido()
        {
            return new UpdatePedidoDTO
            {
                Id = 1,
                IdUsuario = 1,
                Estado = "Enviado",
                FechaPedido = DateTime.UtcNow,
                FechaActualizacion = DateTime.UtcNow,
                IdDireccionEnvio = 1,
                CuponId = null,
                MetodoPago = "Tarjeta",
                ReferenciaPago = "XYZ123",
                Subtotal = 100,
                Descuento = 10,
                CostoEnvio = 5,
                Total = 95, 
                Notas = "Todo correcto"
            };
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoIdEsMenorOIgualACero()
        {
            var dto = CrearDtoValido();
            dto.Id = 0;

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoEstadoEsVacio()
        {
            var dto = CrearDtoValido();
            dto.Estado = "";

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.EstadoRequerido, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoEstadoNoEsValido()
        {
            var dto = CrearDtoValido();
            dto.Estado = "Desconocido";

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.EstadoNoValido, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoFechaEsMuyFutura()
        {
            var dto = CrearDtoValido();
            dto.FechaActualizacion = DateTime.UtcNow.AddHours(1); 

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.FechaActualizacionInvalida, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoMetodoPagoEsMuyLargo()
        {
            var dto = CrearDtoValido();
            dto.MetodoPago = new string('A', 51);

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoMuyLargo, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = CrearDtoValido();
            dto.ReferenciaPago = new string('B', 101);

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaFallar_CuandoNotasSonMuyLargas()
        {
            var dto = CrearDtoValido();
            dto.Notas = new string('C', 501);

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, resultado.Message);
        }

        [Fact]
        public void ValidarUpdate_DeberiaSerExitoso_CuandoDTOEsValido()
        {
            var dto = CrearDtoValido();

            var resultado = PedidoValidator.ValidarUpdate(dto);

            Assert.True(resultado.IsSuccess);
        }
    }
}