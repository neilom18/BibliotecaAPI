using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(_withdrawService.RegisterWithdraw(new Withdraw
            {
                Reserve = new Reserve
                {
                    Book = withdrawDTO.Reserve.Book,
                    EndDate = withdrawDTO.Reserve.EndDate,
                    StartDate = withdrawDTO.Reserve.StartDate,
                    Username = User.Identity.Name,
                },
            }));
        }
    }
}
