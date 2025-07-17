using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService; // alias
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration; 
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoService
{
    public class AgregarAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock; //  Nuevo mock
        private readonly PedidoServiceClass _pedidoService;

        public AgregarAsyncTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _loggerMock = new Mock<ILogger<PedidoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>(); //  Instancia

            _pedidoService = new PedidoServiceClass(
                _pedidoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object //  Inyectado al constructor
            );
        }

        [Fact]
public async Task AgregarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
{
    // Arrange
    var dto = new SavePedidoDTO
    {
        UsuarioId = 1,
        Subtotal = 100,
        Descuento = 10,
        CostoEnvio = 5,
        Total = 95,
        MetodoPago = "Tarjeta"
    };

    var expectedResult = OperationResult.Success("Operación exitosa."); //  mensaje actualizado

    _pedidoRepositoryMock
        .Setup(repo => repo.AgregarAsync(dto))
        .ReturnsAsync(expectedResult);

    // Act
    var result = await _pedidoService.AgregarAsync(dto);

    // Assert
    Assert.True(result.IsSuccess);
    Assert.Equal("Operación exitosa.", result.Message); //  mensaje actualizado
    _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(dto), Times.Once);
}

[Fact]
public async Task AgregarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
{
    // Act
    var result = await _pedidoService.AgregarAsync(null!);

    // Assert
    Assert.False(result.IsSuccess);
    Assert.Equal("La entidad no puede ser nula.", result.Message); //  mensaje actualizado
    _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SavePedidoDTO>()), Times.Never);
}

[Fact]
public async Task AgregarAsync_DeberiaRetornarError_CuandoUsuarioIdEsNegativo()
{
    // Arrange
    var dto = new SavePedidoDTO
    {
        UsuarioId = -5
    };

    // Act
    var result = await _pedidoService.AgregarAsync(dto);

    // Assert
    Assert.False(result.IsSuccess);
    Assert.Equal("El ID del usuario debe ser mayor que cero.", result.Message); //  mensaje actualizado
    _pedidoRepositoryMock.Verify(repo => repo.AgregarAsync(It.IsAny<SavePedidoDTO>()), Times.Never);
}
    }
}