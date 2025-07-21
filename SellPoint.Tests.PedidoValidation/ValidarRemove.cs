using System;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Validations.PedidoValidator;
using SellPoint.Aplication.Validations.Mensajes; 

namespace SellPoint.Tests.PedidoValidation
{
    public class ValidarRemoveTests
    {
        [Fact]
        public void ValidarRemove_DeberiaFallar_CuandoDTOEsNulo()
        {
            var resultado = PedidoValidator.ValidarRemove(null!);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.EntidadNula, resultado.Message);
        }

        [Fact]
        public void ValidarRemove_DeberiaFallar_CuandoIdEsMenorOIgualACero()
        {
            var dto = new RemovePedidoDTO { Id = 0 };

            var resultado = PedidoValidator.ValidarRemove(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, resultado.Message);
        }

        [Fact]
        public void ValidarRemove_DeberiaSerExitoso_CuandoDTOEsValido()
        {
            var dto = new RemovePedidoDTO { Id = 1 };

            var resultado = PedidoValidator.ValidarRemove(dto);

            Assert.True(resultado.IsSuccess);
        }
    }
}