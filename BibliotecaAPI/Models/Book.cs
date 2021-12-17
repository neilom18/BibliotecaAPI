using System;

namespace BibliotecaAPI.Models
{
    public class Book : Base
    {
        public Book(string title, string description, decimal price, Guid authorId,
            DateTime releaseYear, int amountCopies, uint pageNumber)
        {
            Title = title;
            Description = description;
            Price = price;
            AuthorId = authorId;
            ReleaseYear = releaseYear;
            AmountCopies = amountCopies;
            PageNumber = pageNumber;
        }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public Guid AuthorId { get; private set; }
        public DateTime ReleaseYear { get; private set; } // .Net 6 tem o DataOnly
        public string AuthorName { get; private set; }
        public int AmountCopies { get; private set; }
        public uint PageNumber { get; private set; }

        public void SetAuthorName(string authorName) => AuthorName = authorName;
    }
}
