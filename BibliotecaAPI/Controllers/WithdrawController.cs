using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BibliotecaAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class WithdrawController : ControllerBase
    {
        private readonly WithdrawService _withdrawService;

        public WithdrawController(WithdrawService withdrawService)
        {
            _withdrawService = withdrawService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult PostWithdraw(WithdrawDTO withdrawDTO)
        {
            Withdraw with = null;
            if (withdrawDTO.ReserveId != null)
            {
                // Criar o Withdraw com os dados da reserva 
            }
            else
            {
                with = new Withdraw
                {
                    Book = withdrawDTO.Book,
                    StartDate = withdrawDTO.StartDate,
                    EndDate = withdrawDTO.EndDate,
                    CustomerId = Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value),
                };
            }

            return Ok(_withdrawService.RegisterWithdraw(with));
        }

        [HttpGet, AllowAnonymous, Route("current_user")]
        public IActionResult Get()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            return Ok(_withdrawService.GetWithdrawById(id));
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get([FromQuery]WithdrawQuery parameters)
        {
            return Ok(_withdrawService.GetWithdraw(parameters));
        }

        [HttpPost, AllowAnonymous, Route("finalize/{id}")]
        public IActionResult Finalize(Guid id)
        {
            return Ok(_withdrawService.FinalizeWithdraw(id));
        }
    }
}
