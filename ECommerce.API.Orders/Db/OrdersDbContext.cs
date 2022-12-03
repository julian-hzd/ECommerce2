using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Orders.Db
{
    public class OrdersDbContext : DbContext
    {
        public DbSet<Db.Order> Orders { get; set; }
        public DbSet<Db.OrderItem> OrderItems { get; set; }
        public OrdersDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
