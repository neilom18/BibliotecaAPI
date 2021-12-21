namespace BibliotecaAPI.DTOs.ResultDTO
{
    public class UserCraeteResultDTO
    {
        public bool Sucess { get; set; }
        public string[] Errors { get; set; }
        public UserDTO User { get; set; }
    }

    public class UserDTO
    {
        public string Username { get; set; }
        public string Role { get; set; }
    }
}
