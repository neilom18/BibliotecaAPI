namespace BibliotecaAPI.DTOs
{
    public class AddressDTO : Validator
    {
        public AddressDTO(string cep, string logradouro, string complemento, string bairro, string localidade, string uf)
        {
            Cep = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get;  set; }
        public string Uf { get; set; }

        public override void Validar()
        {
            Valido = true;
            Cep = Cep.Replace("-", "");
            Cep = Cep.Trim();
            if (string.IsNullOrWhiteSpace(Cep) || Cep.Length != 8) Valido = false;
            if (!int.TryParse(Cep, out _)) Valido = false;
            if (string.IsNullOrWhiteSpace(Bairro)) Valido = false;
            if (string.IsNullOrWhiteSpace(Uf)) Valido = false;
            if (string.IsNullOrWhiteSpace(Localidade)) Valido = false;
        }
    }
}
