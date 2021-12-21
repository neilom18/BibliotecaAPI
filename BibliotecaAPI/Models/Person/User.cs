using System;

namespace BibliotecaAPI.Models
{
    public class User : Base
    {
        public User(string username, string password, string document, int age) : base()
        {
            Username = username;
            Password = password;
            Document = document;
            Age = age;
        }

        public string Username { get;private set; }
        public string Password { get;private set; }
        public string Role { get;private set; }
        public string Document { get;private set; }
        public int Age { get;private set; }
        public int FailedAttempts { get;private set; } = 0;
        public bool IsLockout { get;private set; } = false;
        public DateTime? LockoutDate { get;private set; }

        public void SetPassword(string password) => Password = password;
        public void IncrementFailedAttempts() => FailedAttempts++;
        public void Lock()
        {
            LockoutDate = DateTime.Now;
            IsLockout = true;
        }
        public void Unlock()
        {
            LockoutDate = null;
            IsLockout = false;
            FailedAttempts = 0;
        }
        public void SetRole(string role) => Role = role;

    }
}
