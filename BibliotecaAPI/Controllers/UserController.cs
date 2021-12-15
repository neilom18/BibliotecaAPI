using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BibliotecaAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly EmployeerService _employeerService;
        private readonly UserService _userService;
        private readonly CustomerService _customerService;

        public UserController(UserService userService, CustomerService customerService, EmployeerService employeerService)
        {
            _userService = userService;
            _customerService = customerService;
            _employeerService = employeerService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Register([FromBody] NewUserDTO newUserDTO)
        {
            newUserDTO.Validar();
            if (!newUserDTO.Valido) return BadRequest();
            var result = _customerService.CreateAsync(new Customer
            {
                User = new User
                {
                    Username = newUserDTO.Username,
                    Password = newUserDTO.Password,
                    Age = newUserDTO.Age,
                    CPF = newUserDTO.CPF,
                },
                CPF = newUserDTO.CPF,
                CEP = newUserDTO.CEP,
                Address = newUserDTO.Address
            }).Result;

            if (result.Errors)
            {
                return BadRequest(new UserCraeteResultDTO
                {
                    Sucess = false,
                    Errors = new string[] { result.CreateException.Message },
                    User = null,
                });
            }
            return Ok(new UserCraeteResultDTO
            {
                Sucess = true,
                User = new UserDTO { Username = result.User.Username, Role = result.User.Role },
                Errors = null
            });
        }

        [HttpPost, Authorize(Roles = "admin"), Route("employeer")]
        public IActionResult EmployeerRegister([FromBody] NewUserDTO newUserDTO)
        {
            var result = _employeerService.CreateAsync(new Employeer
            {
                User = new User
                {
                    Username = newUserDTO.Username,
                    Password = newUserDTO.Password,
                    Age = newUserDTO.Age,
                    CPF = newUserDTO.CPF,
                },
                CPF = newUserDTO.CPF,
                CEP = newUserDTO.CEP,
                Address = newUserDTO.Address
            }).Result;

            if (result.Errors)
            {
                return BadRequest(new UserCraeteResultDTO
                {
                    Sucess = false,
                    Errors = new string[] { result.CreateException.Message },
                    User = null,
                });
            }

            return Ok(new UserCraeteResultDTO
            {
                Sucess = true,
                User = new UserDTO { Username = result.User.Username, Role = result.User.Role },
                Errors = null
            });
        }

        [HttpPost, AllowAnonymous, Route("login")]
        public IActionResult Login(UserLoginDTO userLogin)
        {
            userLogin.Validar();
            if (!userLogin.Valido) return BadRequest();
            var result = _userService.Login(userLogin.Username, userLogin.Password);
            if (result.Sucess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet, Authorize(Roles = "customer,admin,employee"),Route("current_user")]
        public IActionResult GetCurrentUser()
        {
            return Ok(_userService.GetCurrentUser(User.Identity.Name));
        }

        [HttpGet, Authorize(Roles = "admin,employeer")]
        public IActionResult Get(UserQuery parameters)
        {
            return Ok(_userService.Get(parameters));
        }

        [HttpGet, Authorize(Roles ="admin,employeer"), Route("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok(_userService.GetById(id));
        }      

        [HttpPut, AllowAnonymous, Route("reset_password")]
        public IActionResult ResetPassword(ResetPasswordDTO resetPassword)
        {
            resetPassword.Validar();
            if (!resetPassword.Valido) return BadRequest();
            var result = _userService.ResetPassword(resetPassword);
            if(result.Sucess == true)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
