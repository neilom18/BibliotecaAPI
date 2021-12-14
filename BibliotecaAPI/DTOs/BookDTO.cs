namespace BibliotecaAPI.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.Guid AuthorId { get; set; }
        public System.DateTime ReleaseYear { get; set; }
        public int AmountCopies { get; set; }
        public uint PageNumber { get; set; }
    }
}
