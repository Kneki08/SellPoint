using SellPoint.View.Models.ModelsCategoria;

namespace SellPoint.View.Mappers.Categoria
{
    public interface ICategoriaMapper
    {
        UpdateCategoriaModel ConvertToUpdate(CategoriaModel model);
        SaveCategoriaModel ConvertToSave(CategoriaModel model);
    }

    public class CategoriaMapper : ICategoriaMapper
    {
        public UpdateCategoriaModel ConvertToUpdate(CategoriaModel model)
        {
            return new UpdateCategoriaModel
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo,
                EstaEliminado = model.EstaEliminado,
                FechaActualizacion = DateTime.Now 
            };
        }

        public SaveCategoriaModel ConvertToSave(CategoriaModel model)
        {
            return new SaveCategoriaModel
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
            };
        }
    }
}
