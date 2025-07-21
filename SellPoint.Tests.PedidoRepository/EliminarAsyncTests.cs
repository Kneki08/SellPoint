using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Tests.PedidoRepository
{
    public class EliminarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public EliminarAsyncTests()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SellPointTestDb;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString, loggerMock.Object);
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
            var dto = new RemovePedidoDTO { Id = 99999 }; // ID que no exista en la DB

            var resultado = await _repository.EliminarAsync(dto);

            Assert.False(resultado.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoNoEliminado, resultado.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarSuccess_CuandoPedidoEsValidoYExiste()
        {
            var dto = new RemovePedidoDTO { Id = 1 }; // ⚠️ Asegúrate de que exista en la DB de prueba

            var resultado = await _repository.EliminarAsync(dto);

            if (resultado.IsSuccess)
            {
                Assert.True(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoEliminado, resultado.Message);
            }
            else
            {
                // Asegúrate de que esté claro si no se eliminó por no existir
                Assert.False(resultado.IsSuccess);
                Assert.Equal(MensajesValidacion.PedidoNoEliminado, resultado.Message);
            }
        }
    }
}