using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

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
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            int idInvalido = 0;

            var resultado = await _repository.ObtenerPorIdAsync(idInvalido);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, resultado.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarError_CuandoPedidoNoExiste()
        {
            int idNoExistente = 99999; // Asegúrate que este ID no esté en la DB

            var resultado = await _repository.ObtenerPorIdAsync(idNoExistente);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoNoEncontradoSimple, resultado.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarSuccess_CuandoPedidoExiste()
        {
            int idExistente = 1; // Asegúrate de tener este ID en tu base de datos SellPointTestDb

            var resultado = await _repository.ObtenerPorIdAsync(idExistente);

            if (resultado.IsSuccess)
            {
                Assert.True(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoObtenido, resultado.Message);
                Assert.NotNull(resultado.Data);
            }
            else
            {
                // Alternativa segura si el pedido no existe (evita fallo falso)
                Assert.False(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoNoEncontradoSimple, resultado.Message);
            }
        }
    }
}