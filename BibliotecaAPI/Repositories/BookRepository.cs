using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.ExtensionsMethod;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class BookRepository
    {
        private readonly Dictionary<Guid, Book> _book;
        public BookRepository()
        {
            _book = new Dictionary<Guid, Book>();
        }

        public Book Register(Book book)
        {
            if(_book.TryAdd(book.Id,book))
                return book;
            throw new Exception();
        }

        public Book Get(Guid id)
        {
            if(_book.TryGetValue(id, out Book book))
                return book;

            throw new Exception();
        }

        public IEnumerable<Book> Get(IEnumerable<Guid> ids)
        {
            foreach(var id in ids)
            {
                yield return Get(id);
            }
        }

        public IEnumerable<Book> Get(BookQuery parameters)
        {
            IEnumerable<Book> bookFiltered = _book.Values;

            /*if(parameters.AuthorName != null)
                bookFiltered = bookFiltered.Where(b => b.AuthorName == parameters.AuthorName);
            if(parameters.Title != null)
                bookFiltered = bookFiltered.Where(b => b.Title == parameters.Title);
            if(parameters.Description != null)
                bookFiltered = bookFiltered.Where(b => b.Description == parameters.Description);
            if(parameters.ReleaseYear != null)
                bookFiltered = bookFiltered.Where(b => b.ReleaseYear == parameters.ReleaseYear);*/

            bookFiltered.WhereIf(parameters.AuthorName, x => x.AuthorName == parameters.AuthorName)
                .WhereIf(parameters.Title, x => x.Title == parameters.Title)
                .WhereIf(parameters.Description, x => x.Description == parameters.Description)
                .WhereIf(parameters.ReleaseYear, x => x.ReleaseYear == parameters.ReleaseYear);

            return bookFiltered.Paginaze(parameters.Page, parameters.Size);
        }

        public Book Update(Book book, Guid id)
        {
            if(_book.TryGetValue(id, out Book bookToUpdate))
            {
                bookToUpdate = book;
                return bookToUpdate;
            }

            throw new Exception();
        }

        public bool Delete(Guid id)
        {
            var book = Get(id);
            if(_book.Remove(book.Id))
                return true;
            return false;
        }

        public void DeleteAllByAuthor(List<Book> books)
        {
            foreach (Book book in books)
                _book.Remove(book.Id);
        }
    }
}
