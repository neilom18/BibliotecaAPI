using BibliotecaAPI.Enums;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Withdraw : Base
    {
        public Withdraw(DateTime startDate, DateTime endDate, Guid? reservedId, Guid customerId, List<Book> book)
        {
            StartDate = startDate;
            EndDate = endDate;
            ReservedId = reservedId;
            CustomerId = customerId;
            Book = book;
        }

        public Withdraw(DateTime startDate, DateTime endDate, Guid customerId, List<Book> book)
        {
            StartDate = startDate;
            EndDate = endDate;
            Book = book;
            CustomerId = customerId;
        }

        public EStatus Status { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Guid? ReservedId { get; private set; }
        public Guid CustomerId { get; private set; }
        public List<Book> Book { get; private set; }

        public void SetStatus(EStatus status) => Status = status;
    }
}
