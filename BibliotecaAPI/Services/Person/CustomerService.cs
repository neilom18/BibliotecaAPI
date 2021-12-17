using BibliotecaAPI.Manager;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public class CustomerService
    {
        private UsersRepository _usersRepository;
        private CustomersRepository _clientsRepository;
        private AddressService _addressService;

        public CustomerService
            (
            UsersRepository usersRepository,
            CustomersRepository clientsRepository,
            AddressService addressService
            )
        {
            _usersRepository = usersRepository;
            _clientsRepository = clientsRepository;
            _addressService = addressService;
        }

        public async Task<UserCreateResult> CreateAsync(Customer customer)
        {
            var res = await _addressService.GetAddressAsync(customer.CEP, 5); // Tenta pegar o Endereço pelo CEP
            if (res is null || res.Cep is null)
            {
                if (customer.Address is null)
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
                res = await _addressService.GetAddressAsync(customer.Address.Cep, 5);
                if (res is null || res.Cep is null) 
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
            }
            else { customer.SetAddress(res) ; }

            var userExist = _usersRepository.GetbyUsername(customer.User.Username);


            if (userExist != null)
                return UserCreateResult.ErrorResult(UserCreateResult.UsernameUsedExcpetion.USERNAME_USED_EXCEPTION);

            customer.SetRole();
            var newUser = _usersRepository.Create(customer.User); // Salva o Usuario

            _clientsRepository.Create(customer); // Salva o Cliente

            return UserCreateResult.SucessResult(newUser);
        }

    }
}
