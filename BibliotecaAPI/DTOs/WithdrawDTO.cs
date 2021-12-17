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
        public IEnumerable<Guid> Book { get; set; } 

        public override void Validar()
        {
            Valido = true;
            if (StartDate < DateTime.UtcNow.AddMinutes(1)) Valido = false;
            else if(EndDate < DateTime.UtcNow.AddMonths(1)) Valido = false;
        }

    }
}
