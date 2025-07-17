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
    public class EliminarAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public EliminarAsyncTests()
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
        public async Task EliminarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            // Act
            var resultado = await _pedidoService.EliminarAsync(null!);

            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("El ID del pedido es inválido.", resultado.Message); 
            _pedidoRepositoryMock.Verify(r => r.EliminarAsync(It.IsAny<RemovePedidoDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            // Arrange
            var dto = new RemovePedidoDTO { Id = -10 };

            // Act
            var resultado = await _pedidoService.EliminarAsync(dto);

            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("El ID del pedido es inválido.", resultado.Message); 
            _pedidoRepositoryMock.Verify(r => r.EliminarAsync(It.IsAny<RemovePedidoDTO>()), Times.Never);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            // Arrange
            var dto = new RemovePedidoDTO { Id = 5 };
            var expected = OperationResult.Success("Operación exitosa.");

            _pedidoRepositoryMock
                .Setup(r => r.EliminarAsync(dto))
                .ReturnsAsync(expected);

            // Act
            var resultado = await _pedidoService.EliminarAsync(dto);

            // Assert
            Assert.True(resultado.IsSuccess);
            Assert.Equal("Operación exitosa.", resultado.Message); 
            _pedidoRepositoryMock.Verify(r => r.EliminarAsync(dto), Times.Once);
        }
    }
}