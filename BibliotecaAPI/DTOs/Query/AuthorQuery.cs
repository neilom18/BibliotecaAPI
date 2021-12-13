namespace BibliotecaAPI.DTOs.Query
{
    public class AuthorQuery
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Nationality { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 20;
    }
}
