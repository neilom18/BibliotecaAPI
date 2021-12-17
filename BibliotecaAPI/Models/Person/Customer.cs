﻿using System;

namespace BibliotecaAPI.Models
{
    public class Customer
    {
        public Customer(string document, string cep,User user, Address address)
        {
            Document = document;
            CEP = cep;
            Address = address;
            User = user;
        }

        public string Document { get; private set; }
        public string CEP { get; private set; }
        public Address? Address { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public User User { get; private set; }

        public void SetCreatedDate(DateTime date) => CreatedDate = date;
        public void SetAddress(Address address) => Address = address;
        public void SetRole() => User.SetRole("customer");
    }
}
