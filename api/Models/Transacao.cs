using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Transacao
    {
        [Key]
        public int Id {get; set;}

        public string Descricao {get; set;}

        public string Tipo {get; set;}

        public int ContaId{get; set;}

        public Conta Conta{get; set;}
    }
}