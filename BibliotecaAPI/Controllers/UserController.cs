using BibliotecaAPI.DTOs;
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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get([FromQuery] int page, [FromQuery] int size)
        {
            return Ok(_userService.Get(page, size));
        }
        [HttpPost, AllowAnonymous]
        public IActionResult Cadastro([FromBody] NewUserDTO newUserDTO)
        {
            try
            {
                return Ok(_userService.CreateAsync(new Client
                {
                    User = new User
                    {
                        Username = newUserDTO.Username,
                        Password = newUserDTO.Password,
                    },
                    CPF = newUserDTO.CPF,
                    CEP = newUserDTO.CEP,
                    Address = newUserDTO.Address
                }).Result);
            }
            catch(Exception ex)
            {
                return BadRequest(new Result { Status = "Failed", Error = ex.Message});
            }
        }
    }
}
