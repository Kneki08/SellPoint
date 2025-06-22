

namespace SellPoint.Aplication.Dtos.ProductoDTO
{
    public record RemoveProductoDTO
    {
        public int Id { get; set; }
        public bool EsEliminado { get; set; }
    }
}
