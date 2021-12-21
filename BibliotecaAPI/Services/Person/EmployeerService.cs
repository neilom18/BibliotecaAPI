using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public class EmployeerService
    {
        private UsersRepository _usersRepository;
        private EmployeerRepository _employeerRepository;
        private AddressService _addressService;

        public EmployeerService
            (
            UsersRepository usersRepository,
            EmployeerRepository employeerRepository,
            AddressService addressService
            )
        {
            _usersRepository = usersRepository;
            _employeerRepository = employeerRepository;
            _addressService = addressService;
        }

        public async Task<UserCreateResult> CreateAsync(NewUserDTO data)
        {
            var res = await _addressService.GetAddressAsync(data.CEP, 5); // Tenta pegar o Endereço pelo CEP
            if (res is null || res.CEP is null)
            {
                if (data.Address is null)
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
                res = await _addressService.GetAddressAsync(data.Address.CEP, 5);
                if (res is null || res.CEP is null)
                    return UserCreateResult.ErrorResult(UserCreateResult.UserAddressExcpetion.USER_ADDRESS_EXCEPTION);
            }

            var employeer = new Employeer
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
                );

            employeer.Address.Update(
                data.Address.Logradouro,
                data.Address.Complemento,
                data.Address.Bairro,
                data.Address.Localidade,
                data.Address.Uf,
                data.Address.Numero);

            var userExist = _usersRepository.GetbyUsername(employeer.User.Username);


            if (userExist != null)
                return UserCreateResult.ErrorResult(UserCreateResult.UsernameUsedExcpetion.USERNAME_USED_EXCEPTION);

            employeer.SetRole();
            var newUser = _usersRepository.Create(employeer.User); // Salva o Usuario

            _employeerRepository.Create(employeer); // Salva o Funcionario

            return UserCreateResult.SucessResult(newUser);
        }
    }
}
