using System;

namespace api.Domain.Models
{
    public class Deposito : Transacao, IDeposito
    {
        private const decimal taxaDep = 0.01m;
        private const string descTaxaDep = "Taxa referente a depósito";
        private const string tipoTaxaDep = "TAXA";
        private const string descDep = "Depósito";
        private const string tipoDep = "DEP";

        public Deposito(IConta conta,
            DateTime data,
            decimal saldoAnterior,
            decimal saldoPosterior,
            decimal valor
        ) : base (conta,
            descDep,
            tipoDep,
            data,
            saldoAnterior,
            saldoPosterior,
            valor            
        )
        {}

        public ITransacao Taxa { get; private set; }

        public ITransacao calculaTaxa()
        {
            var valorTaxa = taxaDep * this.Valor;
            this.Conta.Debitar(valorTaxa);
            this.Taxa = new Transacao(this.Conta, 
                descTaxaDep, 
                tipoTaxaDep, 
                this.Data, 
                this.SaldoPosterior, 
                this.Conta.Saldo,
                valorTaxa);

            return this.Taxa;
        }

    }
}