using System.ComponentModel.DataAnnotations;

namespace api.Domain.Models
{
    public class Conta:Iconta
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Número de conta inválido")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo Cliente é obrigatório")]
        [MaxLength(50, ErrorMessage = "Deve conter no máximo 50 caracteres")]
        public string Cliente { get; set; }

        [Range(0.01,9999999999.9, ErrorMessage = "Saldo deve ser positivo")]
        public decimal Saldo {get; set; }

        public void Creditar(decimal valor)
        {
            if (valor < 0.01m)
            {
                throw new System.Exception("Valor inválido");
            }

            Saldo += valor;
        }

        public void Debitar(decimal valor)
        {
            if (valor < 0.01m)
            {
                throw new System.Exception("Valor inválido");
            }

            if (valor > Saldo)
            {
                throw new System.Exception("Saldo insuficiente");
            }

            Saldo -= valor;
        }
    }
}