using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class ReserveDTO : Validator
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> BookId { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(StartDate <= DateTime.UtcNow) Valido = false;
            else if(BookId.Count > 10) Valido = false;
            else if(EndDate <= StartDate) Valido = false;
        }
    }
}
