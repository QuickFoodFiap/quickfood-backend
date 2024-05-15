using Application.Models.Request;

namespace Application.UseCases
{
    public interface IPedidoUseCase
    {
        Task<bool> CadastrarPedidoAsync(PedidoRequest request, CancellationToken cancellationToken);
    }
}
