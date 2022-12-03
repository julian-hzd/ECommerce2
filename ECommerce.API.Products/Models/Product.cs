using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Models
{
    // The one we use for database access to provider
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Inventory { get; set; }
        public int Id { get; set; }
    }
}
