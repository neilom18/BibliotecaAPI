using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class WithdrawDTO : Validator
    {
        public Guid? ReserveId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> Book { get; set; }

        public override Dictionary<string, string> GetErrors()
        {
            return _errors;
        }

        public override void Validar()
        {
            Valido = true;

            if (StartDate.Date < DateTime.UtcNow.Date)
            {
                _errors.Add(nameof(StartDate), "Data de inicio não pode ser menor que a data atual");
                Valido = false;
            }

            if(EndDate.Date > DateTime.UtcNow.Date.AddMonths(3)) 
            {
                _errors.Add(nameof(EndDate), "Não pode alugar um livro pra mais de 3 meses");
                Valido = false;
            }

            if (EndDate <= StartDate)
            {
                _errors.Add(nameof(EndDate), "A data de término tem que ser maior que a data de início");
                Valido = false;
            }

            if (Book.Count > 10)
            {
                _errors.Add(nameof(Book), "O número máximo de livros para reservar é 10");
                Valido = false;
            }
        }

    }
}
