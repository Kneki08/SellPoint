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
using SellPoint.Persistence.Repositories;
//using Moq;
using Xunit;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using System.Threading.Tasks;
using System;


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



    }
}
