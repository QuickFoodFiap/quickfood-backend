using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class ProdutoUseCase(IProdutoRepository usuarioRepository) : IProdutoUseCase
    {
        private readonly IProdutoRepository _produtoRepository = usuarioRepository;

        public async Task<bool> CadastrarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            var produtos = _produtoRepository.Find(e => e.Id == request.Id || e.Nome == request.Nome || e.Descricao == request.Descricao);

            var produtoExistente = produtos.FirstOrDefault(g => g.Id == request.Id);


            if (produtoExistente == null)
            {
                var produto = new Produto(request.Id, request.Descricao, request.Nome, request.Preco, request.Categoria, request.Ativo);
                await _produtoRepository.InsertAsync(produto, cancellationToken);
            }
            else
            {
                return false;
            }

            if (!await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> AtualizarProdutoAsync(ProdutoRequest request, CancellationToken cancellationToken)
        {
            var produtoExistente = await _produtoRepository.FindByIdAsync(request.Id, cancellationToken);

            if (produtoExistente != null)
            {
                var produto = new Produto(request.Id, request.Descricao, request.Nome, request.Preco, request.Categoria, request.Ativo);
                await _produtoRepository.UpdateAsync(produto, cancellationToken);
            }

            if (!await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken))
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeletarProdutoAsync(Guid id, CancellationToken cancellationToken)
        {
            var produtoExistente = await _produtoRepository.FindByIdAsync(id, cancellationToken);

            if (produtoExistente != null)
            {
                await _produtoRepository.DeleteAsync(id, cancellationToken);
            }
            else
            {
                return false;
            }

            if (!await _produtoRepository.UnitOfWork.CommitAsync(cancellationToken))
            {
                return false;
            }

            return true;
        }

        public async Task<IEnumerable<Produto>> ObterTodosProdutosAsync(CancellationToken cancellationToken) =>
            await _produtoRepository.ObterTodosProdutosAsync();

        public async Task<IEnumerable<Produto>> ObterProdutosCategoriaAsync(Categoria categoria, CancellationToken cancellationToken) =>
            await _produtoRepository.ObterProdutosCategoriaAsync(categoria);
    }
}
