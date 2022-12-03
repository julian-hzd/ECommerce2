using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Interfaces;
using ECommerce.API.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDBContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;

        // dependency injection example through constructor
        // do not manage the instance yourself
        // getting access to the services through the context
        // injecting them in the class so you can use them

        public ProductsProvider(ProductsDBContext dbContext, ILogger<ProductsProvider> log, IMapper map)
        {
            this.dbContext = dbContext;
            this.logger = log;
            this.mapper = map;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new DB.Product() { Id=1, Name = "Keyboard", Inventory = 100, Price =100});
                dbContext.Products.Add(new DB.Product() { Id=2, Name = "Mouse", Inventory = 40, Price =50});
                dbContext.Products.Add(new DB.Product() { Id =3, Name = "Monitor", Inventory = 20, Price =50});
                dbContext.Products.Add(new DB.Product() { Id=4, Name = "HeadSet", Inventory = 30, Price =50});
                dbContext.SaveChanges();
            }
        }


        public async Task<(bool isSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)>GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    //var result =  mapper.Map<IEnumerable<DB.Product>, IEnumerable<Models.Product>>(products);
                    var result =  mapper.Map<IEnumerable<Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool isSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p=>p.Id ==id);
                if (product != null)
                {
                    var result = mapper.Map<DB.Product, Models.Product>(product);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
