using ERestaurant.Domain.DomainModels;
using ERestaurant.Service.Interface;
using ERestaurant.Repository;

namespace ERestaurant.Service.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer> _customerRepository;

        public CustomerService(IRepository<Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAll(x => x).ToList();
        }

        public Customer GetCustomer(Guid id)
        {
            return _customerRepository.Get(x => x, predicate: c => c.Id == id);
        }

        public Customer CreateCustomer(Customer customer)
        {
            return _customerRepository.Insert(customer);
        }

        public Customer UpdateCustomer(Customer customer)
        {
            return _customerRepository.Update(customer);
        }

        public void DeleteCustomer(Guid id)
        {
            var customer = GetCustomer(id);
            if (customer != null)
                _customerRepository.Delete(customer);
        }
    }
}
