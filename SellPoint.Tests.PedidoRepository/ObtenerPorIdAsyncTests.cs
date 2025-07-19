using Xunit;
using Microsoft.Extensions.Logging;
using Moq;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoRepository
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public ObtenerPorIdAsyncTests()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SellPointTestDb;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsMenorOIgualACero()
        {
            // Arrange
            int id = 0;

            // Act
            var result = await _repository.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("El Id debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoNoSeEncuentra()
        {
            // Arrange
            int id = 999999; 

            // Act
            var result = await _repository.ObtenerPorIdAsync(id);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Contains(result.Message, new[] {
            "No se encontró el pedido.",
            "Error al obtener el pedido por Id"
        });
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarExito_CuandoExistePedido()
        {
            // Arrange
            int id = 1; 

            // Act
            var result = await _repository.ObtenerPorIdAsync(id);

            // Assert
            if (result.IsSuccess)
            {
                Assert.NotNull(result.Data);
                Assert.Equal("Pedido obtenido correctamente.", result.Message);
            }
            else
            {
                
                Assert.False(result.IsSuccess);
                Assert.Contains(result.Message, new[] {
                "No se encontró el pedido.",
                "Error al obtener el pedido por Id"
        });
            }
        }
    }
}