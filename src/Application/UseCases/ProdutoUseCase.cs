using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;

namespace Application.UseCases
{
    public class ProdutoUseCase(IProdutoRepository usuarioRepository) : IProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository = usuarioRepository;

        public async Task<bool> RegistrarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            var produtos = _produtoRepository.Find(e => e.Id == request.Id || e.Nome == request.Nome || e.Descricao == request.Descricao);

            var produtoExistente = produtos.FirstOrDefault(g => g.Id == request.Id);

            var produto = new Produto(produtoExistente?.Id ?? request.Id, request.Descricao, request.Nome, request.Preco, request.Categoria);

            if (produtoExistente != null)
            {
                await _produtoRepository.UpdateAsync(produto, cancellationToken);
            }
            else
            {
                await _produtoRepository.InsertAsync(produto, cancellationToken);
            }

            if (!await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken))
            {
                return false;
            }

            return true;
        }
    }
}
