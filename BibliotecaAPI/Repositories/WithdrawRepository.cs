using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Enums;
using BibliotecaAPI.ExtensionsMethod;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class WithdrawRepository
    {
        private readonly Dictionary<Guid, Withdraw> _withdraw;

        public WithdrawRepository()
        {
            _withdraw = new Dictionary<Guid, Withdraw>();
        }

        public Withdraw Register(Withdraw withdraw)
        {
            if(_withdraw.TryAdd(withdraw.Id, withdraw))
            {
                withdraw.SetStatus(EStatus.Ongoing);
                return withdraw;
            }
            throw new Exception();
        }

        public void OpenWithdraw(Reserve reserve)
        {
            var with = new Withdraw
                (
                    book: reserve.Book,
                    startDate: reserve.StartDate,
                    endDate: reserve.EndDate,
                    reservedId: reserve.Id,
                    customerId: reserve.CustomerId
                );
            with.SetStatus(EStatus.Started);

            if (!_withdraw.TryAdd(with.Id, with))
                throw new Exception();
        }

        public int NumberOfWithdrawInDate(DateTime dateStart, DateTime dateEnd, Guid bookId)
        {
            var books = _withdraw.Values.Where(y => y.Status == EStatus.Ongoing && y.Book.Any(b => b.Id == bookId));
           
            books = books.Where(x => dateStart.Date <= x.StartDate.Date && dateEnd.Date >= x.StartDate.Date ||
                                     dateStart.Date <= x.EndDate.Date && dateEnd.Date >= x.EndDate.Date);
            return books.Count();
        }

        public IEnumerable<Withdraw> Get(WithdrawQuery parameters)
        {
            IEnumerable<Withdraw> withdrawFiltered = _withdraw.Values;

            withdrawFiltered = withdrawFiltered.WhereIf(parameters.Status, x => x.Status == parameters.Status)
                .WhereIf(parameters.StartDate, x => x.StartDate == parameters.StartDate)
                .WhereIf(parameters.EndDate, x => x.EndDate == parameters.EndDate)
                .WhereIf(parameters.AuthorId, x => x.Book.Any(b => b.AuthorId == parameters.AuthorId))
                .WhereIf(parameters.BookName, x => x.Book.Any(b => b.Title == parameters.BookName));

            return withdrawFiltered.Paginaze(parameters.Page, parameters.Size);
        }


        public Withdraw GetStartedByReserveId(Guid id)
        {
            return _withdraw.Values.Where(w => w.Status == EStatus.Started && w.ReservedId == id).FirstOrDefault();
        }

        public Withdraw GetOngoingById(Guid id)
        {
            return _withdraw.Values.Where(w => w.Status == EStatus.Ongoing && w.Id == id).FirstOrDefault();
        }
        public IEnumerable<Withdraw> GetOngoingByCustomer(Guid id)
        {
            return _withdraw.Values.Where(w => w.Status == EStatus.Ongoing && w.CustomerId == id);
        }

        public Withdraw GetOngoingByReserveId(Guid id)
        {
            return _withdraw.Values.Where(w => w.Status == EStatus.Ongoing && w.ReservedId == id).FirstOrDefault();
        }

        public Withdraw Finalize(Guid id)
        {
            if(_withdraw.TryGetValue(id, out var withdraw))
            {
                withdraw.SetStatus(EStatus.Finalized);
                return withdraw;
            }

            throw new Exception();
        }

        public Withdraw Cancel(Guid id)
        {
            if (_withdraw.TryGetValue(id, out var withdraw))
            {
                withdraw.SetStatus(EStatus.Canceled);
                return withdraw;
            }

            throw new Exception();
        }

        public void Update(Reserve reserve, Guid id)
        {
            var w = GetStartedByReserveId(id);
            if (_withdraw.TryGetValue(w.Id, out var withdraw))
                withdraw.UpdateDate(reserve.StartDate, reserve.EndDate);
            else
            {
                throw new Exception("Não foi possível encontrar a reserva");
            }
        }
    }
}
