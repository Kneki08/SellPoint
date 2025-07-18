using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Persistence.Repositories;
//using Moq;
using Xunit;
using DetalleServiceClass = SellPoint.Aplication.Services.DetallepedidoService.DetallepedidoService; // alias
using System.Threading.Tasks;
using SellPoint.Aplication.Dtos.DetallePedido;



namespace SellPoint.Pesistence.Test.DetallePedidoTest.ActualizarDetallePedidoTest
{
    public class ActualizarDetallePedidoTest
    {
        private readonly Mock<IDetallePedidoRepository> _DetalleRepositoryMock;
        private readonly Mock<ILogger<DetalleServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock; //  Nuevo mock
        private readonly DetalleServiceClass _DetalleService;


        public ActualizarDetallePedidoTest()
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
        public async Task ActualizarAsync_ShouldReturnFailure_WhenDetallePedidoIsNull()
        {
            // Arrange
            UpdateDetallePedidoDTO? detallePedido = null;
            const string expectedMessage = "La entidad no puede ser nula.";

            // Configuración específica para null
            _DetalleRepositoryMock.Setup(x => x.ActualizarAsync(It.IsAny<UpdateDetallePedidoDTO>()))
                .ReturnsAsync(new OperationResult
                {
                    IsSuccess = false,
                    Message = expectedMessage
                });

            // Act
            var result = await _DetalleService.ActualizarAsync(detallePedido!); // Use null-forgiving operator (!)

            // Assert
            Assert.IsType<OperationResult>(result);
            Assert.False(result.IsSuccess);
            Assert.Equal(expectedMessage, result.Message);
        }

    }
}
