namespace BibliotecaAPI.Models
{
    public class Withdraw : Base
    {
        public bool Finalized { get; set; }
        public Reserve Reserve { get; set; }
    }
}
