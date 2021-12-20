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
            try
            {
                return Ok(_authorService.RegisterAuthor(
                        new Author
                        (
                            name: newAuthor.Name,
                            age: newAuthor.Age,
                            nationality: newAuthor.Nationality
                            )));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message }) ;
            }
        }

        [HttpGet, AllowAnonymous, Route("{id}")]
        public IActionResult GetAuthorById(Guid id)
        {
            try
            {
                return Ok(_authorService.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
        }

        [HttpPut, AllowAnonymous]
        public IActionResult PutAuthor(NewAuthorDTO newAuthor,[FromQuery] Guid id)
        {
            try
            {
                return Ok(_authorService.Update(new Author
                    (
                        name: newAuthor.Name,
                        age: newAuthor.Age,
                        nationality: newAuthor.Nationality
                        ), id));
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Get([FromQuery] AuthorQuery parameters) 
        {
            return Ok(_authorService.Get(parameters));
        }

        [HttpDelete, AllowAnonymous, Route("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                _authorService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new ExceptionResult { Sucess = false, Message = ex.Message });
            }
        }
    }
}
