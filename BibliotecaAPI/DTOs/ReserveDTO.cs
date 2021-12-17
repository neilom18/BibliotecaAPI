using BibliotecaAPI.Models;
using System;
using System.Collections.Generic;

namespace BibliotecaAPI.DTOs
{
    public class ReserveDTO
    {

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<Guid> BookId { get; set; }

        
    }
}
