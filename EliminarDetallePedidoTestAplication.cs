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
//using Moq;
using Xunit;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using System.Threading.Tasks;
using System;
using SellPoint.Aplication.Services.DetallepedidoService;


namespace SellPoint.Pesistence.Test.DetallePedidoTest.EliminarDetallePedidoTest
{
    public class EliminarDetallePedidoTest
    {
        private readonly Mock<IDetallePedidoRepository> _DetalleRepositoryMock;
        private readonly Mock<ILogger<DetalleServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock; //  Nuevo mock
        private readonly DetalleServiceClass _DetalleService;


        public EliminarDetallePedidoTest()
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

        [Fact]
        public async Task EliminarAsync_ShouldReturnFailure_WhenIdIsInvalid()
        {
            // Arrange
            var detallePedido = new RemoveDetallePedidoDTO { Id = 0 };
            string expectedMessage = "El Id debe ser mayor que cero.";

            // Configurar el mock
            _DetalleRepositoryMock.Setup(x => x.EliminarAsync(It.IsAny<RemoveDetallePedidoDTO>()))
                .ReturnsAsync(new OperationResult
                {
                    IsSuccess = false,
                    Message = expectedMessage
                });

            // Act
            var result = await _DetalleService.EliminarAsync(detallePedido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }
        public async Task EliminarAsync_ShouldReturnFailure_WhenRemoveDtoIsNull()
        {
            // Arrange
            RemoveDetallePedidoDTO nullDto = null;

            // Act
            var result = await _DetalleService.EliminarAsync(nullDto);

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Se requiere crear un DTO")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task EliminarAsync_ShouldCallRepository_WhenDtoIsValid()
        {
            // Arrange
            var validDto = new RemoveDetallePedidoDTO { Id = 1 };
            _DetalleRepositoryMock.Setup(x => x.EliminarAsync(validDto))
                .ReturnsAsync(OperationResult.Success("Eliminado correctamente"));

            // Act
            var result = await _DetalleService.EliminarAsync(validDto);

            // Assert
            Assert.True(result.IsSuccess);
            _DetalleRepositoryMock.Verify(x => x.EliminarAsync(validDto), Times.Once);
            
        }

        [Fact]
        public async Task EliminarAsync_ShouldLogError_WhenRepositoryFails()
        {
            // Arrange
            var validDto = new RemoveDetallePedidoDTO { Id = 1 };
            _DetalleRepositoryMock.Setup(x => x.EliminarAsync(validDto))
                .ReturnsAsync(OperationResult.Failure("Error de repositorio"));

            // Act
            var result = await _DetalleService.EliminarAsync(validDto);

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No se pudo eliminar el detalle del pedido")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }


    }
}
