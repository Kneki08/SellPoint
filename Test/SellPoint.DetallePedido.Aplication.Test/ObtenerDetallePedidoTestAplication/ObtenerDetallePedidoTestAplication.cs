using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using System.Threading.Tasks;

namespace SellPoint.Pesistence.Test.DetallePedidoTest.ObtenerDetallePedidoTest
{
    public class ObtenerDetallePedidoTest
    {

        private readonly Mock<IDetallePedidoRepository> _DetalleRepositoryMock;
        private readonly Mock<ILogger<DetalleServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock; //  Nuevo mock
        private readonly DetalleServiceClass _DetalleService;


        public ObtenerDetallePedidoTest()
        {
            _DetalleRepositoryMock = new Mock<IDetallePedidoRepository>();
            _loggerMock = new Mock<ILogger<DetalleServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            // Configuración básica del mock de configuración
            _configurationMock.Setup(x => x.GetSection(It.IsAny<string>())).Returns(new Mock<IConfigurationSection>().Object);

            _DetalleService = new DetalleServiceClass(
                _DetalleRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );

        }
        public async Task ObtenerPorIdAsyncShouldReturnFailureWhenIdIsInvalid()
        {
            // Arrange
            var detallePedido = new ObtenerDetallePedidoDTO { ProductoId = 0 };
            const string expectedMessage = "El Id debe ser mayor que cero.";

            // Act
            var result = await _DetalleService.ObtenerPorIdAsync(detallePedido.ProductoId);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_ShouldReturnSuccess_WhenIdIsValid()
        {
            // Arrange
            int validId = 1;
            _DetalleRepositoryMock.Setup(x => x.ObtenerPorIdAsync(validId))
                .ReturnsAsync(new OperationResult { IsSuccess = true });

            // Act
            var result = await _DetalleService.ObtenerPorIdAsync(validId);

            // Assert
            Assert.True(result.IsSuccess);
        }
        [Fact]
        public async Task ObtenerTodosAsync_ShouldCallRepository()
        {
            // Arrange
            var expectedResult = OperationResult.Success("Datos obtenidos");
            _DetalleRepositoryMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _DetalleService.ObtenerTodosAsync();

            // Assert
            Assert.Equal(expectedResult, result);
            _DetalleRepositoryMock.Verify(x => x.ObtenerTodosAsync(), Times.Once);
        }

        [Fact]
        public async Task ObtenerTodosAsync_ShouldLogError_WhenRepositoryFails()
        {
            // Arrange
            _DetalleRepositoryMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(OperationResult.Failure("Error de repositorio"));

            // Act
            var result = await _DetalleService.ObtenerTodosAsync();

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No se pudo obtener todos los detalles del pedido")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_ShouldCallRepository_WhenIdIsValid()
        {
            // Arrange
            int validId = 1;
            var expectedResult = OperationResult.Success("Datos obtenidos");
            _DetalleRepositoryMock.Setup(x => x.ObtenerPorIdAsync(validId))
                .ReturnsAsync(expectedResult);

            // Act
            var result = await _DetalleService.ObtenerPorIdAsync(validId);

            // Assert
            Assert.Equal(expectedResult, result);
            _DetalleRepositoryMock.Verify(x => x.ObtenerPorIdAsync(validId), Times.Once);
           
        }

        [Fact]
        public async Task ObtenerPorIdAsync_ShouldLogError_WhenRepositoryReturnsNull()
        {
            // Arrange
            int validId = 1;
            _DetalleRepositoryMock.Setup(x => x.ObtenerPorIdAsync(validId))
                .ReturnsAsync((OperationResult)null);

            // Act
            var result = await _DetalleService.ObtenerPorIdAsync(validId);

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("El repositorio devolvió un valor nulo")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }


    }
}
