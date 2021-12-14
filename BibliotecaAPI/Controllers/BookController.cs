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
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult PostBook(BookDTO bookDTO)
        {
            return Ok(_bookService.RegisterBook(new Book
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AmountCopies = bookDTO.AmountCopies,
                PageNumber = bookDTO.PageNumber,
                AuthorId = bookDTO.AuthorId,
                ReleaseYear = bookDTO.ReleaseYear.Date,
            }));
        }

        [HttpGet, AllowAnonymous, Route("{id}")]
        public IActionResult GetBook(Guid id)
        {
            return Ok(_bookService.GetBook(id));
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetBook([FromQuery]BookQuery parameters)
        {
            return Ok(_bookService.GetBooks(parameters));
        }

        [HttpPut, AllowAnonymous]
        public IActionResult PutBook(BookDTO bookDTO,Guid id)
        {
            return Ok(_bookService.UpdateBook(new Book 
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AmountCopies = bookDTO.AmountCopies,
                PageNumber = bookDTO.PageNumber,
            }, id));
        }

        [HttpDelete, AllowAnonymous, Route("{id}")]
        public IActionResult DeleteBook(Guid id) 
        {
            var deleted = _bookService.DeleteBook(id);
            if (deleted)
                return Ok();
            return BadRequest();
        }

    }
}
