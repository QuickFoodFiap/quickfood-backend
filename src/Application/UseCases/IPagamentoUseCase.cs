using Domain.Entities;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public interface IPagamentoUseCase
    {
        Task<bool> EfetuarPagamento(Pedido pedido, FormaPagamento tipoPagamento, CancellationToken cancellationToken);
    }
}
