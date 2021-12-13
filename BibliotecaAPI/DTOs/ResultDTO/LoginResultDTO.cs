namespace BibliotecaAPI.DTOs.Login
{
    public class LoginResultDTO
    {
        public bool Sucess { get; set; }
        public string[] Errors { get; set; }
        public UserLoginResultDTO UserLogin { get; set; }
    }

    public class UserLoginResultDTO
    {
        public System.Guid Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
