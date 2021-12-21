using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class ResetPasswordDTO : Validator
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            if(NewPassword.Length < 7)
            {
                _errors.Add(nameof(NewPassword), "A senha deve conter ao menos 7 caracteres ou digitos");
                Valido = false;
            }

            if(NewPassword == OldPassword)
            {
                _errors.Add(nameof(NewPassword), "Senha informada inválida");
                Valido = false;
            }
        }
    }
}
