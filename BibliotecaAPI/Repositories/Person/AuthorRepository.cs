﻿using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.ExtensionsMethod;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class AuthorRepository
    {
        private readonly Dictionary<Guid, Author> _author;
        private readonly BookRepository _bookRepository;

        public AuthorRepository(BookRepository bookRepository)
        {
            _author = new Dictionary<Guid, Author>();
            _bookRepository = bookRepository;
        }

        public void Register(Author author)
        {
            Guid id = Guid.NewGuid();
            author.Id = id;
            author.Books = new List<Book>();
            if (!_author.TryAdd(id, author))
                throw new Exception();
        }

        public void AddBook(Book book)
        {
            var author = Get(book.AuthorId);
            author.Books.Add(book);
        }

        public Author Get(Guid id)
        {
            //return _author.TryGetValue(id, out Author author) ? author : null;
                                                            
            // Escolher uma das duas formas para padronizar 

            if (_author.TryGetValue(id, out var author))
                return author;

            throw new Exception("Usuario não encontrado");
        }

        public List<Author> Get(AuthorQuery parameters)
        {
            IEnumerable<Author> authorFiltered = _author.Values;

            /*if (parameters.Name != null)
                authorFiltered = authorFiltered.Where(a => a.Name == parameters.Name);
            if (parameters.Age != null)
                authorFiltered = authorFiltered.Where(a => a.Age == parameters.Age);
            if(parameters.Nationality != null)
                authorFiltered = authorFiltered.Where(a => a.Nationality == parameters.Nationality);*/

            authorFiltered.WhereIf(parameters.Name, x => x.Name == parameters.Name)
                .WhereIf(parameters.Age, x => x.Age == parameters.Age)
                .WhereIf(parameters.Nationality, x => x.Nationality == parameters.Nationality);
            
            return authorFiltered.Paginaze(parameters.Page, parameters.Size).ToList();
        }

        public Author Update(Author author, Guid id)
        {
            if(_author.TryGetValue(id, out var authorToUpdate))
            {
                authorToUpdate.Name = author.Name;
                authorToUpdate.Age = author.Age;
                authorToUpdate.Nationality = author.Nationality;

                return Get(id);  
            }

            throw new Exception("Usuario não encontrado");
        }

        public void Delete(Guid id)
        {
            var author = Get(id);
            _bookRepository.DeleteAllByAuthor(author.Books);
            _author.Remove(id);
        }
    }
}
