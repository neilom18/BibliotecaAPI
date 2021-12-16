using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.DTOs.ResultDTO;
using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Services
{
    public class BookService
    {
        private readonly BookRepository _repository;
        private readonly AuthorRepository _authorRepository;

        public BookService(BookRepository repository,AuthorRepository authorRepository)
        {
            _repository = repository;
            _authorRepository = authorRepository;
        }

        public Book RegisterBook(Book book)
        {
            var author = _authorRepository.Get(book.AuthorId);
            book.AuthorName = author.Name;

            book.Id = Guid.NewGuid();
            _authorRepository.AddBook(book);

            return _repository.Register(book);
        }

        public Book GetBook(Guid id)
        {
            return _repository.Get(id);
        }
        
        public IEnumerable<Book> GetBooks(BookQuery parameters)
        {
            return _repository.Get(parameters);
        }

        public Book UpdateBook(Book book, Guid id) 
        {
            return _repository.Update(book, id);
        }

        public bool DeleteBook(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}
