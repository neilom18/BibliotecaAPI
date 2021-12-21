using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Services
{
    public class WithdrawService
    {
        private readonly WithdrawRepository _repository;
        public readonly ReservationRepository _reserveRepository;

        public WithdrawService(WithdrawRepository repository, ReservationRepository reservation)
        {
            _repository = repository;
            _reserveRepository = reservation;
        }

        public Withdraw RegisterWithdraw(Withdraw withdraw)
        {
            if (withdraw.ReservedId is null)
            {
                var available = VerifyAvailability(withdraw);
                if (available.Available) {
                    var reserve = new Reserve
                    (
                        startDate: withdraw.StartDate,
                        endDate: withdraw.EndDate,
                        customerId: withdraw.CustomerId,
                        status: Enums.EStatus.Ongoing,
                        book: withdraw.Book
                        );
                    _reserveRepository.Register(reserve);
                    withdraw.SetReservedId(reserve.Id);
                    return _repository.Register(withdraw); 
                }
                throw new Exception($"O livro {available.Title} não está disponível");
            }
            withdraw.SetStatus(Enums.EStatus.Ongoing);
            return withdraw;
        }

        public Availability VerifyAvailability(Withdraw withdraw)
        {
            if ((withdraw.EndDate.Date - withdraw.StartDate.Date).TotalDays < 5)
                throw new Exception("O tempo mínimo de reserva é de 5 dias");

            foreach (Book book in withdraw.Book)
            {
                var n1 = _repository.NumberOfWithdrawInDate(withdraw.StartDate, withdraw.EndDate, book.Id);
                var n2 = _reserveRepository.NumberOfReserversInDate(withdraw.StartDate, withdraw.EndDate, book.Id);
                if (n1 + n2 >= book.AmountCopies)
                {
                    return new Availability { Available = false, Title = book.Title };
                }
            }
            return new Availability { Available = true };
        }

        public Withdraw GetReserve(Guid id) 
        {

            return _repository.GetStartedByReserveId(id);
        }

        public IEnumerable<Withdraw> GetWithdraw(WithdrawQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public IEnumerable<Withdraw> GetWithdrawByCustomerId(Guid id)
        {
            return _repository.GetOngoingByCustomer(id);
        }

        public Withdraw FinalizeWithdraw(Guid id)
        {
            var w = _repository.GetOngoingById(id);
            if (w is null)
                throw new Exception("Essa reserva foi cancelada ou não foi iniciada");
            
            _reserveRepository.Finalize(w.ReservedId.Value);
            return _repository.Finalize(w.Id);
        }
    }
}
