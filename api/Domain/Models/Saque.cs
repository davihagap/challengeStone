using System;

namespace api.Domain.Models
{
    public class Saque : Transacao, ISaque
    {
        private const decimal taxaSaq = 4m;
        private const string descTaxaSaq = "Taxa referente a saque";
        private const string tipoTaxaSaq = "TAXA";
        private const string descSaq = "Saque";
        private const string tipoSaq = "SAQ";

        public Deposito(IConta conta,
            DateTime data,
            decimal saldoAnterior,
            decimal saldoPosterior,
            decimal valor
        ) : base (conta,
            descSaq,
            tipoSaq,
            data,
            saldoAnterior,
            saldoPosterior,
            valor            
        )
        {}

        public ITransacao Taxa { get; private set; }

        public ITransacao calculaTaxa()
        {
            this.Conta.Debitar(taxaSaq);
            this.Taxa = new Transacao(this.Conta, 
                descTaxaSaq, 
                tipoTaxaSaq, 
                this.Data, 
                this.SaldoPosterior, 
                this.Conta.Saldo,
                taxaSaq);

            return this.Taxa;
        }

    }
}