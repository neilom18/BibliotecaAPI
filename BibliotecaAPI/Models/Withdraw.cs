using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Withdraw : Base
    {
        public bool Finalized { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CustomerId { get; set; }
        public List<Book> Book { get; set; }

    }
}
