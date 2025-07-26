using Microsoft.EntityFrameworkCore;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Domainn.Entities.Products;

namespace SellPoint.Persistence.Conext
{
    public class SellPointContext : DbContext
    {
        public SellPointContext(DbContextOptions<SellPointContext> options): base(options){ }

        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
