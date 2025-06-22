using SellPoint.Aplication.Interfaces.Repositorios;
using SellPoint.Domain.Base;
using SellPoint.Domainn.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Persistence.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        public Task<OperationResult> ActualizarAsync(Categoria entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> AgregarAsync(Categoria entidad)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> EliminarAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerConProductosAsync(int categoriaId)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerPorNombreAsync(string nombre)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> ObtenerTodosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
