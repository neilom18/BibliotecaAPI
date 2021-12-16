
namespace BibliotecaAPI.DTOs
{
    public class NewAuthorDTO : Validator
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(Name is null ||Name.Length < 4) Valido = false;
            else if(Age < 12) Valido = false;
            else if(Nationality is null) Valido = false;
        }
    }
}
