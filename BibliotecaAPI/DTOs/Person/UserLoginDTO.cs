namespace BibliotecaAPI.DTOs
{
    public class UserLoginDTO : Validator
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(Username is null || Username.Length < 4) Valido = false;
            if(Password is null || Password.Length < 7) Valido = false;
        }
    }
}
