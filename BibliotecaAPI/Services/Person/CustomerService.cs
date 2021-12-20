using BibliotecaAPI.DTOs;
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

        public async Task<UserCreateResult> CreateAsync(NewUserDTO data)
        {
           
            var res = await _addressService.GetAddressAsync(data.CEP, 5); // Tenta pegar o Endereço pelo CEP
            if (res is null || res.CEP is null)
            {
                if (data.Address is null)
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
                res = await _addressService.GetAddressAsync(data.Address.Cep, 5);
                if (res is null || res.CEP is null) 
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
            }

            var customer = new Customer
                (
                user: new User
                    (
                        username: data.Username,
                        password: data.Password,
                        document: data.Document,
                        age: data.Age
                    ),
                address: new Address
                    (
                        cep: res.CEP,
                        bairro: res.Bairro,
                        logradouro: res.Logradouro,
                        uf: res.Uf,
                        localidade: res.Localidade,
                        complemento: res.Complemento
                    ),
                document: data.Document
                ) ;

            customer.Address.Update(
                data.Address.Logradouro,
                data.Address.Complemento,
                data.Address.Bairro,
                data.Address.Localidade,
                data.Address.Uf);

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
