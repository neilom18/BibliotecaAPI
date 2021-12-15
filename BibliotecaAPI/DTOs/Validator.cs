namespace BibliotecaAPI.DTOs
{
    public abstract class Validator
    {
        public bool Valido { get;protected set; }
        public abstract void Validar();
    }
}
