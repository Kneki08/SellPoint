using Microsoft.EntityFrameworkCore;
using SellPoint.Domain.Entities.Orders;
using SellPoint.Domainn.Entities.Orders;
using SellPoint.Domainn.Entities.Products;
using SellPoint.Domainn.Entities.Users;
using SellPoint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SellPoint.Persistence.Context
{
        public class SellPointDbContext : DbContext
       {
        public SellPointDbContext(DbContextOptions<SellPointDbContext> options) : base(options)
        {

        }

        public DbSet<Categoria> Categoria { get; set; }
            public DbSet<Carrito> Carrito { get; set; }
            public DbSet<Cupon> Cupon { get; set; }
            public DbSet<Producto> Producto { get; set; }
            public DbSet<DetallePedido> DetallePedido { get; set; }
            public DbSet<Pedido> Pedido { get; set; }
             public DbSet< Cliente> Clientes { get; set; }

    }

}
