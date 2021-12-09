using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BibliotecaAPI.Services
{
    public class UserService
    {
        private UsersRepository _usersRepository;
        private JWTTokenService _tokenService;
        private ClientsRepository _clientsRepository;

        public UserService(UsersRepository repository, JWTTokenService tokenService, ClientsRepository clientsRepository)
        {
            _usersRepository = repository;
            _tokenService = tokenService;
            _clientsRepository = clientsRepository;
        }

        public IEnumerable<UserDTO> Get(int page, int size)
        {
            var user = _usersRepository.Get(page, size);

            return user.Select(u =>
            {
                return new UserDTO
                {
                    Role = u.Role,
                    Username = u.Username,
                };
            });
        }

        public async Task<UserDTO> CreateAsync(Client client)
        {
            var res = await GetAddressAsync("https://viacep.com.br/ws/" + client.CEP + "/json/", 5); // Tenta pegar o Endereço pelo CEP
            if (res is null || res.Cep is null)
                throw new Exception("Não foi possível encontrar esse CEP, passe o endereço no corpo da requisição.");
            client.Address = res;
            var userExist = _usersRepository.GetbyUsername(client.User.Username);


            if (userExist != null)
                throw new Exception();
            
            var newUser = _usersRepository.Create(client.User); // Salva o Usuario

            _clientsRepository.Create(client); // Salva o Cliente

            return new UserDTO
            {
                Username = newUser.Username,
                Role = newUser.Role,
            };
        }
        static async Task<Address> GetAddressAsync(string url, int retryCount)
        {
            var client = new HttpClient();
            var response = string.Empty;
            Address address = null;
            var retry = false;
            var retryIndex = 0;

            do
            {
                Console.WriteLine("Fazendo requisicao {0} de {1}", retryIndex, retryCount);
                var rs = await client.GetAsync(url);
                if (!rs.IsSuccessStatusCode)
                {
                    retry = true;
                    retryIndex++;
                }
                response = await rs.Content.ReadAsStringAsync();
                address = JsonConvert.DeserializeObject<Address>(response);
            } while (retry && retryIndex <= retryCount);
            return address;
        }
    }


    
}
