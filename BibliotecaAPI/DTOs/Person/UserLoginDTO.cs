using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class UserLoginDTO : Validator
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if(Username is null || Username.Length < 4)
            {
                _errors.Add(nameof(Username), "O nome deve conter no mínimo 4 caracteres");
                Valido = false;
            }

            if (Password is null || Password.Length < 7)
            {
                _errors.Add(nameof(Password), "A senha deve conter ao menos 7 caracteres ou digitos");
                Valido = false;
            }
        }
    }
}
