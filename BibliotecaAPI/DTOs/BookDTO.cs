namespace BibliotecaAPI.DTOs
{
    public class BookDTO : Validator
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public System.Guid AuthorId { get; set; }
        public int ReleaseYear { get; set; }
        public int AmountCopies { get; set; }
        public uint PageNumber { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(AmountCopies <= 0) Valido = false;
            else if(Price <= 0) Valido = false;
            else if(PageNumber <= 0) Valido = false;
            else if(AmountCopies <= 0) Valido = false;
            else if(Description.Length <= 3 || Description.Length > 120) Valido = false;
            else if(ReleaseYear <= 0) Valido = false;
        }
    }
}
