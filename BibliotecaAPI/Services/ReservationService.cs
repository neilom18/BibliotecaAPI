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

        public ReservationService(ReservationRepository repository)
        {
            _repository = repository;
        }

        public Reserve RegisterReserve(Reserve reserve)
        {
            return _repository.Register(reserve);
        }

        public IEnumerable<Reserve> GetReserves(Guid id) 
        {
            return _repository.GetById(id);
        }

        public IEnumerable<Reserve> GetReserves(ReserveQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public bool CancelReserves(Guid id)
        {
            return _repository.Cancel(id);
        }

        public Reserve Update(Reserve reserve, Guid id)
        {
            return _repository.Update(reserve, id);
        }

        public Reserve Finalize(Guid id)
        {
            return _repository.Finalize(id);
        }
    }
}
