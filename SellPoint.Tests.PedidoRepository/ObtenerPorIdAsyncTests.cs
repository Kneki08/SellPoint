using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoRepository
{
    public class ObtenerPorIdAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public ObtenerPorIdAsyncTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string not found.");

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
            int idNoExistente = 99999;

            var resultado = await _repository.ObtenerPorIdAsync(idNoExistente);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoNoEncontradoSimple, resultado.Message);
        }

        [Fact]
        public async Task ObtenerPorIdAsync_DeberiaRetornarSuccess_CuandoPedidoExiste()
        {
            int idExistente = 1;

            var resultado = await _repository.ObtenerPorIdAsync(idExistente);

            if (resultado.IsSuccess)
            {
                Assert.True(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoObtenido, resultado.Message);
                Assert.NotNull(resultado.Data);
            }
            else
            {
                Assert.False(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoNoEncontradoSimple, resultado.Message);
            }
        }
    }
}