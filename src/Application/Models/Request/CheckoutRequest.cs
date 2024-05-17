using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public record CheckoutRequest
    {
        [EnumDataType(typeof(FormaPagamento))]
        [Display(Name = "Forma de Pagamento")]
        public FormaPagamento FormaPagamento { get; set; }
    }
}
