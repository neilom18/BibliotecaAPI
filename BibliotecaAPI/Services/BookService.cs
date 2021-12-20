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
            if (_repository.Get(book) is not null)
                throw new Exception("Esse livro ja foi cadastrado");
            var author = _authorRepository.Get(book.AuthorId);
            book.SetAuthorName(author.Name);
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

        public IEnumerable<Book> GetBooks(IEnumerable<Guid> ids)
        {
            return _repository.Get(ids);
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
