namespace BibliotecaAPI.DTOs.Query
{
    public class BookQuery
    {
        public string? AuthorName { get; set; }
        public string? Title { get; set; }
        public System.DateTime? ReleaseYear { get; set; }
        public string? Description { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
    }
}
