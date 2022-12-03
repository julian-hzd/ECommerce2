using AutoMapper;
using ECommerce.API.Customers.Db;
using ECommerce.API.Customers.Interfaces;
using ECommerce.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Customers.Providers
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomersDbContext dbContext;
        private readonly ILogger<CustomersProvider> logger;
        private readonly IMapper mapper;

        // dependency injection example through constructor
        // do not manage the instance yourself
        // getting access to the services through the context
        // injecting them in the class so you can use them

        public CustomersProvider(CustomersDbContext dbContext, ILogger<CustomersProvider> log, IMapper map)
        {
            this.dbContext = dbContext;
            this.logger = log;
            this.mapper = map;

            SeedData();
        }

        public async Task<(bool isSuccess, Models.Customer Customer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(p=>p.Id==id);
                if (customer != null)
                {
                    var result = mapper.Map<Models.Customer>(customer);
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

        public async Task<(bool isSuccess, IEnumerable<Models.Customer> Customers, string ErrorMessage)> GetCustomersAsync()
        {
            try
            {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var result = mapper.Map<IEnumerable<Models.Customer>>(customers);
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
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Db.Customer() { Id = 1, Name = "Claudiu", Address= "321 Claude"});
                dbContext.Customers.Add(new Db.Customer() { Id = 2, Name = "Ivan", Address = "00 FakeAddress" });
                dbContext.Customers.Add(new Db.Customer() { Id = 3, Name = "Aref", Address = "25 St-Charles" });
                dbContext.Customers.Add(new Db.Customer() { Id = 4, Name = "Helen", Address = "98 St-Jean" });
                dbContext.SaveChanges();
            }
        }


    }
}
