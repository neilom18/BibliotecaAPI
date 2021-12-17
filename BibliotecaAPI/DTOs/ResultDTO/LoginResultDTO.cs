namespace BibliotecaAPI.DTOs.ResultDTO
{
    public class LoginResultDTO
    {
        public bool Sucess { get; set; }
        public string[] Errors { get; set; }
        public UserLoginResultDTO UserLogin { get; set; }
    }

    public class UserLoginResultDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
