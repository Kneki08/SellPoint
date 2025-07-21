using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using SellPoint.Aplication.Validations.Mensajes; 

namespace SellPoint.Tests.PedidoService
{
    public class AgregarAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public AgregarAsyncTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _loggerMock = new Mock<ILogger<PedidoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _pedidoService = new PedidoServiceClass(
                _pedidoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

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
        public async Task AgregarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = CrearDtoValido();
            var expectedResult = OperationResult.Success(MensajesValidacion.OperacionExitosa);

            _pedidoRepositoryMock
                .Setup(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()))
                .ReturnsAsync(expectedResult);

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(MensajesValidacion.OperacionExitosa, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Once);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _pedidoService.AgregarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.EntidadNula, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsMenorOIgualACero()
        {
            var dto = CrearDtoValido();
            dto.IdUsuario = 0;

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.UsuarioIdInvalido, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoSubtotalEsNegativo()
        {
            var dto = CrearDtoValido();
            dto.Subtotal = -1;

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.SubtotalNegativo, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoDescuentoEsNegativo()
        {
            var dto = CrearDtoValido();
            dto.Descuento = -5;

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.DescuentoNegativo, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoCostoEnvioEsNegativo()
        {
            var dto = CrearDtoValido();
            dto.CostoEnvio = -10;

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.CostoEnvioNegativo, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoTotalNoCoincide()
        {
            var dto = CrearDtoValido();
            dto.Total = 999; // Total incorrecto a propósito

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.TotalInconsistente, result.Message);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsNulo()
        {
            var dto = CrearDtoValido();
            dto.MetodoPago = null!;

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoRequerido, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoMetodoPagoEsMuyLargo()
        {
            var dto = CrearDtoValido();
            dto.MetodoPago = new string('A', 51);

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoMuyLargo, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = CrearDtoValido();
            dto.ReferenciaPago = new string('B', 101);

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }

        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoNotasSonMuyLargas()
        {
            var dto = CrearDtoValido();
            dto.Notas = new string('C', 501);

            var result = await _pedidoService.AgregarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<Domainn.Entities.Orders.Pedido>()), Times.Never);
        }


    }
}