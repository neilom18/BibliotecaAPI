namespace BibliotecaAPI.Models
{
    public class Address
    {
        public Address(string? cep, string? logradouro, string? complemento, string? bairro, string? localidade, string? uf)
        {
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public string CEP { get; private set; }
        public string Logradouro { get; private set; }
        public string Complemento { get; private set; }
        public string Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Uf { get; private set; }

        public void Update(string? logradouro, string? complemento, string? bairro, string? localidade, string uf, string numero)
        {
            if (string.IsNullOrEmpty(Logradouro))
                Logradouro = logradouro;
            if (string.IsNullOrEmpty(Bairro))
                Bairro = bairro;
            if (string.IsNullOrEmpty(Complemento))
                Complemento = complemento;
            if(string.IsNullOrEmpty(Numero))
                Numero = numero;
            if (string.IsNullOrEmpty(Localidade))
                Localidade = localidade;
            if (string.IsNullOrEmpty(Uf))
                Uf = uf;
        }
    }
}
