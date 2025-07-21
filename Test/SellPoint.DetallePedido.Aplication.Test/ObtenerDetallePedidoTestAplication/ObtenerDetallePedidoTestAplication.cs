using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using SellPoint.Domain.Entities.Orders;

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
        [Fact]
        public async Task ObtenerPorIdAsyncShouldReturnFailureWhenIdIsInvalid()
        {
            var detallePedido = new ObtenerDetallePedidoDTO { ProductoId = 0 };
            const string expectedMessage = "El Id debe ser mayor que cero.";

            var result = await _DetalleService.ObtenerPorIdAsync(detallePedido.ProductoId);

            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }
        [Fact]
        public async Task ObtenerPorIdAsync_ShouldReturnSuccess_WhenIdIsValid()
        {
            int validId = 1;
            var entity = new SellPoint.Domain.Entities.Orders.DetallePedido
            {
                Pedidoid = 1,
                ProductoId = 2,
                Cantidad = 3
            };

            _DetalleRepositoryMock.Setup(x => x.ObtenerPorIdAsync(validId))
                .ReturnsAsync(OperationResult.Success(entity));

            var result = await _DetalleService.ObtenerPorIdAsync(validId);

            Assert.True(result.IsSuccess);
            var dto = Assert.IsType<DetallePedidoDTO>(result.Data);
            Assert.Equal(entity.Pedidoid, dto.PedidoId);
            Assert.Equal(entity.ProductoId, dto.ProductoId);
            Assert.Equal(entity.Cantidad, dto.Cantidad);
        }

        [Fact]
        public async Task ObtenerTodosAsync_ShouldCallRepository()
        {
            var entityList = new List<DetallePedido>
            {
                new DetallePedido { Pedidoid = 1, ProductoId = 2, Cantidad = 3 }
            };

            _DetalleRepositoryMock.Setup(x => x.ObtenerTodosAsync())
                .ReturnsAsync(OperationResult.Success(entityList));

            var result = await _DetalleService.ObtenerTodosAsync();

            Assert.True(result.IsSuccess);
            var list = Assert.IsType<List<DetallePedidoDTO>>(result.Data);
            Assert.Single(list);
        }

    }
}
