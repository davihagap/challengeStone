using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Conta
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Número de conta inválido")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Deve conter no máximo 50 caracteres")]
        public string Cliente { get; set; }

        [Range(0,int.MaxValue, ErrorMessage = "Saldo deve ser positivo")]
        public decimal Saldo {get; set; }
    }
}