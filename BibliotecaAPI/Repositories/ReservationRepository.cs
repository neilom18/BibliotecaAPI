using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Enums;
using BibliotecaAPI.ExtensionsMethod;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class ReservationRepository
    {
        private readonly Dictionary<Guid, Reserve> _reservation;

        public ReservationRepository()
        {
            _reservation = new Dictionary<Guid, Reserve>();
        }

        public Reserve Register(Reserve reservation)
        {
            reservation.Id = Guid.NewGuid();
            if(_reservation.TryAdd(reservation.Id, reservation))
                return reservation;

            throw new Exception();
        }

        public Reserve Get(Guid id)
        {
            if(_reservation.TryGetValue(id, out var reservation))
                return reservation;

            return null;
        }

        public IEnumerable<Reserve> GetById(Guid id)
        {
            return _reservation.Values.Where(x => x.CustomerId == id);
        }

        public IEnumerable<Reserve> Get(ReserveQuery parameters)
        {
            IEnumerable<Reserve> reserveFiltered = _reservation.Values;

            /*if (parameters.BookName != null)
                reserveFiltered = reserveFiltered.Where(x => x.Book.Any(b => b.Title == parameters.BookName));
            if(parameters.StartDate != null)
                reserveFiltered = reserveFiltered.Where(x => x.StartDate == parameters.StartDate);
            if(parameters.EndDate != null)
                reserveFiltered = reserveFiltered.Where(x => x.EndDate == parameters.EndDate);
            if(parameters.AuthorId != null)
                reserveFiltered = reserveFiltered.Where(x => x.Book.Any(b => b.AuthorId == parameters.AuthorId));*/

            reserveFiltered = reserveFiltered.WhereIf(parameters.BookName, x => x.Book.Any(b => b.Title == parameters.BookName))
                .WhereIf(parameters.StartDate, x => x.StartDate == parameters.StartDate)
                .WhereIf(parameters.EndDate, x => x.EndDate == parameters.EndDate)
                .WhereIf(parameters.AuthorId, x => x.Book.Any(b => b.AuthorId == parameters.AuthorId));


            return reserveFiltered.Paginaze(parameters.Page, parameters.Size);
        }

        public bool Cancel(Guid id)
        {
            if (_reservation.TryGetValue(id, out var reserve)) 
            {
                reserve.Status = EStatus.Canceled;
                return true;
            }
            return false;
        }

        public Reserve Update(Reserve reserve, Guid id)
        {
            if (_reservation.TryGetValue(id, out var reserveToUpdate))
            {
                reserveToUpdate.StartDate = reserve.StartDate;
                reserveToUpdate.EndDate = reserve.EndDate;
                reserveToUpdate.Book = reserve.Book;

                return Get(id);
            }
            throw new Exception();
        }

        public Reserve Finalize(Guid id) 
        {
            if(_reservation.TryGetValue(id, out var reserve))
            {
                reserve.Status = EStatus.Finalized;
                return reserve;
            }
            throw new Exception();
        }

        public int NumberOfReserversInDate(DateTime dateStart,DateTime dateEnd, Guid bookId)
        {
            var books = _reservation.Values.Where(y => y.Status == EStatus.Started && y.Book.Any(b => b.Id == bookId));
            /*books = books.Where(x => dateStart.Date >= x.StartDate.Date && dateEnd.Date <= x.EndDate.Date ||
                                dateStart.Date >= x.StartDate.Date && dateEnd.Date >= x.EndDate.Date && dateStart.Date <= x.EndDate.Date ||
                                dateStart.Date <= x.StartDate.Date && dateEnd.Date <= x.EndDate.Date && dateEnd.Date >= x.StartDate.Date
                                );*/
            books = books.Where(x => dateStart.Date <= x.StartDate.Date && dateEnd.Date >= x.StartDate.Date ||
                                     dateStart.Date <= x.EndDate.Date && dateEnd.Date >= x.EndDate.Date);
            return books.Count();
        }
    }
}
