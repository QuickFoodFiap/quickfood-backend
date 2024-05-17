using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class ProdutoUseCase(IProdutoRepository produtoRepository) : IProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository = produtoRepository;

        public async Task<bool> CadastrarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var produtos = _produtoRepository.Find(e => e.Id == request.Id || e.Nome == request.Nome);

            var produtoExistente = produtos.FirstOrDefault(g => g.Id == request.Id);

            if (produtoExistente != null)
            {
                return false;
            }

            var produto = ProdutoFactory.Criar(request);

            await _produtoRepository.InsertAsync(produto, cancellationToken);

            return await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> AtualizarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var produtoExistente = await _produtoRepository.FindByIdAsync(request.Id, cancellationToken);

            if (produtoExistente == null)
            {
                return false;
            }

            var produto = ProdutoFactory.Criar(request);

            await _produtoRepository.UpdateAsync(produto, cancellationToken);

            return await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> DeletarProdutoAsync(Guid id, CancellationToken cancellationToken)
        {
            var produtoExistente = await _produtoRepository.FindByIdAsync(id, cancellationToken);

            if (produtoExistente == null)
            {
                return false;
            }

            await _produtoRepository.DeleteAsync(id, cancellationToken);

            return await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync(CancellationToken cancellationToken) =>
            await _produtoRepository.ObterTodosProdutosAsync();

        public async Task<IEnumerable<Produto>> ObterProdutosCategoriaAsync(Categoria categoria, CancellationToken cancellationToken) =>
            await _produtoRepository.ObterProdutosCategoriaAsync(categoria);
    }

    public static class ProdutoFactory
    {
        public static Produto Criar(ProdutoRequest request) =>
            new(request.Id, request.Nome, request.Descricao, request.Preco, request.Categoria, request.Ativo);
    }
}
