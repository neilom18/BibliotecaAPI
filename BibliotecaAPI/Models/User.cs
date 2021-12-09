using System;

namespace BibliotecaAPI.Models
{
    public class User : Base
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "cliente";
        public int FailedAttempts { get; set; } = 0;
        public bool IsLockout { get; set; } = false;
        public DateTime? LockoutDate { get; set; }


    }
}
