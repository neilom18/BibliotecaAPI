using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class BookUpdateDTO : Validator
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }
        public int AmountCopies { get; set; }
        public uint PageNumber { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }
        public override void Validar()
        {
            Valido = true;
            if (AmountCopies <= 0)
            {
                _errors.Add(nameof(AmountCopies), "Numero de cópias tem que ser maior que 0.");
                Valido = false;
            }

            if (Price <= 0)
            {
                _errors.Add(nameof(Price), "O preço tem que ser maior que 0");
                Valido = false;
            }

            if (PageNumber <= 0)
            {
                _errors.Add(nameof(PageNumber), "O numero de páginas tem que ser maior que 0.");
                Valido = false;
            }

            if (Description.Length <= 3 || Description.Length > 120)
            {
                _errors.Add(nameof(Description), "A descrição deve conter entre 4 e 120 de tamanho");
                Valido = false;
            }

            if (ReleaseYear <= 0)
            {
                _errors.Add(nameof(ReleaseYear), "O ano não pode ser 0 ou negativo");
                Valido = false;
            }

            if (ReleaseYear > System.DateTime.Today.Year)
            {
                _errors.Add(nameof(ReleaseYear), "Não pode cadastrar um livro com a data de lançamento nos próximos anos");
            }
        }
    }
}

