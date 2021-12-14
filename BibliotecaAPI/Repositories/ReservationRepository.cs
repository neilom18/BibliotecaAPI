using BibliotecaAPI.DTOs.Query;
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

        public IEnumerable<Reserve> GetByUsername(string username)
        {
            return _reservation.Values.Where(x => x.Username == username);
        }

        public IEnumerable<Reserve> Get(ReserveQuery parameters)
        {
            IEnumerable<Reserve> reserveFiltered = _reservation.Values;

            if (parameters.BookName != null)
                reserveFiltered = reserveFiltered.Where(x => x.Book.Any(b => b.Title == parameters.BookName));
            if(parameters.StartDate != null)
                reserveFiltered = reserveFiltered.Where(x => x.StartDate == parameters.StartDate);
            if(parameters.EndDate != null)
                reserveFiltered = reserveFiltered.Where(x => x.EndDate == parameters.EndDate);
            if(parameters.AuthorId != null)
                reserveFiltered = reserveFiltered.Where(x => x.Book.Any(b => b.AuthorId == parameters.AuthorId));

            return reserveFiltered.Skip(parameters.Page == 1 ? 0 : (parameters.Page - 1) * parameters.Size)
                .Take(parameters.Size).ToList();
        }

        public bool Cancel(Guid id)
        {
            if (_reservation.TryGetValue(id, out var reserve)) 
            {
                reserve.Status = Enums.ReserveStatus.Canceled;
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

        public void Finalize(Guid id) 
        {
            var r = Get(id);
            r.Status = Enums.ReserveStatus.Finalized;
        }
    }
}
