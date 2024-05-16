using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class PedidoUseCase : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public PedidoUseCase(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> CadastrarPedidoAsync(PedidoRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var pedidoExistente = await _pedidoRepository.FindByIdAsync(request.PedidoId, cancellationToken);

            if (pedidoExistente != null)
            {
                throw new InvalidOperationException("Pedido já existe.");
            }

            var pedido = new Pedido(request.PedidoId, request.ClienteId);

            foreach (var item in request.Items)
            {
                var produto = await _produtoRepository.FindByIdAsync(item.ProdutoId, cancellationToken) ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

                pedido.AdicionarItem(new PedidoItem(item.ProdutoId, item.Quantidade, produto.Preco));
            }

            await _pedidoRepository.InsertAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> EfetuarCheckoutAsync(Guid pedidoId, CancellationToken cancellationToken)
        {
            var pedido = await GetPedidoOrThrowAsync(pedidoId, cancellationToken);

            if (!pedido.EfetuarCheckout())
            {
                throw new InvalidOperationException("Não foi possível realizar o checkout do pedido.");
            }

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> AlterarStatusAsync(Guid pedidoId, PedidoStatus pedidoStatus, CancellationToken cancellationToken)
        {
            var pedido = await GetPedidoOrThrowAsync(pedidoId, cancellationToken);

            if (!pedido.AlterarStatus(pedidoStatus))
            {
                throw new InvalidOperationException("Não foi possível alterar o status do pedido.");
            }

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(CancellationToken cancellationToken) =>
            _pedidoRepository.ObterTodosPedidosAsync();

        private async Task<Pedido> GetPedidoOrThrowAsync(Guid pedidoId, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.FindByIdAsync(pedidoId, cancellationToken);

            return pedido ?? throw new InvalidOperationException($"Pedido {pedidoId} não encontrado.");
        }
    }
}
