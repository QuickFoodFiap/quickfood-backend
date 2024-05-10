using Application.Models.Request;

namespace Application.UseCases
{
    public interface IProdutoUseCase
    {
        Task<bool> RegistrarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken);
    }
}
