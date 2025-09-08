using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERestaurant.Domain.DomainModels;

namespace ERestaurant.Service.Interface
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        Customer GetCustomer(Guid id);
        Customer CreateCustomer(Customer customer);
        Customer UpdateCustomer(Customer customer);
        void DeleteCustomer(Guid id);
    }
}
