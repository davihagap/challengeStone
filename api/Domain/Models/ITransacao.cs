namespace api.Domain.Models
{
    public class Transacao
    {
        public int Id {get; set;}

        public string Descricao {get; set;}

        public string Tipo {get; set;}

        public DateTime Data {get; set;}
        
        public decimal SaldoAnterior {get; set;}

        public decimal SaldoPosterior {get; set;}

        public decimal Valor {get; set;}

        public IConta Conta{get; set;}
        
    }
}