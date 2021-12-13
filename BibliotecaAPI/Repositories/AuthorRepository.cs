﻿using BibliotecaAPI.DTOs.Query;
using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BibliotecaAPI.Repositories
{
    public class AuthorRepository
    {
        private readonly Dictionary<Guid, Author> _author;

        public AuthorRepository()
        {
            _author = new Dictionary<Guid, Author>();
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

            if (_author.TryGetValue(id, out var user))
                return user;

            throw new Exception("Usuario não encontrado");
        }

        public List<Author> Get(AuthorQuery parameters)
        {
            IEnumerable<Author> authorFiltered = _author.Values;

            if (parameters.Name != null)
                authorFiltered = authorFiltered.Where(a => a.Name == parameters.Name);
            if (parameters.Age != null)
                authorFiltered = authorFiltered.Where(a => a.Age == parameters.Age);
            if(parameters.Nationality != null)
                authorFiltered = authorFiltered.Where(a => a.Nationality == parameters.Nationality);
            
            return authorFiltered.Skip(parameters.Page == 1 ? 0 : (parameters.Page - 1) * parameters.Size)
                .Take(parameters.Size).ToList();
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

    }
}