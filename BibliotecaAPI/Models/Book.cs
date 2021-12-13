﻿namespace BibliotecaAPI.Models
{
    public class Book : Base
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public System.Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int AmountCopies { get; set; }
        public uint PageNumber { get; set; }
    }
}
