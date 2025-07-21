using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SellPoint.Aplication.Services.DetallepedidoService;

using SellPoint.Aplication.Dtos.DetallePedido;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Aplication.Interfaces.Repositorios;

namespace SellPoint.Tests.DetallePedidoService
{
    public class DetallepedidoServiceTests
    {
        private readonly Mock<IDetallePedidoRepository> _repositoryMock;
        private readonly Mock<ILogger<DetallepedidoService>> _loggerMock;
        private readonly Mock<IConfiguration> _configMock;
        private readonly DetallepedidoService _service;

        public DetallepedidoServiceTests()
        {
            _repositoryMock = new Mock<IDetallePedidoRepository>();
            _loggerMock = new Mock<ILogger<DetallepedidoService>>();
            _configMock = new Mock<IConfiguration>();

            _service = new DetallepedidoService(
                _repositoryMock.Object,
                _loggerMock.Object,
                _configMock.Object);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_SiIdEsInvalido()
        {
            // Arrange
            var dto = new RemoveDetallePedidoDTO { Id = 0 };

            // Act
            var result = await _service.EliminarAsync(dto);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id debe ser mayor que cero.", result.Message);
        }

       
    }
  }

