namespace BibliotecaAPI.DTOs.Query
{
    public class UserQuery
    {
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? CPF { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
    }
}
