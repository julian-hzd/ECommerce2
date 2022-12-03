using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.DB
{
    public class ProductsDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ProductsDBContext(DbContextOptions options) : base(options)
        {

        }
    }
}
