using BibliotecaAPI.DTOs;
using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.DTOs.ResultDTO;
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
            bookDTO.Validar();
            if (!bookDTO.Valido) return BadRequest();
            try
            {
                return Ok(_bookService.RegisterBook(new Book
                        (
                            title: bookDTO.Title,
                            description: bookDTO.Description,
                            price: bookDTO.Price,
                            authorId: bookDTO.AuthorId,
                            releaseYear: bookDTO.ReleaseYear,
                            pageNumber: bookDTO.PageNumber,
                            amountCopies: bookDTO.AmountCopies
                        )));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
        }

        [HttpGet, AllowAnonymous, Route("{id}")]
        public IActionResult GetBook(Guid id)
        {
            try
            {
                return Ok(_bookService.GetBook(id));
            }
            catch(Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message});
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult GetBook([FromQuery]BookQuery parameters)
        {
            return Ok(_bookService.GetBooks(parameters));
        }

        [HttpPut, AllowAnonymous]
        public IActionResult PutBook(BookDTO bookDTO,Guid id)
        {
            bookDTO.Validar();
            if(!bookDTO.Valido) return BadRequest();
            return Ok(_bookService.UpdateBook(new Book
                (
                    title: bookDTO.Title,
                    description: bookDTO.Description,
                    price: bookDTO.Price,
                    releaseYear: bookDTO.ReleaseYear,
                    amountCopies: bookDTO.AmountCopies,
                    pageNumber: bookDTO.PageNumber
                    ), id));
        }

        [HttpDelete, AllowAnonymous, Route("{id}")]
        public IActionResult DeleteBook(Guid id) 
        {
            try
            {
                _bookService.DeleteBook(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message});
            }
        }

    }
}
