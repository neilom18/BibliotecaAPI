namespace BibliotecaAPI.DTOs
{
    public class ResetPasswordDTO : Validator
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }

        public override void Validar()
        {
            Valido = true;
            if(NewPassword.Length < 7) Valido = false;
        }
    }
}
