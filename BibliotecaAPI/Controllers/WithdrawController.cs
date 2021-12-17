using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace BibliotecaAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class WithdrawController : ControllerBase
    {
        private readonly WithdrawService _withdrawService;
        private readonly BookService _bookService;

        public WithdrawController(WithdrawService withdrawService,BookService bookService)
        {
            _withdrawService = withdrawService;
            _bookService = bookService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult PostWithdraw(WithdrawDTO withdrawDTO)
        {
            withdrawDTO.Validar();
            if (!withdrawDTO.Valido) return BadRequest();
            Withdraw with;
            if (withdrawDTO.ReserveId != null)
            {
                // Pega um Withdraw em aberto da reserva correspondente
                with = _withdrawService.GetReserve(withdrawDTO.ReserveId.Value);
            }
            else
            {
                _bookService.GetBooks(withdrawDTO.Book);
                with = new Withdraw
                    (
                        book: _bookService.GetBooks(withdrawDTO.Book).ToList(),
                        startDate: withdrawDTO.StartDate,
                        endDate: withdrawDTO.EndDate,
                        customerId: Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value)
                    );
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
