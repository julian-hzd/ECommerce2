using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ECommerce.API.Orders.Db;
using ECommerce.API.Orders.Interfaces;
using ECommerce.API.Orders.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


/// <summary>
/// Julian Hernandez Delgado
/// </summary>
namespace ECommerce.API.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;

        // dependency injection example through constructor
        // do not manage the instance yourself
        // getting access to the services through the context
        // injecting them in the class so you can use them

        public OrdersProvider(OrdersDbContext dbContext, ILogger<OrdersProvider> log, IMapper map)
        {
            this.dbContext = dbContext;
            this.logger = log;
            this.mapper = map;

            SeedData();
        }

        public async Task<(bool isSuccess, IEnumerable<Model.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var order = await dbContext.Orders.Where(o => o.CustomerId == customerId).ToListAsync();
                if (order != null)
                {
                    var result = mapper.Map<IEnumerable<Model.Order>>(order);
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

        private void SeedData()
        {
            Db.OrderItem x = new Db.OrderItem {Id=1, OrderId=1, ProductId=1, Quantity=10, UnitPrice=10 };
            List<Db.OrderItem> l = new List<Db.OrderItem> { x };
            if (!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order() { Id = 4, CustomerId=1, Items= l, OrderDate= DateTime.Now, Total=180});
                dbContext.Orders.Add(new Db.Order() { Id = 5, CustomerId=2, Items= l , OrderDate= DateTime.Now, Total=300});
                dbContext.Orders.Add(new Db.Order() { Id = 6, CustomerId=3, Items= l , OrderDate= DateTime.Now, Total=450});
                dbContext.SaveChanges();
            }
        }
    }
}
