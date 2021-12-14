using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Services
{
    public class AuthorService
    {
        private readonly AuthorRepository _repository;
        public AuthorService(AuthorRepository author)
        {
            _repository = author;
        }

        public Author RegisterAuthor(Author author)
        {
            _repository.Register(author);
            return author;
        }

        public Author Get(Guid id)
        {
            return _repository.Get(id);
        }

        public Author Update(Author author, Guid id)
        {
            return _repository.Update(author, id);
        }

        public IEnumerable<Author> Get(AuthorQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public void Delete(Guid id)
        {
            _repository.Delete(id);
        }
    }
}
