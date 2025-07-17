using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Aplication.Dtos.Pedido;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;

namespace SellPoint.Tests.PedidoRepository
{
    public class ActualizarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public ActualizarAsyncTests()
        {
            var connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=SellPointTestDb;";
            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString, loggerMock.Object);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEntidadEsNula()
        {
            var result = await _repository.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var dto = new UpdatePedidoDTO { Id = 0 };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El Id del pedido debe ser mayor que cero.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoFechaActualizacionEsInvalida()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                FechaActualizacion = DateTime.MinValue
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La fecha de actualización no es válida.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoMetodoPagoSuperaLongitud()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                MetodoPago = new string('A', 51),
                FechaActualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El método de pago no debe superar los 50 caracteres.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoReferenciaPagoSuperaLongitud()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                ReferenciaPago = new string('B', 101),
                FechaActualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La referencia de pago no debe superar los 100 caracteres.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNotasSuperanLongitud()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Notas = new string('C', 501),
                FechaActualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Las notas no deben superar los 500 caracteres.", result.Message);
        }
    }
}