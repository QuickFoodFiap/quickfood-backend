using Core.Domain.Abstractions;
using Core.Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Pagamento : Entity, IAggregateRoot
    {
        public Guid PedidoId { get; private set; }
        public FormaPagamento TipoPagamento { get; private set; }
        public decimal Valor { get; private set; }

        private readonly Lazy<List<Transacao>> _transacoesLazy = new(() => []);
        public IReadOnlyCollection<Transacao> Transacoes => _transacoesLazy.Value;

        public Pagamento(Guid pedidoId, FormaPagamento tipoPagamento, decimal valor)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            TipoPagamento = tipoPagamento;
            Valor = valor;
        }

        public void AdicionarTransacao(Transacao transacao) =>
            _transacoesLazy.Value.Add(transacao);
    }
}
