using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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
            return Ok(_reservation.RegisterReserve(new Reserve
            {
                Book = reserve.Book,
                StartDate = reserve.StartDate,
                EndDate = reserve.EndDate,
                Username = User.Identity.Name,
            }));
        }

        [HttpGet, AllowAnonymous, Route("current_user")]
        public IActionResult GetReserverOfCurrentUser()
        {
            return Ok(_reservation.GetReserves(User.Identity.Name));
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
            if (_reservation.CancelReserves(id))
                return Ok();
            return BadRequest();
        }

        [HttpPut, AllowAnonymous, Route("{id}")]
        public IActionResult PutReserves(ReserveDTO reserve, Guid id)
        {
            return Ok(_reservation.Update(new Reserve
            {
                Book = reserve.Book,
                StartDate = reserve.StartDate,
                EndDate = reserve.EndDate,
            }, id));
        }

        /*[HttpPost, AllowAnonymous, Route("finalize/{id}")]
        public IActionResult Finalize(Guid id)
        {

        }*/
    }
}
