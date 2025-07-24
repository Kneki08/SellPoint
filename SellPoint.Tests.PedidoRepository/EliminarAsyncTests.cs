using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SellPoint.Aplication.Dtos.Pedido;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;
using System.IO;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoRepository
{
    public class EliminarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public EliminarAsyncTests()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString!, loggerMock.Object);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            RemovePedidoDTO? dto = null;

            var resultado = await _repository.EliminarAsync(dto!);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.EntidadNula, resultado.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var dto = new RemovePedidoDTO { Id = 0 };

            var resultado = await _repository.EliminarAsync(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, resultado.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoSPNoEliminaNada()
        {
            var dto = new RemovePedidoDTO { Id = 99999 }; // ID que no existe

            var resultado = await _repository.EliminarAsync(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoNoEliminado, resultado.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarSuccess_CuandoPedidoEsValidoYExiste()
        {
            var dto = new RemovePedidoDTO { Id = 1 }; // ⚠️ Asegúrate que este ID exista en la DB de prueba

            var resultado = await _repository.EliminarAsync(dto);

            Assert.True(resultado.IsSuccess, resultado.Message);
            Assert.Equal(MensajesValidacion.PedidoEliminado, resultado.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoOcurreExcepcionInesperada()
        {
            var connectionStringInvalida = "Data Source=servidor_invalido;Initial Catalog=BD;Integrated Security=True;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            var repositorioFalla = new PedidoRepositoryClass(connectionStringInvalida, loggerMock.Object);

            var dto = new RemovePedidoDTO { Id = 1 };

            var resultado = await repositorioFalla.EliminarAsync(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.ErrorEliminarPedido, resultado.Message);
        }
    }
}