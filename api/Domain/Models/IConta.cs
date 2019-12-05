namespace api.Domain.Models
{
    public interface IConta
    {
        public int Numero { get; set; }

        public string Cliente { get; set; }

        public decimal Saldo {get; set; }

        void Creditar(decimal valor);
        void Debitar(decimal valor);
    }
}