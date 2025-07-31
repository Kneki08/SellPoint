using SellPoint.Aplication.Dtos.Categoria;
using SellPoint.View.Models.ModelsCategoria;

namespace SellPoint.View.Services.CategoriaApiClient
{
    public interface ICategoriaMapper
    {
        CategoriaDTO Convert(CategoriaModel model);
        IEnumerable<CategoriaDTO> Convert(IEnumerable<CategoriaModel> models);
    }

    public class CategoriaMapper : ICategoriaMapper
    {
        public CategoriaDTO Convert(CategoriaModel model)
        {
            return new CategoriaDTO
            {
                Id = model.Id,
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Activo = model.Activo
            };
        }

        public IEnumerable<CategoriaDTO> Convert(IEnumerable<CategoriaModel> models)
        {
            return models.Select(Convert).ToList();
        }
    }
}
