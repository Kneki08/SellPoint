using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Orders;
using Xunit;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Aplication.Validations.Mensajes;

namespace SellPoint.Tests.PedidoService
{
    public class ActualizarAsyncTests
    {
        private readonly Mock<IPedidoRepository> _pedidoRepositoryMock;
        private readonly Mock<ILogger<PedidoServiceClass>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly PedidoServiceClass _pedidoService;

        public ActualizarAsyncTests()
        {
            _pedidoRepositoryMock = new Mock<IPedidoRepository>();
            _loggerMock = new Mock<ILogger<PedidoServiceClass>>();
            _configurationMock = new Mock<IConfiguration>();

            _pedidoService = new PedidoServiceClass(
                _pedidoRepositoryMock.Object,
                _loggerMock.Object,
                _configurationMock.Object
            );
        }

        private UpdatePedidoDTO CrearDtoValido()
        {
            return new UpdatePedidoDTO
            {
                Id = 1,
                IdUsuario = 1,
                IdDireccionEnvio = 10,
                CuponId = null,
                Estado = EstadoPedido.EnPreparacion.ToString(),
                MetodoPago = MetodoPago.Tarjeta.ToString(),
                ReferenciaPago = "ABC123456",
                Notas = "Actualizar pedido",
                FechaPedido = DateTime.UtcNow,
                FechaActualizacion = DateTime.Now
            };
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = CrearDtoValido();
            var expectedResult = OperationResult.Success(MensajesValidacion.OperacionExitosa);

            _pedidoRepositoryMock
                .Setup(repo => repo.ActualizarAsync(It.IsAny<Pedido>()))
                .ReturnsAsync(expectedResult);

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal(MensajesValidacion.OperacionExitosa, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<Pedido>()), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _pedidoService.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.EntidadNula, result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<Pedido>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var dto = CrearDtoValido();
            dto.Id = 0;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.IdPedidoInvalido, result.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEstadoEsInvalido(string? estado)
        {
            var dto = CrearDtoValido();
            dto.Estado = estado!;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.EstadoPedidoRequerido, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEstadoNoEsValido()
        {
            var dto = CrearDtoValido();
            dto.Estado = "Inexistente";

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.EstadoPedidoInvalido, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoMetodoPagoSuperaLongitud()
        {
            var dto = CrearDtoValido();
            dto.Estado = EstadoPedido.EnPreparacion.ToString();
            dto.MetodoPago = new string('A', 51);
            dto.FechaActualizacion = DateTime.UtcNow;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.MetodoPagoMuyLargo, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = CrearDtoValido();
            dto.Estado = EstadoPedido.EnPreparacion.ToString();
            dto.ReferenciaPago = new string('B', 101);
            dto.FechaActualizacion = DateTime.UtcNow;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.ReferenciaPagoMuyLarga, result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNotasExceden500Caracteres()
        {
            var dto = CrearDtoValido();
            dto.Estado = EstadoPedido.EnPreparacion.ToString();
            dto.Notas = new string('C', 501);
            dto.FechaActualizacion = DateTime.UtcNow;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.NotasMuyLargas, result.Message);
        }   

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoFechaActualizacionEsInvalida()
        {
            var dto = CrearDtoValido();
            dto.FechaActualizacion = DateTime.MinValue;

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal(MensajesValidacion.FechaActualizacionInvalida, result.Message);
        }
    }
}