using ECommerce.API.Search.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.API.Search.Interface
{
    public interface ICustomersService
    {
        Task<(bool isSuccess, Customer Customer, string ErrorMessage)> GetCustomerAsync(int customerId);

    }
}
