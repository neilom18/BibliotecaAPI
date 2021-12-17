using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

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
    }
}
