using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Repositories
{
    public class WithdrawRepository
    {
        private readonly Dictionary<Guid, Withdraw> _withdraw;

        public WithdrawRepository(Dictionary<Guid, Withdraw> withdraw)
        {
            _withdraw = withdraw;
        }

        public Withdraw Register(Withdraw withdraw)
        {
            withdraw.Id = Guid.NewGuid();
            if(_withdraw.TryAdd(withdraw.Id, withdraw))
                return withdraw;
            throw new Exception();
        }
    }
}
