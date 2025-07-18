using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
//using Moq;
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
        public async Task AgregarAsync_ShouldReturnFailure_WhenDetallePedidoIsNull()
        {
            SaveDetallePedidoDTO? detallePedido = null;
            const string expectedMessage = "La entidad no puede ser nula.";

            // Act
            var result = await _DetalleService.AgregarAsync(detallePedido!); // Use null-forgiving operator (!)

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }


        [Fact]
        public async Task AgregarAsync_ShouldReturnFailure_WhenPedidoIdIsInvalid()
        {
            // Arrange
            var detallePedido = new SaveDetallePedidoDTO
            {
                PedidoId = 0,
                ProductoId = 1,
                Cantidad = 2
            };
            string expectedMessage = "El PedidoId debe ser mayor que cero.";

            // Configurar el mock
            _DetalleRepositoryMock.Setup(x => x.AgregarAsync(It.IsAny<SaveDetallePedidoDTO>()))
                .ReturnsAsync(new OperationResult
                {
                    IsSuccess = false,
                    Message = expectedMessage
                });

            // Act
            var result = await _DetalleService.AgregarAsync(detallePedido);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }
    }
}
