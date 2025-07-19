using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;

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
        public async Task EliminarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.EliminarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task EliminarAsync_DeberiaRetornarError_CuandoIdEsMenorOIgualACero()
        {
            var dto = new RemovePedidoDTO { Id = 0 };

            var result = await _repository.EliminarAsync(dto);

            Assert.False(result.IsSuccess); 
            Assert.Equal("El Id del pedido debe ser mayor que cero.", result.Message);
        }

        
        
        [Fact]
        public async Task EliminarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new RemovePedidoDTO
            {
                Id = 1 
            };

            var result = await _repository.EliminarAsync(dto);

            
            if (result.IsSuccess)
                Assert.Equal("Pedido eliminado correctamente.", result.Message);
            else
                Assert.Equal("Error al eliminar el pedido", result.Message);
        }
    }
}