using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Conta
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MaxLength(50, ErrorMessage = "Deve conter no máximo 50 caracteres")]
        public string Cliente { get; set; }

        [Range(0.0,decimal.MaxValue, ErrorMessage = "Saldo deve ser positivo")]
        public decimal Saldo {get; set; }
    }
}