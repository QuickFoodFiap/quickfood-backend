using Core.Domain.Abstractions;
using Core.Domain.Entities;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Pedido : Entity, IAggregateRoot
    {
        public int NumeroPedido { get; private set; }
        public Guid ClienteId { get; private set; }
        public PedidoStatus PedidoStatus { get; private set; }
        public decimal ValorTotal { get; private set; }

        private readonly List<PedidoItem> _pedidoItems;
        public IReadOnlyCollection<PedidoItem> PedidoItems => _pedidoItems;

        protected Pedido() =>
            _pedidoItems = [];

        public Pedido(Guid id, Guid clienteId)
        {
            Id = id;
            ClienteId = clienteId;
            _pedidoItems = [];
        }

        public bool PedidoItemExistente(PedidoItem item) =>
            _pedidoItems.Any(p => p.ProdutoId == item.ProdutoId);

        public void AdicionarItem(PedidoItem item)
        {
            item.AssociarPedido(Id);

            if (PedidoItemExistente(item))
            {
                var itemExistente = _pedidoItems.FirstOrDefault(p => p.ProdutoId == item.ProdutoId);
                if (itemExistente != null)
                {
                    itemExistente.AdicionarUnidades(item.Quantidade);
                    item = itemExistente;

                    _pedidoItems.Remove(itemExistente);
                }
            }

            item.CalcularValor();
            _pedidoItems.Add(item);

            CalcularValorPedido();
        }

        public void CalcularValorPedido() =>
            ValorTotal = PedidoItems.Sum(p => p.CalcularValor());

        public void AlterarStatusPedidoRascunho() =>
            PedidoStatus = PedidoStatus.Rascunho;

    }
}
