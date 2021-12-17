namespace BibliotecaAPI.Models
{
    public class Address
    {
        public string Cep { get; private set; }
        public string? Logradouro { get; private set; }
        public string? Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string? Localidade { get; private set; }
        public string Uf { get; private set; }
    }
}
