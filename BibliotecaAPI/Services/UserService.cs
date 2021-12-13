using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Login;
using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.Manager;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections;
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

        public IEnumerable<UserDataDTO> Get(string cpf,int age,string name,int page, int size)
        {
            var user = _usersRepository.Get(cpf,age,name,page, size);

            return user.Select(u =>
            {
                return new UserDataDTO
                {
                    Username = u.Username,
                    Role = u.Role,
                    Age = u.Age,
                    CPF = u.CPF,
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
                CPF = user.CPF,
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
                CPF = user.CPF,
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
                    Errors = new string[] { $"Ocorreu um erro ao authenticar {loginResult.Exception.Message}" }
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
                    Id = loginResult.User.Id,
                    Token = token,
                    Role = loginResult.User.Role,
                }
            };
        }

        public ResetPasswordResultDTO ResetPassword(ResetPasswordDTO resetPassword)
        {
            var result = _loginManager.Authentication(resetPassword.Username, resetPassword.OldPassword);
            if (result.Error)
            {
                return new ResetPasswordResultDTO
                {
                    Sucess = false,
                    Errors = new string[] { $" Ocorreu um erro ao trocar a senha: {result.Exception.Message} "}
                };
            }

            _usersRepository.ChangePassword(result.User.Id, resetPassword.NewPassword);

            return new ResetPasswordResultDTO
            {
                Sucess = true,
                Errors=null,
            };
        }
    }
}
