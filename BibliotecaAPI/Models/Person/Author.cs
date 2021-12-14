using System.Collections.Generic;

namespace BibliotecaAPI.Models
{
    public class Author : Base
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }
        public List<Book> Books { get; set; }
    }
}
