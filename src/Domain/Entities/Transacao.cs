using Core.Domain.Entities;
using Domain.ValueObjects;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Transacao : Entity
    {
        public DateTime DataTransacao { get; private set; }
        public decimal ValorTotal { get; private set; }
        public StatusTransacao Status { get; private set; }
        public string QrCodePix { get; private set; }

        public Guid PagamentoId { get; private set; }

        // Relation
        public Pagamento Pagamento { get; private set; }

        public Transacao(decimal valorTotal, StatusTransacao status, Guid pagamentoId)
        {
            Id = Guid.NewGuid();
            DataTransacao = DateTime.UtcNow;
            ValorTotal = valorTotal;
            Status = status;
            PagamentoId = pagamentoId;
            QrCodePix = GerarQrCodePix();
        }

        private static string GerarQrCodePix()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var random = new Random();

            var qrCodeBuilder = new StringBuilder();

            for (var i = 0; i < 50; i++)
            {
                qrCodeBuilder.Append(chars[random.Next(chars.Length)]);
            }

            return qrCodeBuilder.ToString();
        }
    }
}
