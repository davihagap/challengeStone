using System.ComponentModel.DataAnnotations;
using api.Domain.Exceptions;

namespace api.Domain.Models
{
    public class Conta:IConta
    {
        [Key]
        [Range(1, int.MaxValue, ErrorMessage = "Número de conta inválido")]
        public int Numero { get; set; }

        [Required(ErrorMessage = "Campo Cliente é obrigatório")]
        [MaxLength(50, ErrorMessage = "Deve conter no máximo 50 caracteres")]
        public string Cliente { get; set; }

        [Required(ErrorMessage = "Campo Saldo é obrigatório")]
        [Range(0.01,9999999999.9, ErrorMessage = "Saldo deve ser positivo")]
        public decimal Saldo {get; set; }

        public Conta (){}
        
        public Conta(int numero, string cliente, decimal saldo)
        {
            this.Numero = numero;
            this.Cliente = cliente;
            this.Saldo = saldo;
        }

        public void Creditar(decimal valor)
        {
            this.Saldo += valor;
        }

        public void Debitar(decimal valor)
        {
            if (valor > Saldo)
            {
                throw new SaldoInsuficienteException("Saldo insuficiente");
            }

            this.Saldo -= valor;
        }
    }
}