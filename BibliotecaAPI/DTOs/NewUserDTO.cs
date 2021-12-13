namespace BibliotecaAPI.DTOs
{
    public class NewUserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public Models.Address Address { get; set; }
    }
}
