using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellPoint.Aplication.Interfaces.IService
{
    public interface ICategoriaService
    {
        Task<OperationResult> ObtenerTodosAsync();
        Task<OperationResult> ObtenerPorIdAsync(int categoriaId);
        Task<OperationResult> CrearAsync(SaveCategoriaDTO nuevaCategoria);
        Task<OperationResult> ActualizarAsync(UpdateCategoriaDTO categoriaActualizada);
        Task<OperationResult> EliminarAsync(RemoveCategoriaDTO categoriaAEliminar);
    }
}
