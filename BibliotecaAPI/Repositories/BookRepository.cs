using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.Repositories
{
    public class BookRepository
    {
        private readonly Dictionary<Guid, Book> _repository;
        public BookRepository()
        {
            _repository = new Dictionary<Guid, Book>();
        }

        public Book Register(Book book)
        {
            book.Id = Guid.NewGuid();
            if(_repository.TryAdd(book.Id,book))
                return book;
            throw new Exception();
        }

        public Book Update(Book book, Guid id)
        {
            if(_repository.TryGetValue(id, out Book bookToUpdate))
            {
                bookToUpdate.Title = book.Title;
                bookToUpdate.Description = book.Description;
                bookToUpdate.AmountCopies = book.AmountCopies;
                bookToUpdate.PageNumber = book.PageNumber;

                return bookToUpdate;
            }

            throw new Exception();
        }
    }
}
