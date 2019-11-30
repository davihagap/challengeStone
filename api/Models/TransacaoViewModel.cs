using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class TransacaoViewModel
    {
        public string Descricao {get; set;}

        [Range(0,int.MaxValue, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; set;}
    }
}