using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using SellPoint.Domainn.Entities.Orders;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;

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
            Assert.Equal(MensajesValidacion.EntidadNula, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var pedido = new Pedido
            {
                Id = 0,
                IdUsuario = 1,
                Estado = EstadoPedido.EnPreparacion,
                MetodoPago = MetodoPago.Tarjeta,
                Fecha_actualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(pedido);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.PedidoIdInvalido, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoMetodoPagoEsInvalido()
        {
            var pedido = new Pedido
            {
                Id = 1,
                IdUsuario = 1,
                Estado = EstadoPedido.EnPreparacion,
                MetodoPago = (MetodoPago)999, // Valor inválido
                Fecha_actualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(pedido);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoNoValido, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var pedido = new Pedido
            {
                Id = 1,
                IdUsuario = 1,
                Estado = EstadoPedido.EnPreparacion,
                MetodoPago = MetodoPago.Tarjeta,
                ReferenciaPago = new string('X', 101),
                Fecha_actualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(pedido);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNotasSonMuyLargas()
        {
            var pedido = new Pedido
            {
                Id = 1,
                IdUsuario = 1,
                Estado = EstadoPedido.EnPreparacion,
                MetodoPago = MetodoPago.Tarjeta,
                Notas = new string('Y', 501),
                Fecha_actualizacion = DateTime.Now
            };

            var result = await _repository.ActualizarAsync(pedido);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoFechaActualizacionEsInvalida()
        {
            var pedido = new Pedido
            {
                Id = 1,
                IdUsuario = 1,
                Estado = EstadoPedido.EnPreparacion,
                MetodoPago = MetodoPago.Tarjeta,
                Fecha_actualizacion = DateTime.MinValue // Fecha inválida
            };

            var result = await _repository.ActualizarAsync(pedido);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.FechaActualizacionInvalida, result.Message);
        }
    }
}