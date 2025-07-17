using Moq;
using Xunit;
using System.Collections.Generic;
using System.Threading.Tasks;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace SellPoint.Tests.PedidoService
{
    public class ObtenerTodosAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public ObtenerTodosAsyncTests()
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
        public async Task ObtenerTodosAsync_DeberiaRetornarListaPedidos_CuandoHayPedidos()
        {
            // Arrange
            var listaPedidos = new List<PedidoDTO>
            {
                new PedidoDTO { Id = 1, Estado = "Pendiente" },
                new PedidoDTO { Id = 2, Estado = "Pagado" }
            };

            var expected = OperationResult.Success(listaPedidos, "Pedidos obtenidos correctamente.");

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerTodosAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerTodosAsync();

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal("Pedidos obtenidos correctamente.", result.Message);
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<IEnumerable<PedidoDTO>>(result.Data);
            _pedidoRepositoryMock.Verify(r => r.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoRepositorioFalla()
        {
            // Arrange
            var expected = OperationResult.Failure("No se pudieron obtener los pedidos.");

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerTodosAsync())
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerTodosAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("No se pudieron obtener los pedidos.", result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaManejarExcepcion_Interna()
        {
            // Arrange
            _pedidoRepositoryMock
                .Setup(r => r.ObtenerTodosAsync())
                .ThrowsAsync(new System.Exception("Fallo interno"));

            // Act
            var result = await _pedidoService.ObtenerTodosAsync();

            // Assert
            Assert.False(result.IsSuccess);
            Assert.StartsWith("Error al obtener los pedidos", result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerTodosAsync(), Times.Once);
        }
    }
}