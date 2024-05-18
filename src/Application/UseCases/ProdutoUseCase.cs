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

            var produtoExistente = _produtoRepository.Find(e => e.Id == request.Id || e.Nome == request.Nome || e.Descricao == request.Descricao).FirstOrDefault(g => g.Id == request.Id);

            if (produtoExistente != null)
            {
                return false;
            }

            var produto = new Produto(request.Id, request.Nome, request.Descricao, request.Preco, request.Categoria, request.Ativo);

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

            var produto = new Produto(request.Id, request.Nome, request.Descricao, request.Preco, request.Categoria, request.Ativo);

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
}
