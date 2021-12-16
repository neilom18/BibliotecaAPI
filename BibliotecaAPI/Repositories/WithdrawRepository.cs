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
            withdraw.Id = Guid.NewGuid();
            if(_withdraw.TryAdd(withdraw.Id, withdraw))
                return withdraw;
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

            withdrawFiltered.WhereIf(parameters.Finalized, x => x.Status == parameters.Finalized)
                .WhereIf(parameters.StartDate, x => x.StartDate == parameters.StartDate)
                .WhereIf(parameters.EndDate, x => x.EndDate == parameters.EndDate)
                .WhereIf(parameters.AuthorId, x => x.Book.Any(b => b.AuthorId == parameters.AuthorId))
                .WhereIf(parameters.BookName, x => x.Book.Any(b => b.Title == parameters.BookName));

            return withdrawFiltered.Paginaze(parameters.Page, parameters.Size);
        }

        public Withdraw GetStarted(Guid id)
        {
            return _withdraw.Values.Where(w => w.Status == EStatus.Started && w.CustomerId == id).FirstOrDefault();
        }

        public Withdraw Finalize(Guid id)
        {
            if(_withdraw.TryGetValue(id, out var withdraw))
            {
                withdraw.Status = EStatus.Finalized;
                return withdraw;
            }

            throw new Exception();
        }
    }
}
