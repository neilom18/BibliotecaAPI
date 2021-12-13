using BibliotecaAPI.DTOs;
using BibliotecaAPI.Models;
using BibliotecaAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            }));
        }

        [HttpPut, AllowAnonymous]
        public IActionResult PutBook(BookDTO bookDTO, System.Guid id)
        {
            return Ok(_bookService.UpdateBook(new Book 
            {
                Title = bookDTO.Title,
                Description = bookDTO.Description,
                AmountCopies = bookDTO.AmountCopies,
                PageNumber = bookDTO.PageNumber,
            }, id));
        }
    }
}
