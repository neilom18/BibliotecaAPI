using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class AddressDTO : Validator
    {
        public AddressDTO(string cep, string logradouro, string complemento, string bairro, string localidade, string uf)
        {
            CEP = cep;
            Logradouro = logradouro;
            Complemento = complemento;
            Bairro = bairro;
            Localidade = localidade;
            Uf = uf;
        }

        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public string? Complemento { get; set; }
        public string? Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get;  set; }
        public string Uf { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            CEP = CEP.Replace("-", "");
            CEP = CEP.Trim();
            if (string.IsNullOrWhiteSpace(CEP) || CEP.Length != 8)
            {
                _errors.Add(nameof(CEP), "Deve ser informado um CEP válido");
                Valido = false;
            }

            if (!int.TryParse(CEP, out _))
            {
                _errors.Add(nameof(CEP), "O CEP não pode ser Alfanumérico");
                Valido = false;
            }

            if (string.IsNullOrWhiteSpace(Bairro))
            {
                _errors.Add(nameof(Bairro), "O bairro deve ser informado");
                Valido = false;
            }

            if (string.IsNullOrWhiteSpace(Uf))
            {
                _errors.Add(nameof(Uf), "A UF deve ser informada");
                Valido = false;
            }

            if (string.IsNullOrWhiteSpace(Numero))
            {
                _errors.Add(nameof(Numero), "O número deve ser informado");
                Valido = false;
            }
        }
    }
}
