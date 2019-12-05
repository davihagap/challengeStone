using System;

namespace api.Domain.Models
{
    public interface ITransacao
    {
        public int Id {get;}

        public string Descricao {get;}

        public string Tipo {get;}

        public DateTime Data {get;}
        
        public decimal SaldoAnterior {get;}

        public decimal SaldoPosterior {get;}

        public decimal Valor {get;}

        public IConta Conta{get;}
        
    }
}