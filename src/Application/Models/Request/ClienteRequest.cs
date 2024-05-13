using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public record ClienteRequest
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [StringLength(50, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 2)]
        public required string Nome { get; set; }

        [EmailAddress(ErrorMessage = "{0} em formato inválido.")]
        [StringLength(100, ErrorMessage = "O campo {0} deve conter entre {2} e {1} caracteres.", MinimumLength = 5)]
        [Display(Name = "E-mail")]
        public string? Email { get; set; }

        [StringLength(11, ErrorMessage = "O campo {0} deve conter {1} caracteres.")]
        [Display(Name = "CPF")]
        public string? Cpf { get; set; }

        public required bool Ativo { get; set; }
    }
}
