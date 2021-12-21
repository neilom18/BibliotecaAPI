
using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class NewAuthorDTO : Validator
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Nationality { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;
            
            if (Name is null || Name.Length < 4)
            {
                _errors.Add(nameof(Name), "O nome precisa ter no mínimo 4 caracteres");
                Valido = false;
            }

            foreach(var c in Name)
            {
                if (char.IsDigit(c))
                {
                    _errors.Add(nameof(Name), "O nome não pode contem números");
                }
            }

            if (Age < 12)
            {
                _errors.Add(nameof(Age), "Autor precisa ter no mínimo 12 anos");
                Valido = false;
            }

            if (string.IsNullOrWhiteSpace(Nationality))
            {
                _errors.Add(nameof(Nationality), "Precisa ser informado a nacionalidade");
                Valido = false;
            }
        }
    }
}
