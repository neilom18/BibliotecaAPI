using BibliotecaAPI.Models;
using System;

namespace BibliotecaAPI.DTOs.ResultDTO
{
    public class UserDataDTO
    {
        public string CPF { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public int Age { get; set; }
    }
}
