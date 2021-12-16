using BibliotecaAPI.Enums;
using System;

namespace BibliotecaAPI.DTOs.Query
{
    public class WithdrawQuery
    {
        public EStatus Finalized { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? AuthorId { get; set; }
        public string? BookName { get; set; }
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
    }
}
