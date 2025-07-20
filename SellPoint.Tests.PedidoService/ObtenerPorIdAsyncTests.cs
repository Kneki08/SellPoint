using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using SellPoint.Aplication.Validations.Mensajes; // ← Importante

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
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoNoSeEncuentra()
        {
            // Arrange
            int id = 99;
            var mensajeEsperado = string.Format(MensajesValidacion.PedidoNoEncontrado, id);
            var expected = OperationResult.Failure(mensajeEsperado);

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.Is<int>(x => x == id)))
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(mensajeEsperado, result.Message);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(id), Times.Once);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoExistePedido()
        {
            // Arrange
            int id = 1;
            var expectedDto = new PedidoDTO
            {
                Id = id,
                IdUsuario = 1,
                FechaPedido = DateTime.UtcNow,
                Estado = "Pagado",
                IdDireccionEnvio = 10,
                CuponId = null,
                MetodoPago = "Tarjeta",
                ReferenciaPago = "XYZ789"
            };
            var expected = OperationResult.Success(expectedDto, MensajesValidacion.PedidoEncontrado);

            _pedidoRepositoryMock
                .Setup(r => r.ObtenerPorIdAsync(It.Is<int>(x => x == id)))
                .ReturnsAsync(expected);

            // Act
            var result = await _pedidoService.ObtenerPorIdAsync(id);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoEncontrado, result.Message);
            Assert.NotNull(result.Data);
            Assert.IsType<PedidoDTO>(result.Data);
            Assert.Equal(id, ((PedidoDTO)result.Data!).Id);
            _pedidoRepositoryMock.Verify(r => r.ObtenerPorIdAsync(id), Times.Once);
        }
    }
}