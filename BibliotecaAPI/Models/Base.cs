using System;

namespace BibliotecaAPI.Models
{
    public class Base
    {
        public Base()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }
    }
}
