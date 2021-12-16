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

        public WithdrawService(WithdrawRepository repository)
        {
            _repository = repository;
        }

        public Withdraw RegisterWithdraw(Withdraw withdraw)
        {
            
            return _repository.Register(withdraw);
        }

        public IEnumerable<Withdraw> GetWithdraw(WithdrawQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public Withdraw GetWithdrawById(Guid id)
        {
            return _repository.GetStarted(id);
        }

        public Withdraw FinalizeWithdraw(Guid id)
        {
            // Validação a fazer
            return _repository.Finalize(id);
        }
    }
}
