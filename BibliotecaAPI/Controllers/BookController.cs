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

        [HttpPost, Authorize(Roles = "admin,employeer")]
        public IActionResult PostBook(BookDTO bookDTO)
        {
            bookDTO.Validar();
            if (!bookDTO.Valido) return BadRequest(bookDTO.GetErrors());
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

        [HttpGet, Authorize, Route("{id}")]
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

        [HttpGet, Authorize]
        public IActionResult GetBook([FromQuery]BookQuery parameters)
        {
            return Ok(_bookService.GetBooks(parameters));
        }

        [HttpPut, Authorize(Roles = "admin,employeer")]
        public IActionResult PutBook(BookUpdateDTO bookDTO,Guid id)
        {
            bookDTO.Validar();
            if(!bookDTO.Valido) return BadRequest(bookDTO.GetErrors());
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

        [HttpDelete, Authorize(Roles = "admin,employeer"), Route("{id}")]
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
