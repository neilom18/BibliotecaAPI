using BibliotecaAPI.Models;
using BibliotecaAPI.Repositories;
using System;

namespace BibliotecaAPI.Services
{
    public class BookService
    {
        private readonly BookRepository _repository;
        private readonly AuthorRepository _authorRepository;

        public BookService(AuthorRepository authorRepository)
        {
            _repository = new BookRepository();
            _authorRepository = authorRepository;
        }

        public Book RegisterBook(Book book)
        {
            var author = _authorRepository.Get(book.AuthorId);
            book.AuthorName = author.Name;

            _authorRepository.AddBook(book);

            return _repository.Register(book);
        }

        public Book UpdateBook(Book book, Guid id) 
        {
            return _repository.Update(book, id);
        }
    }
}
