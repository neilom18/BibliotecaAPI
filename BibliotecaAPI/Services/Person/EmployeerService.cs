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

        public async Task<UserCreateResult> CreateAsync(Employeer employeer)
        {
            var res = await _addressService.GetAddressAsync(employeer.CEP, 5); // Tenta pegar o Endereço pelo CEP
            if (res is null || res.Cep is null)
            {
                if (employeer.Address is null)
                    return UserCreateResult.ErrorResult(UserCreateResult.UserCreateException.USER_CREATE_EXCEPTION);
            }
            else { employeer.Address = res; }

            var userExist = _usersRepository.GetbyUsername(employeer.User.Username);


            if (userExist != null)
                return UserCreateResult.ErrorResult(UserCreateResult.UsernameUsedExcpetion.USERNAME_USED_EXCEPTION);

            employeer.User.Role = "employeer";
            var newUser = _usersRepository.Create(employeer.User); // Salva o Usuario

            _employeerRepository.Create(employeer); // Salva o Funcionario

            return UserCreateResult.SucessResult(newUser);
        }
    }
}
