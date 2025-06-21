namespace SellPoint.Persistence.Repositorios
{
    public class CuponRepository : ICuponRepository
    {
       private readonly string _connectionString;
       public CuponRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public async Task<OperationResult> CreateCuponAsync(Cupon cupon)
        {
            await _connectionString.Cupones.AddAsync(cupon);
            await _connectionString.SaveChangesAsync();
            return OperationResult.Ok("Cupón creado correctamente.");
        }

        public async Task<OperationResult> UpdateCuponAsync(Cupon cupon)
        {
            var existente = await _connectionString.Cupones.FindAsync(cupon.Id);
            if (existente == null)
                return OperationResult.Fail("Cupón no encontrado.");

            existente.Codigo = cupon.Codigo;
            existente.Descripcion = cupon.Descripcion;
            existente.TipoDescuento = cupon.TipoDescuento;
            existente.ValorDescuento = cupon.ValorDescuento;
            existente.MontoMinimo = cupon.MontoMinimo;
            existente.FechaInicio = cupon.FechaInicio;
            existente.FechaVencimiento = cupon.FechaVencimiento;
            existente.UsosMaximos = cupon.UsosMaximos;
            existente.Activo = cupon.Activo;

            _connectionString.Cupones.Update(existente);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Cupón actualizado correctamente.");
        }

        public async Task<OperationResult> DeleteCuponAsync(int cuponId)
        {
            var cupon = await _connectionString.Cupones.FindAsync(cuponId);
            if (cupon == null)
                return OperationResult.Fail("Cupón no encontrado.");

            _connectionString.Cupones.Remove(cupon);
            await _connectionString.SaveChangesAsync();

            return OperationResult.Ok("Cupón eliminado correctamente.");
        }

        public async Task<OperationResult<Cupon>> GetCuponByIdAsync(int cuponId)
        {
            var cupon = await _connectionString.Cupones.FindAsync(cuponId);
            if (cupon == null)
                return OperationResult<Cupon>.Fail("Cupón no encontrado.");

            return new OperationResult<Cupon>
            {
                Success = true,
                Data = cupon,
                Message = "Cupón obtenido correctamente."
            };
        }

        public async Task<OperationResult<List<Cupon>>> GetAllCuponAsync()
        {
            var cupones = await _connectionString.Cupones.ToListAsync();
            return new OperationResult<List<Cupon>>
            {
                Success = true,
                Data = cupones,
                Message = "Lista de cupones obtenida correctamente."
            };
        }

        public async Task<OperationResult<Cupon>> ValidateCodigoCuponAsync(string codigo)
        {
            var cupon = await _connectionString.Cupones
                .FirstOrDefaultAsync(c => c.Codigo == codigo && c.Activo);

            if (cupon == null)
                return OperationResult<Cupon>.Fail("Cupón inválido o inactivo.");

            if (cupon.FechaVencimiento < DateTime.UtcNow)
                return OperationResult<Cupon>.Fail("Cupón expirado.");

            if (cupon.UsosMaximos.HasValue && cupon.UsosActuales >= cupon.UsosMaximos)
                return OperationResult<Cupon>.Fail("El cupón ha alcanzado el número máximo de usos.");

            return new OperationResult<Cupon>
            {
                Success = true,
                Data = cupon,
                Message = "Cupón válido."
            }
        }
    }
}
