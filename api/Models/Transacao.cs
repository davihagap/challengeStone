using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Transacao
    {
        public Transacao(string descricao, string tipo, DateTime data, decimal valor, int contaId)
        {
            Descricao = descricao;
            Tipo = tipo;
            Data = data;
            Valor = valor;
            ContaId = contaId;
        }

        [Key]
        public int Id {get; set;}

        public string Descricao {get; set;}

        public string Tipo {get; set;}

        public DateTime Data {get; set;}

        [Range(0,int.MaxValue, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; set;}

        public int ContaId{get; set;}

        public Conta Conta{get; set;}
    }
}