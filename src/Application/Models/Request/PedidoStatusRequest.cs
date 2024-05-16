using Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request
{
    public class PedidoStatusRequest
    {
        [EnumDataType(typeof(PedidoStatus))]
        public PedidoStatus Status { get; set; }
    }
}
