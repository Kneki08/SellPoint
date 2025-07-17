using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoService
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public ObtenerPorIdAsyncTests()
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

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsMenorOIgualACero()
        {
            // Act
            var result = await _pedidoService.ObtenerPorIdAsync(0);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del pedido debe ser mayor que cero.", result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoNoSeEncuentra()
        {
            // Arrange
            int id = 99;
            var expected = OperationResult.Failure("No se encontró el pedido con ID " + id);

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(id))
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No se encontró el pedido con ID " + id, result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoExistePedido()
        {
            // Arrange
            int id = 1;
            var expected = OperationResult.Success(new PedidoDTO { Id = id }, "Pedido encontrado.");

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(id))
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerPorIdAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Pedido encontrado.", result.Message);
            Assert.NotNull(result.Data);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(id), Times.Once);
        }
    }
}