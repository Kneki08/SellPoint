using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;

namespace SellPoint.Tests.PedidoRepository
{
    public class ObtenerTodosAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public ObtenerTodosAsyncTests()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SellPointTestDb;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarListaVacia_CuandoNoHayPedidos()
        {
            var result = await _repository.ObtenerTodosAsync();

            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty((IEnumerable<object>)result.Data!);
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarLista_CuandoHayPedidos()
        {
            // Esta prueba solo será válida si previamente insertas datos en la BD de prueba
            // o ya existen registros.
            var result = await _repository.ObtenerTodosAsync();

            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.IsAssignableFrom<IEnumerable<object>>(result.Data!);
        }
    }
}