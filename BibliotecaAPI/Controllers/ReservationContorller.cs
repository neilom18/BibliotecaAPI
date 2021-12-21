using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BibliotecaAPI.Controllers
{
    [ApiController, Route("[controller]")]
    public class ReservationContorller : ControllerBase
    {
        private readonly ReservationService _reservation;
        public ReservationContorller(ReservationService reservation)
        {
            _reservation = reservation;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult PostReserve(ReserveDTO reserve) 
        {
            reserve.Validar();
            if (!reserve.Valido) return BadRequest(reserve.GetErrors());
            try
            {
                return Ok(_reservation.RegisterReserve(new Reserve
                        (
                            startDate: reserve.StartDate.Date,
                            endDate: reserve.EndDate.Date,
                            customerId: Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value)
                        ), reserve.BookId));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
        }

        [HttpGet, AllowAnonymous, Route("current_user")]
        public IActionResult GetReserverOfCurrentUser()
        {
            var id = Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value);
            try
            {
                return Ok(_reservation.GetReserves(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
            // User.FindFirst(ClaimTypes.Sid) Achar o Id
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get([FromQuery]ReserveQuery parameters)
        {
            return Ok(_reservation.GetReserves(parameters));
        }

        [HttpPost, AllowAnonymous, Route("cancel/{id}")]
        public IActionResult CancelReserve(Guid id)
        {
            try
            {
                _reservation.CancelReserves(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message }) ;
            }
           
        }

        [HttpPut, AllowAnonymous, Route("{id}")]
        public IActionResult PutReserves(ReserveDTO reserve, Guid id)
        {
            reserve.Validar();
            if (!reserve.Valido) return BadRequest(reserve.GetErrors());
            try
            {
                return Ok(_reservation.Update(new Reserve
                        (
                            reserve.StartDate,
                            reserve.EndDate
                        ), reserve.BookId, id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message }) ;
            }
        }

        [HttpPost, AllowAnonymous, Route("finalize/{id}")]
        public IActionResult Finalize(Guid id)
        {
            return Ok(_reservation.Finalize(id));
        }
    }
}
