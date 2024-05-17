using Core.Domain.Abstractions;
using Core.Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int NumeroPedido { get; private set; }
        public Guid? ClienteId { get; private set; }
        public PedidoStatus Status { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusTransacao StatusPagamento { get; private set; }
        public DateTime DataCriacao { get; private set; }

        private readonly Lazy<List<PedidoItem>> _pedidoItemsLazy = new(() => []);
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItemsLazy.Value;

        public Pagamento Pagamento { get; set; }

        // Relation
        protected Pedido() { }

        public Pedido(Guid id, Guid? clienteId)
        {
            Id = id;
            ClienteId = clienteId;
            StatusPagamento = StatusTransacao.Pendente;
            Status = PedidoStatus.Rascunho;
            DataCriacao = DateTime.UtcNow;
        }

        public void AdicionarItem(PedidoItem item)
        {
            var itemExistente = PedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);

            if (itemExistente != null)
            {
                itemExistente.AdicionarUnidades(item.Quantidade);
                item = itemExistente;
            }
            else
            {
                _pedidoItemsLazy.Value.Add(item);
            }

            item.CalcularValor();
            CalcularValorPedido();
        }

        public void RemoverItem(PedidoItem item)
        {
            _pedidoItemsLazy.Value.Remove(item);
            CalcularValorPedido();
        }

        private void CalcularValorPedido() =>
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());

        public bool EfetuarCheckout()
        {
            if (Status == PedidoStatus.Rascunho)
            {
                Status = PedidoStatus.Recebido;
                return true;
            }

            return false;
        }

        public void CancelarCheckout() =>
            Status = PedidoStatus.Rascunho;

        public void AlterarStatusPagamento(StatusTransacao statusPagamento) =>
            StatusPagamento = statusPagamento;

        public bool AlterarStatus(PedidoStatus novoStatus)
        {
            switch (Status)
            {
                case PedidoStatus.Recebido:
                    if (novoStatus == PedidoStatus.EmPreparacao)
                    {
                        Status = novoStatus;
                        return true;
                    }

                    break;
                case PedidoStatus.EmPreparacao:
                    if (novoStatus == PedidoStatus.Pronto)
                    {
                        Status = novoStatus;
                        return true;
                    }

                    break;
                case PedidoStatus.Pronto:
                    if (novoStatus == PedidoStatus.Finalizado)
                    {
                        Status = novoStatus;
                        return true;
                    }

                    break;
            }

            return false;
        }
    }
}
