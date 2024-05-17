using Domain.Entities;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.UseCases
{
    public class PagamentoUseCase(IPagamentoRepository pagamentoRepository) : IPagamentoUseCase
    {
        private readonly IPagamentoRepository _pagamentoRepository = pagamentoRepository;

        public async Task<bool> EfetuarPagamento(Pedido pedido, FormaPagamento tipoPagamento, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(pedido);

            var pagamento = new Pagamento(pedido.Id, tipoPagamento, pedido.ValorTotal);

            var transacao = new Transacao(pagamento.Valor, StatusTransacao.Pago, pagamento.Id);

            pagamento.AdicionarTransacao(transacao);

            await _pagamentoRepository.InsertAsync(pagamento, cancellationToken);

            return transacao.Status == StatusTransacao.Pago;
        }
    }
}
