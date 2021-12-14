namespace BibliotecaAPI.Models
{
    public class Book : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.Guid AuthorId { get; set; }
        public System.DateTime ReleaseYear { get; set; } // .Net 6 tem o DataOnly
        public string AuthorName { get; set; }
        public int AmountCopies { get; set; }
        public uint PageNumber { get; set; }
    }
}
