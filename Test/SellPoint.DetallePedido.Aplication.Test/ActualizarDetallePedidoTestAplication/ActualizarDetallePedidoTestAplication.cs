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
        public async Task ActualizarAsync_DeberiaLlamarAlRepositorio_CuandoDTOValido()
        {
            // Arrange
            var dto = new UpdateDetallePedidoDTO
            {
                Id = 1,
                PedidoId = 1,
                ProductoId = 1,
                Cantidad = 2,
                PrecioUnitario = 15
            };

            _DetalleRepositoryMock.Setup(x => x.ActualizarAsync(It.IsAny<DetallePedido>()))
                .ReturnsAsync(OperationResult.Success("Actualizado correctamente"));

            // Act
            var resultado = await _DetalleService.ActualizarAsync(dto);

            // Assert
            Assert.True(resultado.IsSuccess);
            _DetalleRepositoryMock.Verify(x => x.ActualizarAsync(It.IsAny<DetallePedido>()), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarFallo_SiRepositorioFalla()
        {
            // Arrange
            var dto = new UpdateDetallePedidoDTO
            {
                Id = 1,
                PedidoId = 1,
                ProductoId = 1,
                Cantidad = 3,
                PrecioUnitario = 12.5m
            };
            
            _DetalleRepositoryMock.Setup(x => x.ActualizarAsync(It.IsAny<DetallePedido>()))
                .ReturnsAsync(OperationResult.Failure("Error de repositorio"));

            // Act
            var resultado = await _DetalleService.ActualizarAsync(dto);

            // Assert
            Assert.False(resultado.IsSuccess);
            Assert.Equal("Error de repositorio", resultado.Message);
        }
    }
}
