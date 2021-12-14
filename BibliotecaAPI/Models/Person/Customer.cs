using System;

namespace BibliotecaAPI.Models
{
    public class Customer
    {
        public string CPF { get; set; }
        public string CEP { get; set; }
        public Address? Address { get; set; }
        public DateTime CreatedDate { get; set; }
        public User User { get; set; }
    }
}
