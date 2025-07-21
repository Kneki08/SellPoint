using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domain.Entities.Orders;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias


namespace SellPoint.Pesistence.Test.DetallePedidoTest.AgregarDetallePedidoTest
{
    public class AgregarDetallePedidoTest
    {
        private readonly Mock<IDetallePedidoRepository> _DetalleRepositoryMock;
        private readonly Mock<ILogger<DetalleServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock; //  Nuevo mock
        private readonly DetalleServiceClass _DetalleService;


        public AgregarDetallePedidoTest()
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
        public async Task AddAsync_ShouldReturnFailure_WhenDtoIsNull()
        {
            // Arrange
            SaveDetallePedidoDTO? dto = null;
            var expectedMessage = "El DTO no puede ser nulo.";

            // Act
            var result = await _DetalleService.AgregarAsync(dto!);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }



        [Fact]
        public async Task AddAsync_ShouldLogError_WhenRepositoryThrows()
        {
            // Arrange
            var dto = new SaveDetallePedidoDTO
            {
                PedidoId = 1,
                ProductoId = 2,
                Cantidad = 3,
                PrecioUnitario = 20.5m
            };

            _DetalleRepositoryMock.Setup(x => x.AgregarAsync(It.IsAny<DetallePedido>()))
                .ThrowsAsync(new Exception("Error inesperado"));

            // Act
            var result = await _DetalleService.AgregarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Error interno.", result.Message);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, _) => v.ToString().Contains("Error al agregar el detalle del pedido")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }
    }
}

