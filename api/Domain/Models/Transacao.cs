using System;
using System.ComponentModel.DataAnnotations;

namespace api.Domain.Models
{
    public class Transacao
    {
        [Key]
        public int Id {get; set;}

        public string Descricao {get; set;}

        public string Tipo {get; set;}

        public DateTime Data {get; set;}

        [Range(0.01,9999999999.9, ErrorMessage = "Valor anterior deve ser positivo")]
        public decimal SaldoAnterior {get; set;}

        [Range(0.01,9999999999.9, ErrorMessage = "Valor anterior deve ser positivo")]
        public decimal SaldoPosterior {get; set;}

        [Range(0.01,9999999999.9, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; set;}

        public IConta Conta{get; set;}

        public Transacao(IConta conta,
            string descricao,
            string tipo,
            DateTime data,
            decimal saldoAnterior,
            decimal saldoPosterior,
            decimal valor
            )
        {
            this.Conta = conta;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.Data = data;
            this.SaldoAnterior = saldoAnterior;
            this.SaldoPosterior = SaldoPosterior;
            this.Valor = valor;            
        }
    }
}