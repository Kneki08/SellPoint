using Moq;
using Xunit;
using SellPoint.Aplication.Dtos.Pedido;
using SellPoint.Aplication.Interfaces.Repositorios;
using PedidoServiceClass = SellPoint.Aplication.Services.PedidoService.PedidoService;
using SellPoint.Domain.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;

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

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarExito_CuandoDTOEsValido()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Pendiente",
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC123456",
                Notas = "Actualizar pedido",
                FechaActualizacion = DateTime.Now
            };

            var expectedResult = OperationResult.Success("Operación exitosa.");

            _pedidoRepositoryMock
                .Setup(repo => repo.ActualizarAsync(dto))
                .ReturnsAsync(expectedResult);

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.True(result.IsSuccess);
            Assert.Equal("Operación exitosa.", result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.ActualizarAsync(dto), Times.Once);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoDTOEsNulo()
        {
            var result = await _pedidoService.ActualizarAsync(null!);

            Assert.False(result.IsSuccess);
            Assert.Equal("La entidad no puede ser nula.", result.Message);
            _pedidoRepositoryMock.Verify(repo => repo.ActualizarAsync(It.IsAny<UpdatePedidoDTO>()), Times.Never);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoIdEsInvalido()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 0,
                Estado = "Pendiente",
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC",
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El ID del pedido debe ser mayor que cero.", result.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEstadoEsInvalido(string estado)
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = estado,
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC123",
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El estado del pedido es obligatorio.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoEstadoNoEsValido()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Inexistente",
                MetodoPago = "Tarjeta",
                ReferenciaPago = "ABC123",
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El estado del pedido no es válido.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoMetodoPagoSuperaLongitud()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Pagado",
                MetodoPago = new string('A', 51),
                ReferenciaPago = "ABC123",
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("El método de pago no debe superar los 50 caracteres.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoReferenciaPagoEsMuyLarga()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Enviado",
                MetodoPago = "Transferencia",
                ReferenciaPago = new string('B', 101),
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La referencia de pago no debe superar los 100 caracteres.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoNotasExceden500Caracteres()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Pagado",
                MetodoPago = "Transferencia",
                ReferenciaPago = "Ref123",
                Notas = new string('C', 501),
                FechaActualizacion = DateTime.Now
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("Las notas no deben superar los 500 caracteres.", result.Message);
        }

        [Fact]
        public async Task ActualizarAsync_DeberiaRetornarError_CuandoFechaActualizacionEsInvalida()
        {
            var dto = new UpdatePedidoDTO
            {
                Id = 1,
                Estado = "Cancelado",
                MetodoPago = "Tarjeta",
                ReferenciaPago = "Ref001",
                FechaActualizacion = DateTime.MinValue
            };

            var result = await _pedidoService.ActualizarAsync(dto);

            Assert.False(result.IsSuccess);
            Assert.Equal("La fecha de actualización no es válida.", result.Message);
        }
    }
}
