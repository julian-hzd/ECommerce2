using AutoMapper;
using ECommerce.API.Products.DB;
using ECommerce.API.Products.Profiles;
using ECommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.Products.Test
{
    public class ProductsServiceTest
    {
        [Fact]
        public async Task GetProductsReturnsAllProducts()
        {

            var options = new DbContextOptionsBuilder<ProductsDBContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
                .Options;


            var dbContext = new ProductsDBContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var prods = await productsProvider.GetProductsAsync();

            Assert.True(prods.isSuccess);
            Assert.True(prods.Products.Any());

            Assert.Null(prods.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsReturnsProductsUsingValidId()
        {

            var options = new DbContextOptionsBuilder<ProductsDBContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingValidId))
                .Options;


            var dbContext = new ProductsDBContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var prod = await productsProvider.GetProductAsync(1);

            Assert.True(prod.isSuccess);
            Assert.NotNull(prod.Product);
            Assert.True(prod.Product.Id == 1);
            Assert.Null(prod.ErrorMessage);
        }


        [Fact]
        public async Task GetProductsReturnsProductsUsingInvalidId()
        {

            var options = new DbContextOptionsBuilder<ProductsDBContext>()
                .UseInMemoryDatabase(nameof(GetProductsReturnsProductsUsingInvalidId))
                .Options;


            var dbContext = new ProductsDBContext(options);

            CreateProducts(dbContext);

            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);


            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var prod = await productsProvider.GetProductAsync(-1);

            Assert.False(prod.isSuccess);
            Assert.Null(prod.Product);
            Assert.NotNull(prod.ErrorMessage);
        }

        private void CreateProducts(ProductsDBContext dbContext)
        {
            for (int i = 20; i < 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
