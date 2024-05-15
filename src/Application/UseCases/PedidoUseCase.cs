using Application.Models.Request;
using Domain.Entities;
using Domain.Repositories;

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
            var pedidoExistente = await _pedidoRepository.FindByIdAsync(request.PedidoId, cancellationToken);

            if (pedidoExistente is null)
            {
                var pedido = new Pedido(request.PedidoId, request.ClienteId);
                pedido.AlterarStatusPedidoRascunho();

                foreach (var item in request.Items)
                {
                    var produto = await _produtoRepository.FindByIdAsync(item.ProdutoId, cancellationToken);

                    if (produto != null)
                    {
                        pedido.AdicionarItem(new PedidoItem(item.ProdutoId, item.Quantidade, produto.Preco));
                    }
                }

                await _pedidoRepository.InsertAsync(pedido, cancellationToken);

                return await _pedidoRepository.UnitOfWork.CommitAsync(cancellationToken);
            }

            return false;
        }
    }
}
