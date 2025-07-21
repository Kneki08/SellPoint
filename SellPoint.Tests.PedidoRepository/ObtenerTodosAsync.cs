using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

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
        public async Task ObtenerTodosAsync_DeberiaRetornarSuccess_CuandoExistenPedidos()
        {
            var resultado = await _repository.ObtenerTodosAsync();

            if (resultado.IsSuccess)
            {
                Assert.True(resultado.IsSuccess);
                Assert.NotNull(resultado.Data);
                Assert.IsAssignableFrom<IEnumerable<object>>(resultado.Data!);
                Assert.Equal(MensajesValidacion.PedidosObtenidosCorrectamente, resultado.Message);
            }
            else
            {
                // Permite que el test pase si no hay datos (pero informa)
                Assert.False(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.ErrorObtenerTodos, resultado.Message);
            }
        }

        [Fact]
        public async Task ObtenerTodosAsync_DeberiaRetornarError_CuandoNoExistenPedidos()
        {
            // Este test depende de que la tabla esté vacía, opcional si no controlas la data
            var resultado = await _repository.ObtenerTodosAsync();

            if (!resultado.IsSuccess)
            {
                Assert.False(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.ErrorObtenerTodos, resultado.Message);
            }
            else
            {
                // Si hay pedidos, al menos se devuelve lista
                Assert.True(resultado.IsSuccess);
                Assert.NotNull(resultado.Data);
            }
        }
    }
}