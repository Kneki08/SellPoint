using Xunit;
using Moq;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using SellPoint.Domainn.Entities.Orders;
using PedidoRepositoryClass = SellPoint.Persistence.Repositories.PedidoRepository;
using SellPoint.Domain.Base;
using SellPoint.Aplication.Validations.Mensajes;
using System.IO;
using System.Threading.Tasks;

namespace SellPoint.Tests.PedidoRepository
{
    public class ActualizarAsyncTests
    {
        private readonly PedidoRepositoryClass _repository;

        public ActualizarAsyncTests()
        {
            // Leer la configuración desde appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var loggerMock = new Mock<ILogger<PedidoRepositoryClass>>();
            _repository = new PedidoRepositoryClass(connectionString!, loggerMock.Object);
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
                MetodoPago = (MetodoPago)999,
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
                Fecha_actualizacion = DateTime.MinValue
            };

            var result = await _repository.ActualizarAsync(pedido);
            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.FechaActualizacionInvalida, result.Message);
        }
    }
}