namespace BibliotecaAPI.DTOs
{
    public class NewUserDTO : Validator
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public Models.Address Address { get; set; }

        public override void Validar()
        {
            Valido = true;
            CEP = CEP.Replace("-", "");
            CEP = CEP.Trim();
            if(Username is null || Username.Length < 4) Valido = false;
            else if(Password.Length < 7) Valido = false;
            else if(CEP is null || CEP.Length != 8) Valido = false;
            else if(Age < 6) Valido = false;
            else if(!int.TryParse(CPF, out var cep)) Valido = false;
        }
    }


}
