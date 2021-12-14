using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;

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
    }
}
