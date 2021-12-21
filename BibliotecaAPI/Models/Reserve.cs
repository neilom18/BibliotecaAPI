using BibliotecaAPI.Enums;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Reserve : Base
    {
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public EStatus Status { get; private set; }
        public List<Book> Book { get; private set; }
        public Reserve()
        {
            Book = new List<Book>();
        }

        public Reserve(DateTime startDate, DateTime endDate, Guid customerId)
        {
            StartDate = startDate;
            EndDate = endDate;
            CustomerId = customerId;
            Book = new List<Book>(); 
        }

        public Reserve(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }

        public Reserve(DateTime startDate, DateTime endDate, Guid customerId, EStatus status, List<Book> book)
        {
            StartDate = startDate;
            EndDate = endDate;
            CustomerId = customerId;
            Status = status;
            Book = book;
        }

        public void SetBook(List<Book> books) => Book = books;
        public void SetStartDate(DateTime startDate) => StartDate = startDate;
        public void SetEndDate(DateTime endDate) => EndDate = endDate;
        public void SetStatus(EStatus status) => Status = status;
    }
}
