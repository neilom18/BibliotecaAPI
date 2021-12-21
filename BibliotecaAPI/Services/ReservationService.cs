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
                var register = _repository.Register(reserve);
                _withdrawRepository.OpenWithdraw(reserve);
                return register;
            }
            throw new Exception($"O livro {available.Title} não está disponível para reserva nessa data");
        }

        public Availability Available(Reserve reserve)
        {
            // Verifica se reservou por no minimo 5 dias
            if ((reserve.EndDate - reserve.StartDate).TotalDays < 5)
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
            {
                var w =_withdrawRepository.GetStartedByReserveId(id);
                _withdrawRepository.Cancel(w.Id);
                _repository.Cancel(id);
            }
            else
                throw new Exception("Somente é possivel cancelar até 1 dia util antes");
        }

        public Reserve Update(Reserve reserve, List<Guid> bookids, Guid id)
        {
            // Verifica se reservou por no minimo 5 dias
            if ((reserve.EndDate - reserve.StartDate).TotalDays < 5)
                throw new Exception("O tempo mínimo de reserva é de 5 dias");

            // Pega os livros do repositorio pela lista de ids
            List<Book> books = new List<Book>();
            foreach (var bookid in bookids)
            {
                books.Add(_bookRepository.Get(bookid));
            }
            reserve.SetBook(books);

            _withdrawRepository.Update(reserve, id);

            return _repository.Update(reserve, id);
        }

        public Reserve Finalize(Guid id)
        {
            var w = _withdrawRepository.GetOngoingByReserveId(id);
            if (w is null)
                throw new Exception("Essa reserva foi cancelada ou não pode ser finalizada no momento");

            _withdrawRepository.Finalize(w.Id);  
            return _repository.Finalize(id);
        }
    }
}
