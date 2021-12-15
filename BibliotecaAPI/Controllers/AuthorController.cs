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
    public class AuthorController : ControllerBase
    {
        private readonly AuthorService _authorService;
        public AuthorController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult PostAuthor(NewAuthorDTO newAuthor)
        {
            newAuthor.Validar();
            if (!newAuthor.Valido) return BadRequest();
            return Ok(_authorService.RegisterAuthor(
                new Author
                {
                    Name = newAuthor.Name,
                    Age = newAuthor.Age,
                    Nationality = newAuthor.Nationality,
                }));
        }

        [HttpGet, AllowAnonymous, Route("{id}")]
        public IActionResult GetAuthorById(Guid id)
        {
            return Ok(_authorService.Get(id));
        }

        [HttpPut, AllowAnonymous]
        public IActionResult PutAuthor(NewAuthorDTO newAuthor,[FromQuery] Guid id)
        {
            return Ok(_authorService.Update(new Author
            {
                Name = newAuthor.Name,
                Age = newAuthor.Age,
                Nationality = newAuthor.Nationality,
            }, id));
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get([FromQuery] AuthorQuery parameters) 
        {
            return Ok(_authorService.Get(parameters));
        }

        [HttpDelete, AllowAnonymous, Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            _authorService.Delete(id);
            return Ok();
        }
    }
}
