using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class PedidoUseCase(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository, IPagamentoUseCase pagamentoUseCase) : IPedidoUseCase
    {
        private readonly IPedidoRepository _pedidoRepository = pedidoRepository;
        private readonly IProdutoRepository _produtoRepository = produtoRepository;
        private readonly IPagamentoUseCase _pagamentoUseCase = pagamentoUseCase;

        public async Task<bool> CadastrarPedidoAsync(PedidoRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var pedidoExistente = await _pedidoRepository.FindByIdAsync(request.PedidoId, cancellationToken);

            if (pedidoExistente != null)
            {
                throw new InvalidOperationException("Pedido já existe.");
            }

            var pedido = await CriarPedidoAsync(request, cancellationToken);

            await _pedidoRepository.InsertAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> EfetuarCheckoutAsync(Guid pedidoId, CheckoutRequest request, CancellationToken cancellationToken)
        {
            var pedido = await ObterPedidos(pedidoId, cancellationToken);

            if (!pedido.EfetuarCheckout())
            {
                throw new InvalidOperationException("Não foi possível realizar o checkout do pedido.");
            }

            if (await _pagamentoUseCase.EfetuarPagamento(pedido, request.FormaPagamento, cancellationToken))
            {
                pedido.AlterarStatusPagamento(StatusTransacao.Pago);
            }
            else
            {
                pedido.CancelarCheckout();
                throw new InvalidOperationException("Não foi possível realizar o checkout do pedido devido o pagamento não ter sido devidamente realizado.");
            }

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public async Task<bool> AlterarStatusAsync(Guid pedidoId, PedidoStatus pedidoStatus, CancellationToken cancellationToken)
        {
            var pedido = await ObterPedidos(pedidoId, cancellationToken);

            if (!pedido.AlterarStatus(pedidoStatus))
            {
                throw new InvalidOperationException("Não foi possível alterar o status do pedido.");
            }

            await _pedidoRepository.UpdateAsync(pedido, cancellationToken);

            return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
        }

        public Task<IEnumerable<Pedido>> ObterTodosPedidosAsync(CancellationToken cancellationToken) =>
            _pedidoRepository.ObterTodosPedidosAsync();

        private async Task<Pedido> ObterPedidos(Guid pedidoId, CancellationToken cancellationToken)
        {
            var pedido = await _pedidoRepository.FindByIdAsync(pedidoId, cancellationToken);

            return pedido ?? throw new InvalidOperationException($"Pedido {pedidoId} não encontrado.");
        }

        private async Task<Pedido> CriarPedidoAsync(PedidoRequest request, CancellationToken cancellationToken)
        {
            var pedido = new Pedido(request.PedidoId, request.ClienteId);

            foreach (var item in request.Items)
            {
                var produto = await _produtoRepository.FindByIdAsync(item.ProdutoId, cancellationToken) ?? throw new InvalidOperationException($"Produto {item.ProdutoId} não encontrado.");

                pedido.AdicionarItem(new PedidoItem(produto.Id, item.Quantidade, produto.Preco));
            }

            return pedido;
        }
    }
}
