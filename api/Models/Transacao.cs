using System;
using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Transacao
    {
        [Key]
        public int Id {get; set;}

        public string Descricao {get; set;}

        public string Tipo {get; set;}

        public DateTime Data {get; set;}

        [Range(0,int.MaxValue, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; set;}

        public int ContaNum{get; set;}

        public Conta Conta{get; set;}
    }
}