using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Author : Base
    {
        public Author(string name, int age, string nationality) : base()
        {
            Name = name;
            Age = age;
            Nationality = nationality;
            Book = new List<Book>();
        }

        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Nationality { get; private set; }
        public List<Book> Book { get; private set; }

        public void Update(Author author)
        {
            Name = author.Name;
            Age = author.Age;
            Nationality = author.Nationality;
        }
    }
}
