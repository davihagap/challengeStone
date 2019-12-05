using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Domain.Models
{
    public class Transacao: ITransacao
    {
        [Key]
        public int Id {get; protected set;}

        [Required]
        public string Descricao {get; protected set;}

        [Required]
        public string Tipo {get; protected set;}

        [Required]
        public DateTime Data {get; protected set;}

        [Required]
        [Range(0.01,9999999999.9, ErrorMessage = "Valor anterior deve ser positivo")]
        public decimal SaldoAnterior {get; protected set;}

        [Required]
        [Range(0.01,9999999999.9, ErrorMessage = "Valor anterior deve ser positivo")]
        public decimal SaldoPosterior {get; protected set;}

        [Required]
        [Range(0.01,9999999999.9, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; protected set;}

        [Required]
        public int NumeroConta {get; protected set;}

        [NotMapped]
        public IConta Conta{get; protected set;}

        public Transacao(){}

        public Transacao(
            IConta conta,
            string descricao,
            string tipo,
            DateTime data,
            decimal saldoAnterior,
            decimal saldoPosterior,
            decimal valor
            )
        {
            this.NumeroConta = conta.Numero;
            this.Conta = conta;
            this.Descricao = descricao;
            this.Tipo = tipo;
            this.Data = data;
            this.SaldoAnterior = saldoAnterior;
            this.SaldoPosterior = saldoPosterior;
            this.Valor = valor;            
        }
    }
}