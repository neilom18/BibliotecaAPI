namespace BibliotecaAPI.DTOs
{
    public class UserCraeteResultDTO
    {
        public bool Sucess { get; set; }
        public string[] Errors { get; set; }
        public UserDTO User { get; set; }
    }
}
