using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class CustomersRepository
    {
        private readonly Dictionary<Guid, Customer> _customer;

        public CustomersRepository()
        {
            _customer = new Dictionary<Guid, Customer>();
        }

        public void Create(Customer customer)
        {
            customer.SetCreatedDate(DateTime.Now);
            if (!_customer.TryAdd(customer.User.Id, customer))
                throw new Exception();
        }

        public void Update(Customer customer, Guid id)
        {
            if(_customer.TryGetValue(id, out Customer customer2))
            {
                customer2.Update(customer);
            }
            else { throw new Exception("Cliente não encontrado "); }
        }
    }
}
