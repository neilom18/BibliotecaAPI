using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Services
{
    public class ReservationService
    {
        private readonly ReservationRepository _repository;
        private readonly BookRepository _bookRepository;
        private readonly WithdrawRepository _withdrawRepository;

        public ReservationService(ReservationRepository repository, BookRepository bookRepository, WithdrawRepository withdraw)
        {
            _repository = repository;
            _bookRepository = bookRepository;
            _withdrawRepository = withdraw;
        }

        public Reserve RegisterReserve(Reserve reserve, List<Guid> ids)
        {
            // Pega os livros do repositorio pela lista de ids
            List<Book> books = new List<Book>();
            foreach(var id in ids)
            {
                books.Add(_bookRepository.Get(id));
            }
            reserve.SetBook(books);
                                              
            var available = Available(reserve);
            if (available.Available)
            {
                _withdrawRepository.OpenWithdraw(reserve);
                return _repository.Register(reserve);
            }
            throw new Exception($"O livro {available.Title} não está disponível para reserva nessa data");
        }

        public Availability Available(Reserve reserve)
        {
            // Verifica se reservou por no minimo 5 dias
            if ((reserve.EndDate.Date - reserve.StartDate.Date).TotalDays < 5)
                throw new Exception("O tempo mínimo de reserva é de 5 dias");

            foreach (Book book in reserve.Book)
            {
                var n1 = _repository.NumberOfReserversInDate(reserve.StartDate,reserve.EndDate, book.Id);
                var n2 = _withdrawRepository.NumberOfWithdrawInDate(reserve.StartDate,reserve.EndDate,book.Id);
                if(n1 + n2 >= book.AmountCopies)
                {
                    return new Availability { Available = false, Title = book.Title};
                }
            }
            return new Availability { Available = true};
        }

        public IEnumerable<Reserve> GetReserves(Guid id) 
        {
            return _repository.GetByCustomerId(id);
        }

        public IEnumerable<Reserve> GetReserves(ReserveQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public void CancelReserves(Guid id)
        {
            var r =_repository.Get(id);
            var d = r.StartDate.Date;
            if (d.DayOfWeek.HasFlag(DayOfWeek.Monday)) d = d.AddDays(-3); 
            int i = DateTime.Compare(d, DateTime.Now);
            if (i == 0 || i == 1)
                _repository.Cancel(id);
            else
                throw new Exception("Somente é possivel cancelar até 1 dia util antes");
        }

        public Reserve Update(Reserve reserve, List<Guid> bookids)
        {
            // Pega os livros do repositorio pela lista de ids
            List<Book> books = new List<Book>();
            foreach (var bookid in bookids)
            {
                books.Add(_bookRepository.Get(bookid));
            }
            reserve.SetBook(books);

            return _repository.Update(reserve);
        }

        public Reserve Finalize(Guid id)
        {
            return _repository.Finalize(id);
        }
    }
}
