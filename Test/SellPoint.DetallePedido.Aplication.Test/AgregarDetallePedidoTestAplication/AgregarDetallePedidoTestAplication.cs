using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
//using Moq;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using SellPoint.Aplication.Services.DetallepedidoService;

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
        [Fact]
        public async Task AgregarAsync_DeberiaRetornarError_CuandoDtoEsNulo()
        {
            // Arrange
            SaveDetallePedidoDTO? dto = null;
            var expectedMessage = "La entidad no puede ser nula.";

            // Act
            var result = await _DetalleService.AgregarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }

        public async Task AgregarAsyncDeberiaValidarCampos(int pedidoId, int productoId, int cantidad, string campoInvalido)
        {
            // Arrange
            var dto = new SaveDetallePedidoDTO
            {
                PedidoId = pedidoId,
                ProductoId = productoId,
                Cantidad = cantidad
            };

            // Act
            var result = await _DetalleService.AgregarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(campoInvalido, result.Message);
        }
        [Fact]
        public async Task AgregarAsync_ShouldReturnFailure_WhenSaveDtoIsNull()
        {
            // Arrange
            SaveDetallePedidoDTO? nullDto = null;

            // Act
            var result = await _DetalleService.AgregarAsync(nullDto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }


        [Fact]
        public async Task AgregarAsync_ShouldCallRepository_WhenDtoIsValid()
        {
            // Arrange
            var validDto = new SaveDetallePedidoDTO
            {
                PedidoId = 1,
                ProductoId = 1,
                Cantidad = 2,
                PrecioUnitario = 10.5m,
                Subtotal = 21.0m
            };

            _DetalleRepositoryMock.Setup(x => x.AgregarAsync(validDto))
                .ReturnsAsync(OperationResult.Success("Agregado correctamente"));

            // Act
            var result = await _DetalleService.AgregarAsync(validDto);

            // Assert
            Assert.True(result.IsSuccess);
            _DetalleRepositoryMock.Verify(x => x.AgregarAsync(validDto), Times.Once);
            
        }

        [Fact]
        public async Task AgregarAsync_ShouldLogError_WhenRepositoryFails()
        {
            // Arrange
            var validDto = new SaveDetallePedidoDTO
            {
                PedidoId = 1,
                ProductoId = 1,
                Cantidad = 2,
                PrecioUnitario = 10.5m,
                Subtotal = 21.0m
            };

            _DetalleRepositoryMock.Setup(x => x.AgregarAsync(validDto))
                .ReturnsAsync(OperationResult.Failure("Error de repositorio"));

            // Act
            var result = await _DetalleService.AgregarAsync(validDto);

            // Assert
            Assert.False(result.IsSuccess);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("No se pudo agregar el detalle pedido")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()),
                Times.Once);
        }

    }
}
