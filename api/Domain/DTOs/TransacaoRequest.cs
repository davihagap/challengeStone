using System.ComponentModel.DataAnnotations;

namespace api.Domain.DTOs
{
    public class TransacaoRequest
    {
        [Required]
        [Range(1,int.MaxValue, ErrorMessage = "Número de conta inválido")]
        public int NumConta {get; set;}

        [Required]
        [Range(0.01,9999999999.9, ErrorMessage = "Valor deve ser positivo")]
        public decimal Valor {get; set;}
    }
}