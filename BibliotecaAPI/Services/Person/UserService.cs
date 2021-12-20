using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Manager;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;


namespace BibliotecaAPI.Services
{
    public class UserService
    {
        private UsersRepository _usersRepository;
        private JWTTokenService _tokenService;
        private LoginManager _loginManager;

        public UserService(UsersRepository repository,
            JWTTokenService tokenService,
            LoginManager loginManager)
        {
            _usersRepository = repository;
            _tokenService = tokenService;
            _loginManager = loginManager;
        }

        public IEnumerable<UserDataDTO> Get(UserQuery paramters)
        {
            var user = _usersRepository.Get(paramters);

            return user.Select(u =>
            {
                return new UserDataDTO
                {
                    Username = u.Username,
                    Role = u.Role,
                    Age = u.Age,
                    Document = u.Document,
                };
            });
        }

        public UserDataDTO GetCurrentUser(string name)
        {
            var user = _usersRepository.GetbyUsername(name);
            return new UserDataDTO
            {
                Username = user.Username,
                Role = user.Role,
                Age = user.Age,
                Document = user.Document,
            };
        }
        public UserDataDTO GetById(Guid id)
        {
            var user = _usersRepository.Get(id);
            return new UserDataDTO
            {
                Username = user.Username,
                Role = user.Role,
                Age = user.Age,
                Document = user.Document,
            };
        }

        public LoginResultDTO Login(string username, string password)
        {
            var loginResult = _loginManager.Authentication(username, password);
            if (loginResult.Error)
            {
                return new LoginResultDTO
                {
                    Sucess = false,
                    Errors = new string[] { $"Ocorreu um erro ao authenticar: {loginResult.Exception.Message}" }
                };
            }
            var token = _tokenService.GenerateToken(loginResult.User);

            return new LoginResultDTO
            {
                Sucess = true,
                Errors = null,
                UserLogin = new UserLoginResultDTO
                {
                    Username = loginResult.User.Username,
                    Token = token,
                    Role = loginResult.User.Role,
                }
            };
        }

        public ResultDTO ResetPassword(ResetPasswordDTO resetPassword)
        {
            var result = _loginManager.Authentication(resetPassword.Username, resetPassword.OldPassword);
            if (result.Error)
            {
                return new ResultDTO
                {
                    Sucess = false,
                    Errors = new string[] { $" Ocorreu um erro ao trocar a senha: {result.Exception.Message} "}
                };
            }

            _usersRepository.ChangePassword(result.User.Id, resetPassword.NewPassword);

            return new ResultDTO
            {
                Sucess = true,
                Errors=null,
            };
        }
    }
}
