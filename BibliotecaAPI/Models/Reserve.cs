using BibliotecaAPI.Enums;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Reserve : Base
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid CustomerId { get; set; }
        public ReserveStatus Status { get; set; }
        public List<Book> Book { get; set; }
    }
}
