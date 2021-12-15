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
        public List<Book> Book { get; set; } // Trocar Book por BookDTO
                                             // e fazer as validações e instanciar por method de extensão

        public override void Validar()
        {
            throw new NotImplementedException();
        }

    }
}
