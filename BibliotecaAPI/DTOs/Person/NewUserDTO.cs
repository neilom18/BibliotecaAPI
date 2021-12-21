using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class NewUserDTO : Validator
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string Document { get; set; }
        public string CEP { get; set; }
        public AddressDTO? Address { get; set; }

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

            if(!int.TryParse(Document, out _)) 
            {
                _errors.Add(nameof(Document), "O documento não pode ser Alfanumérico");
                Valido = false;
            }

            if (Username is null || Username.Length < 4)
            {
                _errors.Add(nameof(Username), "O nome deve conter ao menos 4 caracteres");
                Valido = false;
            }

            if(Password.Length < 7)
            {
                _errors.Add(nameof(Password), "A senha deve conter ao menos 7 caracteres ou digitos");
                Valido = false;
            }

            if(Age < 12)
            {
                _errors.Add(nameof(Age), "O usuário deve conter no mínimo 12 anos");
                Valido = false;
            }

            if(Age > 122 || Age < 0)
            {
                _errors.Add(nameof(Age), "O usuário deve conter uma idade válida");
                Valido = false;
            }
            if(Address is not null)
            {
                Address.Validar();
                if (!Address.Valido) 
                {
                    _errors.Add(nameof(Address), "O endereço deve ser válido");
                    foreach(var erro in Address.GetErrors())
                    {
                        _errors.Add(erro.Key, erro.Value);
                    }
                    Valido = false; 
                } 
            }
        }
    }


}
